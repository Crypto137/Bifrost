using Avalonia.Controls;
using Avalonia.Interactivity;
using Bifrost.Avalonia.Extensions;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using System;
using System.Collections.Generic;

namespace Bifrost.Avalonia.Views.Options;

public partial class OptionsLoggingUserControl : OptionsUserControl
{
    private readonly Dictionary<LoggingChannel, CheckBoxSet> _checkboxes;

    public OptionsLoggingUserControl()
    {
        InitializeComponent();

        LoggingLevelComboBox.PopulateFromEnum<LoggingLevel>();

        _checkboxes = new()
        {
            { LoggingChannel.ALL,                 new(CheckBoxChannelAllDefault,                CheckBoxChannelAllOff,                CheckBoxChannelAllOn) },
            { LoggingChannel.ERROR,               new(CheckBoxChannelErrorDefault,              CheckBoxChannelErrorOff,              CheckBoxChannelErrorOn) },
            { LoggingChannel.CORE,                new(CheckBoxChannelCoreDefault,               CheckBoxChannelCoreOff,               CheckBoxChannelCoreOn) },
            { LoggingChannel.CORE_NET,            new(CheckBoxChannelCoreNetDefault,            CheckBoxChannelCoreNetOff,            CheckBoxChannelCoreNetOn) },
            { LoggingChannel.CORE_JOBS_TP,        new(CheckBoxChannelCoreJobsTPDefault,         CheckBoxChannelCoreJobsTPOff,         CheckBoxChannelCoreJobsTPOn) },
            { LoggingChannel.GAME,                new(CheckBoxChannelGameDefault,               CheckBoxChannelGameOff,               CheckBoxChannelGameOn) },
            { LoggingChannel.PEER_CONNECTOR,      new(CheckBoxChannelPeerConnectorDefault,      CheckBoxChannelPeerConnectorOff,      CheckBoxChannelPeerConnectorOn) },
            { LoggingChannel.DATASTORE,           new(CheckBoxChannelDatastoreDefault,          CheckBoxChannelDatastoreOff,          CheckBoxChannelDatastoreOn) },
            { LoggingChannel.PROFILE,             new(CheckBoxChannelProfileDefault,            CheckBoxChannelProfileOff,            CheckBoxChannelProfileOn) },
            { LoggingChannel.GAME_NETWORK,        new(CheckBoxChannelGameNetworkDefault,        CheckBoxChannelGameNetworkOff,        CheckBoxChannelGameNetworkOn) },
            { LoggingChannel.PAKFILE_SYSTEM,      new(CheckBoxChannelPakfileSystemDefault,      CheckBoxChannelPakfileSystemOff,      CheckBoxChannelPakfileSystemOn) },
            { LoggingChannel.LOOT_MANAGER,        new(CheckBoxChannelLootManagerDefault,        CheckBoxChannelLootManagerOff,        CheckBoxChannelLootManagerOn) },
            { LoggingChannel.GROUPING_SYSTEM,     new(CheckBoxChannelGroupingSystemDefault,     CheckBoxChannelGroupingSystemOff,     CheckBoxChannelGroupingSystemOn) },
            { LoggingChannel.PROTOBUF_DUMPER,     new(CheckBoxChannelProtobufDumperDefault,     CheckBoxChannelProtobufDumperOff,     CheckBoxChannelProtobufDumperOn) },
            { LoggingChannel.GAME_DATABASE,       new(CheckBoxChannelGameDatabaseDefault,       CheckBoxChannelGameDatabaseOff,       CheckBoxChannelGameDatabaseOn) },
            { LoggingChannel.TRANSITION,          new(CheckBoxChannelTransitionDefault,         CheckBoxChannelTransitionOff,         CheckBoxChannelTransitionOn) },
            { LoggingChannel.AI,                  new(CheckBoxChannelAIDefault,                 CheckBoxChannelAIOff,                 CheckBoxChannelAIOn) },
            { LoggingChannel.INVENTORY,           new(CheckBoxChannelInventoryDefault,          CheckBoxChannelInventoryOff,          CheckBoxChannelInventoryOn) },
            { LoggingChannel.MEMORY,              new(CheckBoxChannelMemoryDefault,             CheckBoxChannelMemoryOff,             CheckBoxChannelMemoryOn) },
            { LoggingChannel.MISSIONS,            new(CheckBoxChannelMissionsDefault,           CheckBoxChannelMissionsOff,           CheckBoxChannelMissionsOn) },
            { LoggingChannel.PATCHER,             new(CheckBoxChannelPatcherDefault,            CheckBoxChannelPatcherOff,            CheckBoxChannelPatcherOn) },
            { LoggingChannel.GENERATION,          new(CheckBoxChannelGenerationDefault,         CheckBoxChannelGenerationOff,         CheckBoxChannelGenerationOn) },
            { LoggingChannel.RESPAWN,             new(CheckBoxChannelRespawnDefault,            CheckBoxChannelRespawnOff,            CheckBoxChannelRespawnOn) },
            { LoggingChannel.SAVELOAD,            new(CheckBoxChannelSaveloadDefault,           CheckBoxChannelSaveloadOff,           CheckBoxChannelSaveloadOn) },
            { LoggingChannel.FRONTEND,            new(CheckBoxChannelFrontendDefault,           CheckBoxChannelFrontendOff,           CheckBoxChannelFrontendOn) },
            { LoggingChannel.COMMUNITY,           new(CheckBoxChannelCommunityDefault,          CheckBoxChannelCommunityOff,          CheckBoxChannelCommunityOn) },
            { LoggingChannel.ACHIEVEMENTS,        new(CheckBoxChannelAchievementsDefault,       CheckBoxChannelAchievementsOff,       CheckBoxChannelAchievementsOn) },
            { LoggingChannel.METRICS_HTTP_UPLOAD, new(CheckBoxChannelMetricsHttpUploadDefault,  CheckBoxChannelMetricsHttpUploadOff,  CheckBoxChannelMetricsHttpUploadOn) },
            { LoggingChannel.CURRENCY_CONVERSION, new(CheckBoxChannelCurrencyConversionDefault, CheckBoxChannelCurrencyConversionOff, CheckBoxChannelCurrencyConversionOn) },
            { LoggingChannel.MOBILE,              new(CheckBoxChannelMobileDefault,             CheckBoxChannelMobileOff,             CheckBoxChannelMobileOn) },
            { LoggingChannel.UI,                  new(CheckBoxChannelUIDefault,                 CheckBoxChannelUIOff,                 CheckBoxChannelUIOn) },
            { LoggingChannel.LEADERBOARD,         new(CheckBoxChannelLeaderboardDefault,        CheckBoxChannelLeaderboardOff,        CheckBoxChannelLeaderboardOn) },
        };

        foreach (var kvp in _checkboxes)
        {
            LoggingChannel channel = kvp.Key;
            CheckBoxSet checkBoxes = kvp.Value;

            checkBoxes.Default.Click += (sender, e) => SetLoggingChannelState(channel, LoggingChannelState.Default);
            checkBoxes.Off.Click += (sender, e) => SetLoggingChannelState(channel, LoggingChannelState.Off);
            checkBoxes.On.Click += (sender, e) => SetLoggingChannelState(channel, LoggingChannelState.On);
        }
    }

    public override void Initialize(Window owner, ClientLauncher clientLauncher)
    {
        base.Initialize(owner, clientLauncher);

        LaunchConfig config = _clientLauncher.Config;

        EnableLoggingCheckBox.IsChecked = config.EnableLogging;
        OverrideLoggingLevelCheckBox.IsChecked = config.OverrideLoggingLevel;
        LoggingLevelComboBox.SetSelectedEnumValue(config.LoggingLevel);
        EnableLoggingLevelComboBox(config.OverrideLoggingLevel);

        foreach (LoggingChannel channel in _checkboxes.Keys)
        {
            if (config.LoggingChannelStateDict.TryGetValue(channel, out LoggingChannelState state))
                SetLoggingChannelState(channel, state);
        }
    }

    public override void UpdateClientLauncher()
    {
        base.UpdateClientLauncher();

        LaunchConfig config = _clientLauncher.Config;

        config.EnableLogging = EnableLoggingCheckBox.IsChecked == true;
        config.OverrideLoggingLevel = OverrideLoggingLevelCheckBox.IsChecked == true;
        config.LoggingLevel = LoggingLevelComboBox.GetSelectedEnumValue<LoggingLevel>();

        foreach (var kvp in _checkboxes)
            config.LoggingChannelStateDict[kvp.Key] = kvp.Value.GetState();
    }

    private void EnableLoggingLevelComboBox(bool enable)
    {
        LoggingLevelComboBox.IsEnabled = enable;
    }

    private void SetLoggingChannelState(LoggingChannel channel, LoggingChannelState state)
    {
        CheckBoxSet checkBoxes = _checkboxes[channel];

        switch (state)
        {
            case LoggingChannelState.Default:
                checkBoxes.Default.IsChecked = true;
                checkBoxes.Off.IsChecked = false;
                checkBoxes.On.IsChecked = false;
                break;

            case LoggingChannelState.Off:
                checkBoxes.Default.IsChecked = false;
                checkBoxes.Off.IsChecked = true;
                checkBoxes.On.IsChecked = false;
                break;

            case LoggingChannelState.On:
                checkBoxes.Default.IsChecked = false;
                checkBoxes.Off.IsChecked = false;
                checkBoxes.On.IsChecked = true;
                break;
        }
    }

    #region Event Handlers

    private void OverrideLoggingLevelCheckBox_IsCheckedChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox)
            return;

        EnableLoggingLevelComboBox(checkBox.IsChecked == true);
    }

    private void ResetLoggingChannels_Click(object sender, RoutedEventArgs e)
    {
        foreach (LoggingChannel channel in _checkboxes.Keys)
            SetLoggingChannelState(channel, LoggingChannelState.Default);
    }

    private void DisableAllLoggingChannels_Click(object sender, RoutedEventArgs e)
    {
        foreach (LoggingChannel channel in _checkboxes.Keys)
            SetLoggingChannelState(channel, LoggingChannelState.Off);
    }

    private void EnableAllLoggingChannels_Click(object sender, RoutedEventArgs e)
    {
        foreach (LoggingChannel channel in _checkboxes.Keys)
            SetLoggingChannelState(channel, LoggingChannelState.On);
    }

    #endregion

    private readonly struct CheckBoxSet(CheckBox @default, CheckBox off, CheckBox on)
    {
        public readonly CheckBox Default = @default;
        public readonly CheckBox Off = off;
        public readonly CheckBox On = on;

        public LoggingChannelState GetState()
        {
            if (Default.IsChecked == true)
                return LoggingChannelState.Default;

            if (Off.IsChecked == true)
                return LoggingChannelState.Off;

            if (On.IsChecked == true)
                return LoggingChannelState.On;

            throw new InvalidOperationException();
        }
    }
}