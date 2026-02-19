using Avalonia.Controls;
using Avalonia.Interactivity;
using Bifrost.Avalonia.Views.Dialogs;
using Bifrost.Core.Models;

namespace Bifrost.Avalonia;

public partial class EditServerWindow : Window
{
    private readonly ServerInfo _serverInfo;

    public bool HasChanged { get; private set; }

    public EditServerWindow()
    {
        InitializeComponent();
    }

    public EditServerWindow(ServerInfo serverInfo) : this()
    {
        _serverInfo = serverInfo;

        NameTextBox.Text = serverInfo.Name;
        SiteConfigUrlTextBox.Text = serverInfo.SiteConfigUrl;
    }

    #region Event Handlers

    private async void OKButton_Click(object sender, RoutedEventArgs e)
    {
        string name = NameTextBox.Text;
        if (string.IsNullOrWhiteSpace(name))
        {
            await MessageBoxWindow.Show(this, "Please enter a valid server name.", "Error");
            return;
        }

        string siteConfigUrl = SiteConfigUrlTextBox.Text;
        if (string.IsNullOrWhiteSpace(siteConfigUrl))
        {
            await MessageBoxWindow.Show(this, "Please enter a valid site config URL.", "Error");
            return;
        }

        _serverInfo.Name = name;
        _serverInfo.SiteConfigUrl = siteConfigUrl;
        HasChanged = true;

        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    #endregion
}