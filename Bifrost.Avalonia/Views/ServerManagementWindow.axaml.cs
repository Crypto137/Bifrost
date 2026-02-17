using Avalonia.Controls;
using Avalonia.Interactivity;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using System.Linq;

namespace Bifrost.Avalonia;

public partial class ServerManagementWindow : Window
{
    private readonly ClientLauncher _clientLauncher;

    public ServerManagementWindow()
    {
        InitializeComponent();
    }

    public ServerManagementWindow(ClientLauncher clientLauncher) : this()
    {
        _clientLauncher = clientLauncher;
        PopulateServerListBox();
    }

    private void PopulateServerListBox()
    {
        if (_clientLauncher == null)
            return;

        ServerListBox.Items.Clear();

        foreach (Server server in _clientLauncher.ServerList)
        {
            ListBoxItem item = new() { Content = $"{server.Name} ({server.SiteConfigUrl.Split('/').FirstOrDefault()})", Tag = server };
            ServerListBox.Items.Add(item);
        }

        ServerListBox.SelectedIndex = _clientLauncher.Config.ServerIndex;
    }

    #region Event Handlers

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
    }

    #endregion
}