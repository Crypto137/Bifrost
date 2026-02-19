using Avalonia.Controls;
using Avalonia.Interactivity;
using Bifrost.Avalonia.Views.Dialogs;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using System.Linq;

namespace Bifrost.Avalonia;

public partial class ServerManagementWindow : Window
{
    private readonly ClientLauncher _clientLauncher;

    public bool IsChanged { get; private set; }

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

    private async void AddButton_Click(object sender, RoutedEventArgs e)
    {
        await MessageBoxWindow.Show(this, "Add - not yet implemented", "Error");
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        await MessageBoxWindow.Show(this, "Edit - not yet implemented", "Error");
    }

    private async void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        await MessageBoxWindow.Show(this, "Remove - not yet implemented", "Error");
    }

    #endregion
}