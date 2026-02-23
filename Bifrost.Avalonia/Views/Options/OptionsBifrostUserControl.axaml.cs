using Avalonia.Controls;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using Bifrost.Core.News;

namespace Bifrost.Avalonia.Views.Options;

public partial class OptionsBifrostUserControl : OptionsUserControl
{
    public OptionsBifrostUserControl()
    {
        InitializeComponent();
    }

    public override void Initialize(Window owner, ClientLauncher clientLauncher)
    {
        base.Initialize(owner, clientLauncher);

        GuiConfig config = _clientLauncher.GuiConfig;

        NewsFeedSourceCategories newsCategoryFilter = config.NewsCategoryFilter;
        EnableDefaultNews.IsChecked = newsCategoryFilter.HasFlag(NewsFeedSourceCategories.Default);
        EnableServerNews.IsChecked = newsCategoryFilter.HasFlag(NewsFeedSourceCategories.Server);
        DefaultNewsFeedUrlTextBox.Text = config.DefaultNewsFeedUrl;
    }

    public override void UpdateClientLauncher()
    {
        base.UpdateClientLauncher();

        GuiConfig config = _clientLauncher.GuiConfig;

        NewsFeedSourceCategories newsCategoryFilter = NewsFeedSourceCategories.All;
        if (EnableDefaultNews.IsChecked != true)
            newsCategoryFilter &= ~NewsFeedSourceCategories.Default;

        if (EnableServerNews.IsChecked != true)
            newsCategoryFilter &= ~NewsFeedSourceCategories.Server;

        config.NewsCategoryFilter = newsCategoryFilter;
        config.DefaultNewsFeedUrl = DefaultNewsFeedUrlTextBox.Text.Trim();
    }
}