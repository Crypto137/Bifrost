using System;
using System.Linq;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using Bifrost.Launcher;
using Bifrost.Wpf.Models;

namespace Bifrost.Wpf.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly LaunchManager _launchManager;

        private BindableCollection<ServerModel> _serverCollection;
        private ServerModel _selectedServer;

        public BindableCollection<ServerModel> ServerCollection
        {
            get => _serverCollection;
            set => _serverCollection = value;
        }

        public ServerModel SelectedServer
        {
            get => _selectedServer;
            set
            {
                _selectedServer = value;
                NotifyOfPropertyChange(() => SelectedServer);
            }
        }

        public bool NoStartupMovies { get; set; }
        public bool EnableLogging { get; set; }

        public string GameVersion { get => $"Game Version: {_launchManager.GameDirectory.Version}"; }

        public ShellViewModel()
        {
            _windowManager = new WindowManager();

            // Initialize launch manager
            _launchManager = new();

            if (_launchManager.GameDirectory.Initialize(Directory.GetCurrentDirectory(), out string message) == false)
            {
                MessageBox.Show(message, "Error");
                Environment.Exit(0);
            }

            if (_launchManager.GameDirectory.Version == "Unknown")
                MessageBox.Show($"Unknown game version detected. Please report this information:\n{_launchManager.GameDirectory.GetVersionDebugInfo()}", "Warning");

            // Load data
            ServerCollection = new(_launchManager.ServerList.Select(server => new ServerModel(server)));
            SelectedServer = ServerCollection.ElementAtOrDefault(_launchManager.LaunchConfig.ServerIndex);
            EnableLogging = _launchManager.LaunchConfig.EnableLogging;
            NoStartupMovies = _launchManager.LaunchConfig.NoStartupMovies;
        }

        public void Play()
        {
            UpdateLaunchManager();
            _launchManager.Launch();
            Environment.Exit(0);
        }

        public void ManageServers()
        {
            _windowManager.ShowDialogAsync(new ServerManagementViewModel(_serverCollection));

            // Update selected server
            if (ServerCollection.Any())
            {
                SelectedServer = _launchManager.LaunchConfig.ServerIndex >=0 && _launchManager.LaunchConfig.ServerIndex < _serverCollection.Count
                    ? ServerCollection[_launchManager.LaunchConfig.ServerIndex]
                    : ServerCollection[0];
            }
        }

        public void OpenOptions()
        {
            _windowManager.ShowDialogAsync(new OptionsViewModel(_launchManager));
        }

        public void Exit()
        {
            UpdateLaunchManager();
            Environment.Exit(0);
        }

        private void UpdateLaunchManager()
        {
            // Update server list
            _launchManager.ServerList.Clear();
            _launchManager.ServerList.AddRange(ServerCollection.Select(serverModel => serverModel.ToServer()));

            // Update launch config
            _launchManager.LaunchConfig.ServerIndex = ServerCollection.IndexOf(SelectedServer);
            _launchManager.LaunchConfig.EnableLogging = EnableLogging;
            _launchManager.LaunchConfig.NoStartupMovies = NoStartupMovies;

            // Save updated data
            _launchManager.SaveData();
        }
    }
}
