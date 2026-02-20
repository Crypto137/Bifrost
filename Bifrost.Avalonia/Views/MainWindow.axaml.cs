using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Bifrost.Avalonia.Views.Dialogs;
using Bifrost.Avalonia.Views.Options;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using System;
using System.IO;

namespace Bifrost.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        private const string BackgroundOverrideFileName = "Bifrost.Background.png";

        private ClientLauncher _clientLauncher;

        public MainWindow()
        {
            InitializeComponent();

            ApplyBackgroundOverride();
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

            ServerComboBox.SelectedIndex = _clientLauncher.Config.ServerIndex;
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

            _clientLauncher.Config.ServerIndex = selectedIndex;
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
                Environment.Exit(0);
            else
                await MessageBoxWindow.Show(this, "Failed to launch game client.", "Error");
        }

        #endregion
    }
}