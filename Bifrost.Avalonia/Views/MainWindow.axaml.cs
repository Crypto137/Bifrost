using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Bifrost.Avalonia.Views.Dialogs;
using Bifrost.Avalonia.Views.Options;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using Bifrost.Core.News;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bifrost.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        private const string BackgroundOverrideFileName = "Bifrost.Background.png";

        private ClientLauncher _clientLauncher;

        private int _pendingNewsSources = 0;

        public MainWindow()
        {
            InitializeComponent();

            ApplyBackgroundOverride();

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

            LoadNews();
            RefreshClientData();
            RefreshServerComboBox();
        }

        private void ApplyBackgroundOverride()
        {
            if (File.Exists(BackgroundOverrideFileName) == false)
                return;

            try
            {
                Bitmap bitmap = new(BackgroundOverrideFileName);
                ImageBrush imageBrush = new() { Source = bitmap };
                MainGrid.Background = imageBrush;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void LoadNews()
        {
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

        private void Window_Closing(object sender, WindowClosingEventArgs e)
        {
            _clientLauncher?.SaveData();
        }

        private async void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new(_clientLauncher);
            await optionsWindow.ShowDialog(this);

            // TODO: react to options changes
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
                RefreshServerComboBox();
        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (Design.IsDesignMode)
                return;

            if (_clientLauncher.Launch())
            {
                Close();
                Environment.Exit(0);
            }
            else
            {
                await MessageBoxWindow.Show(this, "Failed to launch game client.", "Error");
            }
        }

        #endregion
    }
}