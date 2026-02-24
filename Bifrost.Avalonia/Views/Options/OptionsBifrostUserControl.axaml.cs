using Avalonia.Controls;
using Bifrost.Avalonia.Themes;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using Bifrost.Core.News;
using System.Linq;

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

        int i = 0;
        int selectedIndex = -1;

        ThemeOverrideComboBox.Items.Clear();
        foreach (LauncherTheme theme in ThemeManager.Instance.Themes.OrderBy(theme => theme.SortOrder))
        {
            ComboBoxItem item = new() { Content = theme.Name, Tag = theme };
            ThemeOverrideComboBox.Items.Add(item);

            if (theme.Name == config.ThemeOverride)
                selectedIndex = i;
            i++;
        }

        ThemeOverrideComboBox.SelectedIndex = selectedIndex != -1 ? selectedIndex : 0;

        NewsFeedSourceCategories newsCategoryFilter = config.NewsCategoryFilter;
        EnableDefaultNews.IsChecked = newsCategoryFilter.HasFlag(NewsFeedSourceCategories.Default);
        EnableServerNews.IsChecked = newsCategoryFilter.HasFlag(NewsFeedSourceCategories.Server);
        DefaultNewsFeedUrlTextBox.Text = config.DefaultNewsFeedUrl;
    }

    public override void UpdateClientLauncher()
    {
        base.UpdateClientLauncher();

        GuiConfig config = _clientLauncher.GuiConfig;

        if (ThemeOverrideComboBox.SelectedItem is ComboBoxItem themeOverrideItem && themeOverrideItem.Tag is LauncherTheme theme)
            config.ThemeOverride = theme.Name;

        NewsFeedSourceCategories newsCategoryFilter = NewsFeedSourceCategories.All;
        if (EnableDefaultNews.IsChecked != true)
            newsCategoryFilter &= ~NewsFeedSourceCategories.Default;

        if (EnableServerNews.IsChecked != true)
            newsCategoryFilter &= ~NewsFeedSourceCategories.Server;

        config.NewsCategoryFilter = newsCategoryFilter;
        config.DefaultNewsFeedUrl = DefaultNewsFeedUrlTextBox.Text.Trim();
    }
}