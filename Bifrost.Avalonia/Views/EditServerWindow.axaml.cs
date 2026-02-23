using Avalonia.Controls;
using Avalonia.Interactivity;
using Bifrost.Avalonia.Views.Dialogs;
using Bifrost.Core.Models;

namespace Bifrost.Avalonia.Views;

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
        NewsFeedUrlTextBox.Text = serverInfo.NewsFeedUrl;
    }

    #region Event Handlers

    private async void OKButton_Click(object sender, RoutedEventArgs e)
    {
        string name = NameTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            await MessageBoxWindow.Show(this, "Please enter a valid server name.", "Error");
            return;
        }

        string siteConfigUrl = SiteConfigUrlTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(siteConfigUrl))
        {
            await MessageBoxWindow.Show(this, "Please enter a valid site config URL.", "Error");
            return;
        }

        string newsFeedUrl = NewsFeedUrlTextBox.Text.Trim();
        // news feed can be empty

        _serverInfo.Name = name;
        _serverInfo.SiteConfigUrl = siteConfigUrl;
        _serverInfo.NewsFeedUrl = newsFeedUrl;
        HasChanged = true;

        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    #endregion
}