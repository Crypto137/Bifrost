using Avalonia.Controls;
using Avalonia.Interactivity;
using Bifrost.Launcher;
using System;
using System.IO;

namespace Bifrost.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        private LaunchManager _launchManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void Window_Opened(object sender, EventArgs e)
        {
            _launchManager = new();

            GameDirectoryInitializationResult result = _launchManager.GameDirectory.Initialize(Directory.GetCurrentDirectory());
            if (result != GameDirectoryInitializationResult.Success)
                Environment.Exit(0);

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
            _launchManager.LaunchConfig.ServerIndex = ServerComboBox.SelectedIndex;
            _launchManager.SaveData();
            _launchManager.Launch();

            Environment.Exit(0);
        }

        #endregion
    }
}