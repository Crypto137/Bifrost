using System;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Bifrost.Core.ClientManagement;
using Bifrost.Wpf.Models;

namespace Bifrost.Wpf.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly ClientLauncher _clientLauncher;

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
            set { _selectedServer = value; NotifyOfPropertyChange(() => SelectedServer); }
        }

        public string GameVersion { get => $"Game Version: {_clientLauncher.ClientMetadata.Version}"; }

        public ShellViewModel()
        {
            _windowManager = new WindowManager();

            // Initialize client launcher
            _clientLauncher = new();

            ClientLauncherInitializationResult result = _clientLauncher.Initialize(Directory.GetCurrentDirectory());
            if (result != ClientLauncherInitializationResult.Success)
            {
                MessageBox.Show(ClientLauncher.GetInitializationResultText(result), "Error");
                Environment.Exit(0);
            }

            if (_clientLauncher.ClientMetadata == ClientMetadata.Unknown)
                MessageBox.Show($"Unknown game version detected. Please report this information:\n{_clientLauncher.GetClientDebugInfo()}", "Warning");

            // Load data
            ServerCollection = new(_clientLauncher.ServerList.Select(server => new ServerModel(server)));
            SelectedServer = ServerCollection.ElementAtOrDefault(_clientLauncher.Config.ServerIndex);
        }

        public void Play()
        {
            UpdateClientLauncher();
            _clientLauncher.Launch();
            Environment.Exit(0);
        }

        public void ManageServers()
        {
            _windowManager.ShowDialogAsync(new ServerManagementViewModel(_serverCollection));

            // Update selected server
            if (ServerCollection.Any())
            {
                SelectedServer = _clientLauncher.Config.ServerIndex >=0 && _clientLauncher.Config.ServerIndex < _serverCollection.Count
                    ? ServerCollection[_clientLauncher.Config.ServerIndex]
                    : ServerCollection[0];
            }
        }

        public void OpenOptions()
        {
            _windowManager.ShowDialogAsync(new OptionsViewModel(_clientLauncher));
        }

        public void Exit()
        {
            UpdateClientLauncher();
            Environment.Exit(0);
        }

        private void UpdateClientLauncher()
        {
            // Update server list
            _clientLauncher.ServerList.Clear();
            _clientLauncher.ServerList.AddRange(ServerCollection.Select(serverModel => serverModel.ToServer()));

            // Update launch config
            _clientLauncher.Config.ServerIndex = ServerCollection.IndexOf(SelectedServer);

            // Save updated data
            _clientLauncher.SaveData();
        }
    }
}
