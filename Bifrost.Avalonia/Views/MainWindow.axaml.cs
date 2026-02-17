using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
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

        #region Event Handlers

        private void Window_Opened(object sender, EventArgs e)
        {
            if (Design.IsDesignMode)
                return;

            _clientLauncher = new();

            ClientLauncherInitializationResult result = _clientLauncher.Initialize(Directory.GetCurrentDirectory());
            if (result != ClientLauncherInitializationResult.Success)
            {
                Close();
                Environment.Exit(0);
            }

            ServerComboBox.Items.Clear();
            foreach (Server server in _clientLauncher.ServerList)
            {
                ComboBoxItem serverItem = new() { Content = server.Name };
                ServerComboBox.Items.Add(serverItem);
            }

            ServerComboBox.SelectedIndex = _clientLauncher.Config.ServerIndex;

            VersionTextBlock.Text = $"Game Version: {_clientLauncher.ClientMetadata.Version}";
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (Design.IsDesignMode)
                return;

            _clientLauncher.Config.ServerIndex = ServerComboBox.SelectedIndex;
            _clientLauncher.SaveData();
            _clientLauncher.Launch();

            Environment.Exit(0);
        }

        #endregion
    }
}