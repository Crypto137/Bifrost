using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bifrost.Core.ClientManagement;
using Bifrost.Core.Models;
using Bifrost.Wpf.Models;

namespace Bifrost.Wpf.ViewModels
{
    public class OptionsLoggingViewModel : OptionsCategoryBaseViewModel
    {
        private readonly Dictionary<LoggingChannel, LoggingChannelState> _loggingChannelStateDict = new();
        private Dictionary<LoggingChannel, Expression<Func<bool>>[]> _loggingChannelCheckboxDict;

        private bool _enableLogging;
        private bool _overrideLoggingLevel;
        private LoggingLevelModel _selectedLoggingLevel;

        public List<LoggingLevelModel> LoggingLevels { get; }

        public OptionsLoggingViewModel(LaunchManager launchManager) : base(launchManager)
        {
            LoggingLevels = Enum.GetNames(typeof(LoggingLevel)).Select(name => new LoggingLevelModel(name)).ToList();
            InitLoggingChannelCheckboxes();

            _enableLogging = _launchManager.LaunchConfig.EnableLogging;
            _overrideLoggingLevel = _launchManager.LaunchConfig.OverrideLoggingLevel;
            _selectedLoggingLevel = LoggingLevels[(int)_launchManager.LaunchConfig.LoggingLevel];

            foreach (var kvp in _launchManager.LaunchConfig.LoggingChannelStateDict)
                _loggingChannelStateDict.Add(kvp.Key, kvp.Value);
        }

        public override void UpdateLaunchManager()
        {
            _launchManager.LaunchConfig.EnableLogging = _enableLogging;
            _launchManager.LaunchConfig.OverrideLoggingLevel = _overrideLoggingLevel;
            _launchManager.LaunchConfig.LoggingLevel = (LoggingLevel)LoggingLevels.IndexOf(SelectedLoggingLevel);
            foreach (var kvp in _loggingChannelStateDict)
                _launchManager.LaunchConfig.LoggingChannelStateDict[kvp.Key] = kvp.Value;
        }

        public void ResetLoggingChannels()
        {
            foreach (LoggingChannel channel in Enum.GetValues<LoggingChannel>())
                SetLoggingChannelState(channel, LoggingChannelState.Default);
        }

        public void DisableAllLoggingChannels()
        {
            foreach (LoggingChannel channel in Enum.GetValues<LoggingChannel>())
                SetLoggingChannelState(channel, LoggingChannelState.Off);
        }

        public void EnableAllLoggingChannels()
        {
            foreach (LoggingChannel channel in Enum.GetValues<LoggingChannel>())
                SetLoggingChannelState(channel, LoggingChannelState.On);
        }

        private void InitLoggingChannelCheckboxes()
        {
            _loggingChannelCheckboxDict = new()
            {
                { LoggingChannel.ALL,                   new Expression<Func<bool>>[] { () => CheckBoxChannelAllDefault,                 () => CheckBoxChannelAllOff,                () => CheckBoxChannelAllOn } },
                { LoggingChannel.ERROR,                 new Expression<Func<bool>>[] { () => CheckBoxChannelErrorDefault,               () => CheckBoxChannelErrorOff,              () => CheckBoxChannelErrorOn } },
                { LoggingChannel.CORE,                  new Expression<Func<bool>>[] { () => CheckBoxChannelCoreDefault,                () => CheckBoxChannelCoreOff,               () => CheckBoxChannelCoreOn } },
                { LoggingChannel.CORE_NET,              new Expression<Func<bool>>[] { () => CheckBoxChannelCoreNetDefault,             () => CheckBoxChannelCoreNetOff,            () => CheckBoxChannelCoreNetOn } },
                { LoggingChannel.CORE_JOBS_TP,          new Expression<Func<bool>>[] { () => CheckBoxChannelCoreJobsTPDefault,          () => CheckBoxChannelCoreJobsTPOff,         () => CheckBoxChannelCoreJobsTPOn } },
                { LoggingChannel.GAME,                  new Expression<Func<bool>>[] { () => CheckBoxChannelGameDefault,                () => CheckBoxChannelGameOff,               () => CheckBoxChannelGameOn } },
                { LoggingChannel.PEER_CONNECTOR,        new Expression<Func<bool>>[] { () => CheckBoxChannelPeerConnectorDefault,       () => CheckBoxChannelPeerConnectorOff,      () => CheckBoxChannelPeerConnectorOn } },
                { LoggingChannel.DATASTORE,             new Expression<Func<bool>>[] { () => CheckBoxChannelDatastoreDefault,           () => CheckBoxChannelDatastoreOff,          () => CheckBoxChannelDatastoreOn } },
                { LoggingChannel.PROFILE,               new Expression<Func<bool>>[] { () => CheckBoxChannelProfileDefault,             () => CheckBoxChannelProfileOff,            () => CheckBoxChannelProfileOn } },
                { LoggingChannel.GAME_NETWORK,          new Expression<Func<bool>>[] { () => CheckBoxChannelGameNetworkDefault,         () => CheckBoxChannelGameNetworkOff,        () => CheckBoxChannelGameNetworkOn } },
                { LoggingChannel.PAKFILE_SYSTEM,        new Expression<Func<bool>>[] { () => CheckBoxChannelPakfileSystemDefault,       () => CheckBoxChannelPakfileSystemOff,      () => CheckBoxChannelPakfileSystemOn } },
                { LoggingChannel.LOOT_MANAGER,          new Expression<Func<bool>>[] { () => CheckBoxChannelLootManagerDefault,         () => CheckBoxChannelLootManagerOff,        () => CheckBoxChannelLootManagerOn } },
                { LoggingChannel.GROUPING_SYSTEM,       new Expression<Func<bool>>[] { () => CheckBoxChannelGroupingSystemDefault,      () => CheckBoxChannelGroupingSystemOff,     () => CheckBoxChannelGroupingSystemOn } },
                { LoggingChannel.PROTOBUF_DUMPER,       new Expression<Func<bool>>[] { () => CheckBoxChannelProtobufDumperDefault,      () => CheckBoxChannelProtobufDumperOff,     () => CheckBoxChannelProtobufDumperOn } },
                { LoggingChannel.GAME_DATABASE,         new Expression<Func<bool>>[] { () => CheckBoxChannelGameDatabaseDefault,        () => CheckBoxChannelGameDatabaseOff,       () => CheckBoxChannelGameDatabaseOn } },
                { LoggingChannel.TRANSITION,            new Expression<Func<bool>>[] { () => CheckBoxChannelTransitionDefault,          () => CheckBoxChannelTransitionOff,         () => CheckBoxChannelTransitionOn } },
                { LoggingChannel.AI,                    new Expression<Func<bool>>[] { () => CheckBoxChannelAIDefault,                  () => CheckBoxChannelAIOff,                 () => CheckBoxChannelAIOn } },
                { LoggingChannel.INVENTORY,             new Expression<Func<bool>>[] { () => CheckBoxChannelInventoryDefault,           () => CheckBoxChannelInventoryOff,          () => CheckBoxChannelInventoryOn } },
                { LoggingChannel.MEMORY,                new Expression<Func<bool>>[] { () => CheckBoxChannelMemoryDefault,              () => CheckBoxChannelMemoryOff,             () => CheckBoxChannelMemoryOn } },
                { LoggingChannel.MISSIONS,              new Expression<Func<bool>>[] { () => CheckBoxChannelMissionsDefault,            () => CheckBoxChannelMissionsOff,           () => CheckBoxChannelMissionsOn } },
                { LoggingChannel.PATCHER,               new Expression<Func<bool>>[] { () => CheckBoxChannelPatcherDefault,             () => CheckBoxChannelPatcherOff,            () => CheckBoxChannelPatcherOn } },
                { LoggingChannel.GENERATION,            new Expression<Func<bool>>[] { () => CheckBoxChannelGenerationDefault,          () => CheckBoxChannelGenerationOff,         () => CheckBoxChannelGenerationOn } },
                { LoggingChannel.RESPAWN,               new Expression<Func<bool>>[] { () => CheckBoxChannelRespawnDefault,             () => CheckBoxChannelRespawnOff,            () => CheckBoxChannelRespawnOn } },
                { LoggingChannel.SAVELOAD,              new Expression<Func<bool>>[] { () => CheckBoxChannelSaveloadDefault,            () => CheckBoxChannelSaveloadOff,           () => CheckBoxChannelSaveloadOn } },
                { LoggingChannel.FRONTEND,              new Expression<Func<bool>>[] { () => CheckBoxChannelFrontendDefault,            () => CheckBoxChannelFrontendOff,           () => CheckBoxChannelFrontendOn } },
                { LoggingChannel.COMMUNITY,             new Expression<Func<bool>>[] { () => CheckBoxChannelCommunityDefault,           () => CheckBoxChannelCommunityOff,          () => CheckBoxChannelCommunityOn } },
                { LoggingChannel.ACHIEVEMENTS,          new Expression<Func<bool>>[] { () => CheckBoxChannelAchievementsDefault,        () => CheckBoxChannelAchievementsOff,       () => CheckBoxChannelAchievementsOn } },
                { LoggingChannel.METRICS_HTTP_UPLOAD,   new Expression<Func<bool>>[] { () => CheckBoxChannelMetricsHttpUploadDefault,   () => CheckBoxChannelMetricsHttpUploadOff,  () => CheckBoxChannelMetricsHttpUploadOn } },
                { LoggingChannel.CURRENCY_CONVERSION,   new Expression<Func<bool>>[] { () => CheckBoxChannelCurrencyConversionDefault,  () => CheckBoxChannelCurrencyConversionOff, () => CheckBoxChannelCurrencyConversionOn } },
                { LoggingChannel.MOBILE,                new Expression<Func<bool>>[] { () => CheckBoxChannelMobileDefault,              () => CheckBoxChannelMobileOff,             () => CheckBoxChannelMobileOn } },
                { LoggingChannel.UI,                    new Expression<Func<bool>>[] { () => CheckBoxChannelUIDefault,                  () => CheckBoxChannelUIOff,                 () => CheckBoxChannelUIOn } },
                { LoggingChannel.LEADERBOARD,           new Expression<Func<bool>>[] { () => CheckBoxChannelLeaderboardDefault,         () => CheckBoxChannelLeaderboardOff,        () => CheckBoxChannelLeaderboardOn } }
            };
        }

        private void SetLoggingChannelState(LoggingChannel channel, LoggingChannelState state)
        {
            _loggingChannelStateDict[channel] = state;

            foreach (var checkbox in _loggingChannelCheckboxDict[channel])
                NotifyOfPropertyChange(checkbox);
        }


        #region CheckBox Properties

        public bool EnableLogging
        {
            get { return _enableLogging; }
            set { _enableLogging = value; NotifyOfPropertyChange(() => EnableLogging); }
        }

        public bool OverrideLoggingLevel
        {
            get { return _overrideLoggingLevel; }
            set { _overrideLoggingLevel = value; NotifyOfPropertyChange(() => OverrideLoggingLevel); }
        }

        public LoggingLevelModel SelectedLoggingLevel
        {
            get { return _selectedLoggingLevel; }
            set { _selectedLoggingLevel = value; NotifyOfPropertyChange(() => SelectedLoggingLevel); }
        }


        public bool CheckBoxChannelAllDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.ALL] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.ALL, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelAllOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.ALL] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.ALL, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelAllOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.ALL] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.ALL, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelErrorDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.ERROR] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.ERROR, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelErrorOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.ERROR] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.ERROR, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelErrorOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.ERROR] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.ERROR, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelCoreDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.CORE] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CORE, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelCoreOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.CORE] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CORE, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelCoreOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.CORE] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CORE, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelCoreNetDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.CORE_NET] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CORE_NET, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelCoreNetOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.CORE_NET] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CORE_NET, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelCoreNetOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.CORE_NET] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CORE_NET, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelCoreJobsTPDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.CORE_JOBS_TP] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CORE_JOBS_TP, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelCoreJobsTPOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.CORE_JOBS_TP] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CORE_JOBS_TP, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelCoreJobsTPOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.CORE_JOBS_TP] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CORE_JOBS_TP, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelGameDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.GAME] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GAME, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelGameOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.GAME] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GAME, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelGameOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.GAME] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GAME, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelPeerConnectorDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.PEER_CONNECTOR] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PEER_CONNECTOR, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelPeerConnectorOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.PEER_CONNECTOR] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PEER_CONNECTOR, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelPeerConnectorOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.PEER_CONNECTOR] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PEER_CONNECTOR, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelDatastoreDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.DATASTORE] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.DATASTORE, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelDatastoreOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.DATASTORE] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.DATASTORE, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelDatastoreOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.DATASTORE] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.DATASTORE, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelProfileDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.PROFILE] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PROFILE, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelProfileOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.PROFILE] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PROFILE, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelProfileOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.PROFILE] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PROFILE, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelGameNetworkDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.GAME_NETWORK] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GAME_NETWORK, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelGameNetworkOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.GAME_NETWORK] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GAME_NETWORK, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelGameNetworkOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.GAME_NETWORK] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GAME_NETWORK, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelPakfileSystemDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.PAKFILE_SYSTEM] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PAKFILE_SYSTEM, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelPakfileSystemOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.PAKFILE_SYSTEM] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PAKFILE_SYSTEM, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelPakfileSystemOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.PAKFILE_SYSTEM] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PAKFILE_SYSTEM, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelLootManagerDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.LOOT_MANAGER] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.LOOT_MANAGER, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelLootManagerOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.LOOT_MANAGER] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.LOOT_MANAGER, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelLootManagerOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.LOOT_MANAGER] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.LOOT_MANAGER, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelGroupingSystemDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.GROUPING_SYSTEM] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GROUPING_SYSTEM, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelGroupingSystemOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.GROUPING_SYSTEM] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GROUPING_SYSTEM, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelGroupingSystemOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.GROUPING_SYSTEM] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GROUPING_SYSTEM, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelProtobufDumperDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.PROTOBUF_DUMPER] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PROTOBUF_DUMPER, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelProtobufDumperOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.PROTOBUF_DUMPER] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PROTOBUF_DUMPER, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelProtobufDumperOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.PROTOBUF_DUMPER] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PROTOBUF_DUMPER, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelGameDatabaseDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.GAME_DATABASE] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GAME_DATABASE, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelGameDatabaseOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.GAME_DATABASE] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GAME_DATABASE, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelGameDatabaseOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.GAME_DATABASE] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GAME_DATABASE, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelTransitionDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.TRANSITION] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.TRANSITION, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelTransitionOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.TRANSITION] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.TRANSITION, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelTransitionOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.TRANSITION] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.TRANSITION, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelAIDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.AI] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.AI, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelAIOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.AI] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.AI, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelAIOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.AI] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.AI, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelInventoryDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.INVENTORY] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.INVENTORY, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelInventoryOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.INVENTORY] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.INVENTORY, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelInventoryOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.INVENTORY] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.INVENTORY, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelMemoryDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.MEMORY] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.MEMORY, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelMemoryOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.MEMORY] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.MEMORY, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelMemoryOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.MEMORY] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.MEMORY, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelMissionsDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.MISSIONS] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.MISSIONS, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelMissionsOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.MISSIONS] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.MISSIONS, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelMissionsOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.MISSIONS] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.MISSIONS, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelPatcherDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.PATCHER] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PATCHER, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelPatcherOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.PATCHER] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PATCHER, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelPatcherOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.PATCHER] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.PATCHER, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelGenerationDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.GENERATION] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GENERATION, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelGenerationOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.GENERATION] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GENERATION, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelGenerationOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.GENERATION] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.GENERATION, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelRespawnDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.RESPAWN] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.RESPAWN, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelRespawnOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.RESPAWN] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.RESPAWN, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelRespawnOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.RESPAWN] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.RESPAWN, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelSaveloadDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.SAVELOAD] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.SAVELOAD, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelSaveloadOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.SAVELOAD] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.SAVELOAD, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelSaveloadOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.SAVELOAD] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.SAVELOAD, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelFrontendDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.FRONTEND] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.FRONTEND, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelFrontendOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.FRONTEND] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.FRONTEND, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelFrontendOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.FRONTEND] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.FRONTEND, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelCommunityDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.COMMUNITY] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.COMMUNITY, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelCommunityOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.COMMUNITY] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.COMMUNITY, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelCommunityOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.COMMUNITY] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.COMMUNITY, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelAchievementsDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.ACHIEVEMENTS] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.ACHIEVEMENTS, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelAchievementsOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.ACHIEVEMENTS] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.ACHIEVEMENTS, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelAchievementsOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.ACHIEVEMENTS] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.ACHIEVEMENTS, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelMetricsHttpUploadDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.METRICS_HTTP_UPLOAD] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.METRICS_HTTP_UPLOAD, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelMetricsHttpUploadOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.METRICS_HTTP_UPLOAD] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.METRICS_HTTP_UPLOAD, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelMetricsHttpUploadOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.METRICS_HTTP_UPLOAD] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.METRICS_HTTP_UPLOAD, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelCurrencyConversionDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.CURRENCY_CONVERSION] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CURRENCY_CONVERSION, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelCurrencyConversionOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.CURRENCY_CONVERSION] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CURRENCY_CONVERSION, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelCurrencyConversionOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.CURRENCY_CONVERSION] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.CURRENCY_CONVERSION, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelMobileDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.MOBILE] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.MOBILE, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelMobileOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.MOBILE] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.MOBILE, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelMobileOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.MOBILE] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.MOBILE, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelUIDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.UI] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.UI, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelUIOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.UI] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.UI, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelUIOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.UI] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.UI, LoggingChannelState.On); }
        }


        public bool CheckBoxChannelLeaderboardDefault
        {
            get { return _loggingChannelStateDict[LoggingChannel.LEADERBOARD] == LoggingChannelState.Default; }
            set { if (value) SetLoggingChannelState(LoggingChannel.LEADERBOARD, LoggingChannelState.Default); }
        }

        public bool CheckBoxChannelLeaderboardOff
        {
            get { return _loggingChannelStateDict[LoggingChannel.LEADERBOARD] == LoggingChannelState.Off; }
            set { if (value) SetLoggingChannelState(LoggingChannel.LEADERBOARD, LoggingChannelState.Off); }
        }

        public bool CheckBoxChannelLeaderboardOn
        {
            get { return _loggingChannelStateDict[LoggingChannel.LEADERBOARD] == LoggingChannelState.On; }
            set { if (value) SetLoggingChannelState(LoggingChannel.LEADERBOARD, LoggingChannelState.On); }
        }

        #endregion
    }
}
