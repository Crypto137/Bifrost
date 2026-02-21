using Avalonia.Controls;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using System;

namespace Bifrost.Avalonia.Views.Options;

public partial class OptionsAdvancedUserControl : OptionsUserControl
{
    public OptionsAdvancedUserControl()
    {
        InitializeComponent();

        foreach (Downloader downloader in Enum.GetValues<Downloader>())
        {
            ComboBoxItem item = new() { Content = Enum.GetName(downloader) };
            DownloadersComboBox.Items.Add(item);
        }
    }

    public override void Initialize(Window owner, ClientLauncher clientLauncher)
    {
        base.Initialize(owner, clientLauncher);

        LaunchConfig config = _clientLauncher.Config;

        Force32BitCheckBox.IsChecked = config.Force32Bit;
        DownloadersComboBox.SelectedIndex = (int)config.Downloader;

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

        LaunchConfig config = _clientLauncher.Config;

        config.Force32Bit = Force32BitCheckBox.IsChecked == true;
        config.Downloader = (Downloader)DownloadersComboBox.SelectedIndex;

        config.NoSound = NoSoundCheckBox.IsChecked == true;
        config.NoAccount = NoAccountCheckBox.IsChecked == true;
        config.NoOptions = NoOptionsCheckBox.IsChecked == true;
        config.NoStore = NoStoreCheckBox.IsChecked == true;
        config.NoCatalog = NoCatalogCheckBox.IsChecked == true;
        config.NoNews = NoNewsCheckBox.IsChecked == true;
        config.NoLogout = NoLogoutCheckBox.IsChecked == true;
    }
}