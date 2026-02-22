using Avalonia.Controls;
using Bifrost.Avalonia.Extensions;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;

namespace Bifrost.Avalonia.Views.Options;

public partial class OptionsAdvancedUserControl : OptionsUserControl
{
    public OptionsAdvancedUserControl()
    {
        InitializeComponent();

        DownloadersComboBox.PopulateFromEnum<Downloader>();
    }

    public override void Initialize(Window owner, ClientLauncher clientLauncher)
    {
        base.Initialize(owner, clientLauncher);

        LaunchConfig config = _clientLauncher.LaunchConfig;

        Force32BitCheckBox.IsChecked = config.Force32Bit;
        DownloadersComboBox.SetSelectedEnumValue(config.Downloader);

        NoSoundCheckBox.IsChecked = config.NoSound;
        NoAccountCheckBox.IsChecked = config.NoAccount;
        NoOptionsCheckBox.IsChecked = config.NoOptions;
        NoStoreCheckBox.IsChecked = config.NoStore;
        NoCatalogCheckBox.IsChecked = config.NoCatalog;
        NoNewsCheckBox.IsChecked = config.NoNews;
        NoLogoutCheckBox.IsChecked = config.NoLogout;
    }

    public override void UpdateClientLauncher()
    {
        base.UpdateClientLauncher();

        LaunchConfig config = _clientLauncher.LaunchConfig;

        config.Force32Bit = Force32BitCheckBox.IsChecked == true;
        config.Downloader = DownloadersComboBox.GetSelectedEnumValue<Downloader>();

        config.NoSound = NoSoundCheckBox.IsChecked == true;
        config.NoAccount = NoAccountCheckBox.IsChecked == true;
        config.NoOptions = NoOptionsCheckBox.IsChecked == true;
        config.NoStore = NoStoreCheckBox.IsChecked == true;
        config.NoCatalog = NoCatalogCheckBox.IsChecked == true;
        config.NoNews = NoNewsCheckBox.IsChecked == true;
        config.NoLogout = NoLogoutCheckBox.IsChecked == true;
    }
}