using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Bifrost.Launcher;
using Bifrost.Wpf.Models;

namespace Bifrost.Wpf.ViewModels
{
    public partial class OptionsViewModel : Screen
    {
        private readonly LaunchManager _launchManager;
        private readonly Dictionary<LoggingChannel, LoggingChannelState> _loggingChannelStateDict = new();

        private bool _overrideLoggingLevel;
        private LoggingLevelModel _selectedLoggingLevel;
        private bool _force32Bit;
        private DownloaderModel _selectedDownloader;
        private bool _noAccount;
        private bool _noOptions;
        private bool _noStore;
        private bool _noCatalog;
        private bool _noNews;
        private bool _noLogout;

        public List<LoggingLevelModel> LoggingLevels { get; }
        public List<DownloaderModel> Downloaders { get; }

        // Properties are in a separate file because they are too verbose

        public OptionsViewModel(LaunchManager launchManager)
        {
            LoggingLevels = Enum.GetNames(typeof(LoggingLevel)).Select(name => new LoggingLevelModel(name)).ToList();
            Downloaders = Enum.GetNames(typeof(Downloader)).Select(name => new DownloaderModel(name)).ToList();

            // Initialize data from the launch manager
            _launchManager = launchManager;

            // Logging
            OverrideLoggingLevel = _launchManager.LaunchConfig.OverrideLoggingLevel;
            SelectedLoggingLevel = LoggingLevels[(int)_launchManager.LaunchConfig.LoggingLevel];
            foreach (var kvp in _launchManager.LaunchConfig.LoggingChannelStateDict)
                _loggingChannelStateDict.Add(kvp.Key, kvp.Value);

            // Advanced
            Force32Bit = _launchManager.LaunchConfig.Force32Bit;
            SelectedDownloader = Downloaders[(int)_launchManager.LaunchConfig.Downloader];
            NoAccount = _launchManager.LaunchConfig.NoAccount;
            NoOptions = _launchManager.LaunchConfig.NoOptions;
            NoStore = _launchManager.LaunchConfig.NoStore;
            NoCatalog = _launchManager.LaunchConfig.NoCatalog;
            NoNews = _launchManager.LaunchConfig.NoNews;
            NoLogout = _launchManager.LaunchConfig.NoLogout;
        }

        #region Logging Channel Management

        public void ResetLoggingChannels()
        {
            CheckBoxChannelAllDefault = true;
            CheckBoxChannelErrorDefault = true;
            CheckBoxChannelCoreDefault = true;
            CheckBoxChannelCoreNetDefault = true;
            CheckBoxChannelCoreJobsTPDefault = true;
            CheckBoxChannelGameDefault = true;
            CheckBoxChannelPeerConnectorDefault = true;
            CheckBoxChannelDatastoreDefault = true;
            CheckBoxChannelProfileDefault = true;
            CheckBoxChannelGameNetworkDefault = true;
            CheckBoxChannelPakfileSystemDefault = true;
            CheckBoxChannelLootManagerDefault = true;
            CheckBoxChannelGroupingSystemDefault = true;
            CheckBoxChannelProtobufDumperDefault = true;
            CheckBoxChannelGameDatabaseDefault = true;
            CheckBoxChannelTransitionDefault = true;
            CheckBoxChannelAIDefault = true;
            CheckBoxChannelInventoryDefault = true;
            CheckBoxChannelMemoryDefault = true;
            CheckBoxChannelMissionsDefault = true;
            CheckBoxChannelPatcherDefault = true;
            CheckBoxChannelGenerationDefault = true;
            CheckBoxChannelRespawnDefault = true;
            CheckBoxChannelSaveloadDefault = true;
            CheckBoxChannelFrontendDefault = true;
            CheckBoxChannelCommunityDefault = true;
            CheckBoxChannelAchievementsDefault = true;
            CheckBoxChannelMetricsHttpUploadDefault = true;
            CheckBoxChannelCurrencyConversionDefault = true;
            CheckBoxChannelMobileDefault = true;
            CheckBoxChannelUIDefault = true;
            CheckBoxChannelLeaderboardDefault = true;
        }

        public void DisableAllLoggingChannels()
        {
            CheckBoxChannelAllOff = true;
            CheckBoxChannelErrorOff = true;
            CheckBoxChannelCoreOff = true;
            CheckBoxChannelCoreNetOff = true;
            CheckBoxChannelCoreJobsTPOff = true;
            CheckBoxChannelGameOff = true;
            CheckBoxChannelPeerConnectorOff = true;
            CheckBoxChannelDatastoreOff = true;
            CheckBoxChannelProfileOff = true;
            CheckBoxChannelGameNetworkOff = true;
            CheckBoxChannelPakfileSystemOff = true;
            CheckBoxChannelLootManagerOff = true;
            CheckBoxChannelGroupingSystemOff = true;
            CheckBoxChannelProtobufDumperOff = true;
            CheckBoxChannelGameDatabaseOff = true;
            CheckBoxChannelTransitionOff = true;
            CheckBoxChannelAIOff = true;
            CheckBoxChannelInventoryOff = true;
            CheckBoxChannelMemoryOff = true;
            CheckBoxChannelMissionsOff = true;
            CheckBoxChannelPatcherOff = true;
            CheckBoxChannelGenerationOff = true;
            CheckBoxChannelRespawnOff = true;
            CheckBoxChannelSaveloadOff = true;
            CheckBoxChannelFrontendOff = true;
            CheckBoxChannelCommunityOff = true;
            CheckBoxChannelAchievementsOff = true;
            CheckBoxChannelMetricsHttpUploadOff = true;
            CheckBoxChannelCurrencyConversionOff = true;
            CheckBoxChannelMobileOff = true;
            CheckBoxChannelUIOff = true;
            CheckBoxChannelLeaderboardOff = true;
        }

        public void EnableAllLoggingChannels()
        {
            CheckBoxChannelAllOn = true;
            CheckBoxChannelErrorOn = true;
            CheckBoxChannelCoreOn = true;
            CheckBoxChannelCoreNetOn = true;
            CheckBoxChannelCoreJobsTPOn = true;
            CheckBoxChannelGameOn = true;
            CheckBoxChannelPeerConnectorOn = true;
            CheckBoxChannelDatastoreOn = true;
            CheckBoxChannelProfileOn = true;
            CheckBoxChannelGameNetworkOn = true;
            CheckBoxChannelPakfileSystemOn = true;
            CheckBoxChannelLootManagerOn = true;
            CheckBoxChannelGroupingSystemOn = true;
            CheckBoxChannelProtobufDumperOn = true;
            CheckBoxChannelGameDatabaseOn = true;
            CheckBoxChannelTransitionOn = true;
            CheckBoxChannelAIOn = true;
            CheckBoxChannelInventoryOn = true;
            CheckBoxChannelMemoryOn = true;
            CheckBoxChannelMissionsOn = true;
            CheckBoxChannelPatcherOn = true;
            CheckBoxChannelGenerationOn = true;
            CheckBoxChannelRespawnOn = true;
            CheckBoxChannelSaveloadOn = true;
            CheckBoxChannelFrontendOn = true;
            CheckBoxChannelCommunityOn = true;
            CheckBoxChannelAchievementsOn = true;
            CheckBoxChannelMetricsHttpUploadOn = true;
            CheckBoxChannelCurrencyConversionOn = true;
            CheckBoxChannelMobileOn = true;
            CheckBoxChannelUIOn = true;
            CheckBoxChannelLeaderboardOn = true;
        }

        #endregion

        public void Apply()
        {
            UpdateLaunchManager();
            TryCloseAsync();
        }

        private void UpdateLaunchManager()
        {
            // Logging
            _launchManager.LaunchConfig.OverrideLoggingLevel = OverrideLoggingLevel;
            _launchManager.LaunchConfig.LoggingLevel = (LoggingLevel)LoggingLevels.IndexOf(SelectedLoggingLevel);
            foreach (var kvp in _loggingChannelStateDict)
                _launchManager.LaunchConfig.LoggingChannelStateDict[kvp.Key] = kvp.Value;

            // Advanced
            _launchManager.LaunchConfig.Force32Bit = Force32Bit;
            _launchManager.LaunchConfig.Downloader = (Downloader)Downloaders.IndexOf(SelectedDownloader);
            _launchManager.LaunchConfig.NoAccount = NoAccount;
            _launchManager.LaunchConfig.NoOptions = NoOptions;
            _launchManager.LaunchConfig.NoStore = NoStore;
            _launchManager.LaunchConfig.NoCatalog = NoCatalog;
            _launchManager.LaunchConfig.NoNews = NoNews;
            _launchManager.LaunchConfig.NoLogout = NoLogout;
        }
    }
}
