using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Bifrost.Avalonia.Themes;
using Bifrost.Avalonia.Views.Dialogs;
using Bifrost.Avalonia.Views.Options;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using Bifrost.Core.News;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bifrost.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<NewsFeedSourceCategories, IBrush> _newsCategoryBrushes;

        private ClientLauncher _clientLauncher;

        private int _pendingNewsSources = 0;

        public MainWindow()
        {
            InitializeComponent();

            _newsCategoryBrushes = new()
            {
                { NewsFeedSourceCategories.Default,     SolidColorBrush.Parse("#0088cc") },
                { NewsFeedSourceCategories.Server,      SolidColorBrush.Parse("#0f9c23") }
            };

            ThemeManager.Instance.Initialize(AppContext.BaseDirectory);

            if (MainGrid.Background is ImageBrush defaultBackgroundBrush && defaultBackgroundBrush.Source is Bitmap defaultBackground)
                ThemeManager.Instance.AddTheme(new(LauncherTheme.DefaultName, -1, defaultBackground));

            // Clear design news placeholders asap
            if (Design.IsDesignMode == false)
                NewsItemStackPanel.Children.Clear();
        }

        private void Initialize()
        {
            _clientLauncher = new();

            string clientPath = ClientHelper.GetClientPath();
            ClientLauncherInitializationResult result = _clientLauncher.Initialize(clientPath);
            if (result != ClientLauncherInitializationResult.Success)
            {
                Close();
                Environment.Exit(0);
            }

            ApplyTheme(_clientLauncher.GuiConfig.ThemeOverride);
            LoadNews();
            RefreshClientData();
            RefreshServerComboBox();
        }

        private void ApplyTheme(string name)
        {
            LauncherTheme theme = ThemeManager.Instance.GetTheme(name);
            if (theme == null)
                return;

            ImageBrush imageBrush = new() { Source = theme.Background };
            MainGrid.Background = imageBrush;
        }

        private void LoadNews()
        {
            if (_clientLauncher.GuiConfig.NewsCategoryFilter == NewsFeedSourceCategories.None)
            {
                NewsGrid.IsVisible = false;
                return;
            }

            NewsGrid.IsVisible = true;

            _clientLauncher.RefreshNewsFeedSources();
            Interlocked.Add(ref _pendingNewsSources, _clientLauncher.NewsFeed.Sources.Count);

            foreach (NewsFeedSource source in _clientLauncher.NewsFeed.Sources.Values)
                Task.Run(() => source.Load(OnNewsFeedSourceLoaded));
        }

        private void OnNewsFeedSourceLoaded(NewsFeedSource source)
        {
            Interlocked.Decrement(ref _pendingNewsSources);

            if (_pendingNewsSources == 0)
                Dispatcher.UIThread.Invoke(RefreshNewsFeed);
        }

        private void RefreshNewsFeed()
        {
            const int MaxNewsFeedItems = 10;

            List<NewsFeedItem> newsList = new();
            _clientLauncher.NewsFeed.GetNews(newsList);

            Controls newsItems = NewsItemStackPanel.Children;
            newsItems.Clear();

            for (int i = 0; i < newsList.Count && i < MaxNewsFeedItems; i++)
            {
                NewsFeedItem newsItem = newsList[i];
                HyperlinkButton button = new() { NavigateUri = new Uri(newsItem.Url) };

                if (_newsCategoryBrushes.TryGetValue(newsItem.Source.Category, out IBrush borderBrush))
                    button.BorderBrush = borderBrush;

                button.Classes.Add("news-item");
                button.Content = new TextBlock() { Text = newsItem.Title };
                newsItems.Add(button);
            }
        }

        private void RefreshClientData()
        {
            VersionTextBlock.Text = $"Game Version: {_clientLauncher.ClientMetadata.Version}";
        }

        private void RefreshServerComboBox()
        {
            ServerComboBox.Items.Clear();
            foreach (ServerInfo serverInfo in _clientLauncher.ServerManager)
            {
                ComboBoxItem serverItem = new() { Content = serverInfo.Name };
                ServerComboBox.Items.Add(serverItem);
            }

            ServerComboBox.SelectedIndex = _clientLauncher.LaunchConfig.ServerIndex;
        }

        #region Event Handlers

        private void Window_Opened(object sender, EventArgs e)
        {
            if (Design.IsDesignMode == false)
                Initialize();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _clientLauncher?.SaveData();
        }

        private async void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            GuiConfig oldConfig = _clientLauncher.GuiConfig.Clone();

            OptionsWindow optionsWindow = new(_clientLauncher);
            await optionsWindow.ShowDialog(this);

            if (_clientLauncher.GuiConfig.ThemeOverride != oldConfig.ThemeOverride)
                ApplyTheme(_clientLauncher.GuiConfig.ThemeOverride);

            if (_clientLauncher.GuiConfig.NewsCategoryFilter != oldConfig.NewsCategoryFilter ||
                _clientLauncher.GuiConfig.DefaultNewsFeedUrl != oldConfig.DefaultNewsFeedUrl)
            {
                LoadNews();
            }
        }

        private void ServerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_clientLauncher == null)
                return;

            int selectedIndex = ServerComboBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            _clientLauncher.LaunchConfig.ServerIndex = selectedIndex;
        }

        private async void ManageServersButton_Click(object sender, RoutedEventArgs e)
        {
            ServerManagementWindow serverManagementWindow = new(_clientLauncher);
            await serverManagementWindow.ShowDialog(this);

            if (serverManagementWindow.ServerListChanged)
            {
                LoadNews();
                RefreshServerComboBox();
            }
        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (Design.IsDesignMode)
                return;

            BottomControlsGrid.IsEnabled = false;

            // Do the launch asynchronously because it may be delayed by an antivirus check.
            if (await Task.Run(_clientLauncher.Launch))
            {
                Close();
            }
            else
            {
                await MessageBoxWindow.Show(this, "Failed to launch game client.", "Error");
                BottomControlsGrid.IsEnabled = true;
            }
        }

        #endregion
    }
}