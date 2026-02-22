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
    private static readonly GridLength LoggingChannelCheckBoxLength = new(100, GridUnitType.Pixel);

    private readonly Dictionary<LoggingChannel, LoggingChannelCheckBoxSet> _checkboxes = new();

    public OptionsLoggingUserControl()
    {
        InitializeComponent();

        LoggingLevelComboBox.PopulateFromEnum<LoggingLevel>();

        // Create checkboxes for all logging channels
        foreach (LoggingChannel loggingChannel in Enum.GetValues<LoggingChannel>())
        {
            // Create grid for this logging channel
            Grid grid = new();
            grid.AddColumns(GridLength.Star, LoggingChannelCheckBoxLength, LoggingChannelCheckBoxLength, LoggingChannelCheckBoxLength);
            LoggingChannelStackPanel.Children.Add(grid);
            
            // Populate the grid
            Label label = new() { Content = Enum.GetName(loggingChannel).Replace("_", "__") };
            grid.AddToColumn(label, 0);

            LoggingChannelCheckBoxSet checkBoxes = new();
            grid.AddToColumn(checkBoxes.Default, 1);
            grid.AddToColumn(checkBoxes.Off, 2);
            grid.AddToColumn(checkBoxes.On, 3);

            // Hook up checkbox events and add a lookup
            checkBoxes.Default.Click += (sender, e) => SetLoggingChannelState(loggingChannel, LoggingChannelState.Default);
            checkBoxes.Off.Click += (sender, e) => SetLoggingChannelState(loggingChannel, LoggingChannelState.Off);
            checkBoxes.On.Click += (sender, e) => SetLoggingChannelState(loggingChannel, LoggingChannelState.On);

            _checkboxes.Add(loggingChannel, checkBoxes);
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
        LoggingChannelCheckBoxSet checkBoxes = _checkboxes[channel];

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

    private readonly struct LoggingChannelCheckBoxSet
    {
        public readonly CheckBox Default;
        public readonly CheckBox Off;
        public readonly CheckBox On;

        public LoggingChannelCheckBoxSet()
        {
            Default = new();
            Off = new();
            On = new();
        }

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