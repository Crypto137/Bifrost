using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using Bifrost.Avalonia.Views.Dialogs;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Bifrost.Avalonia.Views;

public partial class ServerManagementWindow : Window
{
    private readonly ClientLauncher _clientLauncher;

    public bool ServerListChanged { get; private set; }

    public ServerManagementWindow()
    {
        InitializeComponent();
    }

    public ServerManagementWindow(ClientLauncher clientLauncher) : this()
    {
        _clientLauncher = clientLauncher;
        RefreshServerListBox();
    }

    private void RefreshServerListBox()
    {
        if (_clientLauncher == null)
            return;

        ServerListBox.Items.Clear();

        foreach (ServerInfo serverInfo in _clientLauncher.ServerManager)
        {
            ListBoxItem item = new() { Content = $"{serverInfo.Name} ({serverInfo.SiteConfigUrl.Split('/').FirstOrDefault()})", Tag = serverInfo };
            ServerListBox.Items.Add(item);
        }

        ServerListBox.SelectedIndex = _clientLauncher.Config.ServerIndex;
    }

    private async Task<bool> EditServer(ServerInfo serverInfo)
    {
        EditServerWindow editServerWindow = new(serverInfo);
        await editServerWindow.ShowDialog(this);
        return editServerWindow.HasChanged;
    }

    #region Event Handlers

    private async void AddButton_Click(object sender, RoutedEventArgs e)
    {
        if (_clientLauncher == null)
            return;

        // Add a new server and open it for editing immediately.
        ServerInfo serverInfo = _clientLauncher.ServerManager.AddServer();

        if (await EditServer(serverInfo) == false)
        {
            // Interpret edit cancellation as add cancellation.
            _clientLauncher.ServerManager.RemoveServer(serverInfo);
            return;
        }

        _clientLauncher.Config.ServerIndex = _clientLauncher.ServerManager.ServerCount - 1;

        ServerListChanged = true;
        RefreshServerListBox();
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (_clientLauncher == null)
            return;

        int selectedIndex = ServerListBox.SelectedIndex;
        if (selectedIndex < 0 || selectedIndex >= ServerListBox.ItemCount)
            return;

        ServerInfo serverInfo = _clientLauncher.ServerManager.GetServer(selectedIndex);
        if (serverInfo == null)
            return;

        if (await EditServer(serverInfo))
        {
            ServerListChanged = true;
            RefreshServerListBox();
        }
    }

    private async void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        if (_clientLauncher == null)
            return;

        int selectedIndex = ServerListBox.SelectedIndex;
        if (selectedIndex < 0 || selectedIndex >= ServerListBox.ItemCount)
            return;

        ServerInfo serverInfo = _clientLauncher.ServerManager.GetServer(selectedIndex);
        if (serverInfo == null)
            return;

        DialogResult dialogResult = await MessageBoxWindow.Show(this, $"Are you sure you want to remove {serverInfo.Name}?", "Remove Server", MessageBoxType.OKCancel);
        if (dialogResult != DialogResult.OK)
            return;

        _clientLauncher.ServerManager.RemoveServer(selectedIndex);

        if (_clientLauncher.Config.ServerIndex >= _clientLauncher.ServerManager.ServerCount)
            _clientLauncher.Config.ServerIndex = _clientLauncher.ServerManager.ServerCount - 1;

        ServerListChanged = true;
        RefreshServerListBox();
    }

    private void ServerListBox_DoubleTapped(object sender, TappedEventArgs e)
    {
        // treat it as an edit button click, quick and dirty, but it works.
        EditButton_Click(null, null);
    }

    #endregion
}