using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Bifrost.Core;
using System;
using System.IO;

namespace Bifrost.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        private const string BackgroundOverrideFileName = "Bifrost.Background.png";

        private LaunchManager _launchManager;

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

            _launchManager = new();

            GameDirectoryInitializationResult result = _launchManager.GameDirectory.Initialize(Directory.GetCurrentDirectory());
            if (result != GameDirectoryInitializationResult.Success)
            {
                Close();
                Environment.Exit(0);
            }

            ServerComboBox.Items.Clear();
            foreach (Server server in _launchManager.ServerList)
            {
                ComboBoxItem serverItem = new() { Content = server.Name };
                ServerComboBox.Items.Add(serverItem);
            }

            ServerComboBox.SelectedIndex = _launchManager.LaunchConfig.ServerIndex;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (Design.IsDesignMode)
                return;

            _launchManager.LaunchConfig.ServerIndex = ServerComboBox.SelectedIndex;
            _launchManager.SaveData();
            _launchManager.Launch();

            Environment.Exit(0);
        }

        #endregion
    }
}