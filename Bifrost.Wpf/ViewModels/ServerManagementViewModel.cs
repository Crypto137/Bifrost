using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Bifrost.Wpf.Models;

namespace Bifrost.Wpf.ViewModels
{
    public class ServerManagementViewModel : Screen
    {
        private readonly BindableCollection<ServerModel> _shellServerCollection;

        private BindableCollection<ServerModel> _managedServerCollection;
        private ServerModel _selectedServer;

        public BindableCollection<ServerModel> ServerCollection
        {
            get => _managedServerCollection;
            set => _managedServerCollection = value;
        }

        public ServerModel SelectedServer
        {
            get => _selectedServer;
            set
            {
                _selectedServer = value;
                NotifyOfPropertyChange(() => SelectedServer);
                NotifyOfPropertyChange(() => ServerIsSelected);
            }
        }

        public bool ServerIsSelected { get => SelectedServer != null; }

        public ServerManagementViewModel(BindableCollection<ServerModel> serverCollection)
        {
            // Clone data from the shell view model while we edit it
            _shellServerCollection = serverCollection;
            _managedServerCollection = new(_shellServerCollection.Select(server => (ServerModel)server.Clone()).ToList());
        }

        public void AddNewServer()
        {
            ServerModel newServer = new("Local Server", "localhost/SiteConfig.xml");
            _managedServerCollection.Add(newServer);
            SelectedServer = newServer;
        }

        public void DeleteSelectedServer()
        {
            _managedServerCollection.Remove(SelectedServer);
            SelectedServer = null;
        }

        public void Apply()
        {
            if (ValidateServerList() == false)
                return;

            UpdateShellServerCollection();
            TryCloseAsync();
        }

        private void UpdateShellServerCollection()
        {
            _shellServerCollection.Clear();
            _shellServerCollection.AddRange(_managedServerCollection);
        }

        private bool ValidateServerList()
        {
            foreach (ServerModel server in ServerCollection)
            {
                if (string.IsNullOrWhiteSpace(server.Name))
                {
                    MessageBox.Show("Invalid server name.", "Error");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(server.SiteConfigUrl))
                {
                    MessageBox.Show($"Invalid site config URL for server {server.Name}.", "Error");
                    return false;
                }
            }

            return true;
        }
    }
}
