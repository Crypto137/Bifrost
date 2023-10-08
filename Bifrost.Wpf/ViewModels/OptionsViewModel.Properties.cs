using Bifrost.Launcher;
using Bifrost.Wpf.Models;

namespace Bifrost.Wpf.ViewModels
{
    public partial class OptionsViewModel
    {
        // ALL WHO ENTER: BEWARE

        public bool OverrideLoggingLevel
        {
            get => _overrideLoggingLevel;
            set
            {
                _overrideLoggingLevel = value;
                NotifyOfPropertyChange(() => OverrideLoggingLevel);
            }
        }

        public LoggingLevelModel SelectedLoggingLevel
        {
            get => _selectedLoggingLevel;
            set
            {
                _selectedLoggingLevel = value;
                NotifyOfPropertyChange(() => SelectedLoggingLevel);
            }
        }

        public bool Force32Bit
        {
            get => _force32Bit;
            set
            {
                _force32Bit = value;
                NotifyOfPropertyChange(() => Force32Bit);
            }
        }

        public DownloaderModel SelectedDownloader
        {
            get => _selectedDownloader;
            set
            {
                _selectedDownloader = value;
                NotifyOfPropertyChange(() => SelectedDownloader);
            }
        }

        public bool NoAccount
        {
            get => _noAccount;
            set
            {
                _noAccount = value;
                NotifyOfPropertyChange(() => NoAccount);
            }
        }

        public bool NoOptions
        {
            get => _noOptions;
            set
            {
                _noOptions = value;
                NotifyOfPropertyChange(() => NoOptions);
            }
        }

        public bool NoStore
        {
            get => _noStore;
            set
            {
                _noStore = value;
                NotifyOfPropertyChange(() => NoStore);
            }
        }

        public bool NoCatalog
        {
            get => _noCatalog;
            set
            {
                _noCatalog = value;
                NotifyOfPropertyChange(() => NoCatalog);
            }
        }

        public bool NoNews
        {
            get => _noNews;
            set
            {
                _noNews = value;
                NotifyOfPropertyChange(() => NoNews);
            }
        }

        public bool NoLogout
        {
            get => _noLogout;
            set
            {
                _noLogout = value;
                NotifyOfPropertyChange(() => NoLogout);
            }
        }


        #region Channel checkbox hell

        public bool CheckBoxChannelAllDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.ALL] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.ALL] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelAllDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelAllOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelAllOn);
                }
            }
        }

        public bool CheckBoxChannelAllOff
        {
            get => _loggingChannelStateDict[LoggingChannel.ALL] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.ALL] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelAllDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelAllOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelAllOn);
                }
            }
        }

        public bool CheckBoxChannelAllOn
        {
            get => _loggingChannelStateDict[LoggingChannel.ALL] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.ALL] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelAllDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelAllOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelAllOn);
                }
            }
        }


        public bool CheckBoxChannelErrorDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.ERROR] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.ERROR] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelErrorDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelErrorOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelErrorOn);
                }
            }
        }

        public bool CheckBoxChannelErrorOff
        {
            get => _loggingChannelStateDict[LoggingChannel.ERROR] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.ERROR] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelErrorDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelErrorOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelErrorOn);
                }
            }
        }

        public bool CheckBoxChannelErrorOn
        {
            get => _loggingChannelStateDict[LoggingChannel.ERROR] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.ERROR] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelErrorDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelErrorOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelErrorOn);
                }
            }
        }


        public bool CheckBoxChannelCoreDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.CORE] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CORE] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreOn);
                }
            }
        }

        public bool CheckBoxChannelCoreOff
        {
            get => _loggingChannelStateDict[LoggingChannel.CORE] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CORE] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreOn);
                }
            }
        }

        public bool CheckBoxChannelCoreOn
        {
            get => _loggingChannelStateDict[LoggingChannel.CORE] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CORE] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreOn);
                }
            }
        }


        public bool CheckBoxChannelCoreNetDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.CORE_NET] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CORE_NET] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreNetDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreNetOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreNetOn);
                }
            }
        }

        public bool CheckBoxChannelCoreNetOff
        {
            get => _loggingChannelStateDict[LoggingChannel.CORE_NET] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CORE_NET] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreNetDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreNetOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreNetOn);
                }
            }
        }

        public bool CheckBoxChannelCoreNetOn
        {
            get => _loggingChannelStateDict[LoggingChannel.CORE_NET] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CORE_NET] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreNetDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreNetOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreNetOn);
                }
            }
        }


        public bool CheckBoxChannelCoreJobsTPDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.CORE_JOBS_TP] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CORE_JOBS_TP] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreJobsTPDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreJobsTPOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreJobsTPOn);
                }
            }
        }

        public bool CheckBoxChannelCoreJobsTPOff
        {
            get => _loggingChannelStateDict[LoggingChannel.CORE_JOBS_TP] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CORE_JOBS_TP] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreJobsTPDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreJobsTPOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreJobsTPOn);
                }
            }
        }

        public bool CheckBoxChannelCoreJobsTPOn
        {
            get => _loggingChannelStateDict[LoggingChannel.CORE_JOBS_TP] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CORE_JOBS_TP] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreJobsTPDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreJobsTPOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCoreJobsTPOn);
                }
            }
        }


        public bool CheckBoxChannelGameDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.GAME] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GAME] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameOn);
                }
            }
        }

        public bool CheckBoxChannelGameOff
        {
            get => _loggingChannelStateDict[LoggingChannel.GAME] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GAME] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameOn);
                }
            }
        }

        public bool CheckBoxChannelGameOn
        {
            get => _loggingChannelStateDict[LoggingChannel.GAME] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GAME] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameOn);
                }
            }
        }


        public bool CheckBoxChannelPeerConnectorDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.PEER_CONNECTOR] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PEER_CONNECTOR] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelPeerConnectorDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelPeerConnectorOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelPeerConnectorOn);
                }
            }
        }

        public bool CheckBoxChannelPeerConnectorOff
        {
            get => _loggingChannelStateDict[LoggingChannel.PEER_CONNECTOR] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PEER_CONNECTOR] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelPeerConnectorDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelPeerConnectorOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelPeerConnectorOn);
                }
            }
        }

        public bool CheckBoxChannelPeerConnectorOn
        {
            get => _loggingChannelStateDict[LoggingChannel.PEER_CONNECTOR] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PEER_CONNECTOR] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelPeerConnectorDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelPeerConnectorOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelPeerConnectorOn);
                }
            }
        }


        public bool CheckBoxChannelDatastoreDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.DATASTORE] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.DATASTORE] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelDatastoreDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelDatastoreOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelDatastoreOn);
                }
            }
        }

        public bool CheckBoxChannelDatastoreOff
        {
            get => _loggingChannelStateDict[LoggingChannel.DATASTORE] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.DATASTORE] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelDatastoreDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelDatastoreOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelDatastoreOn);
                }
            }
        }

        public bool CheckBoxChannelDatastoreOn
        {
            get => _loggingChannelStateDict[LoggingChannel.DATASTORE] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.DATASTORE] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelDatastoreDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelDatastoreOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelDatastoreOn);
                }
            }
        }


        public bool CheckBoxChannelProfileDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.PROFILE] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PROFILE] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelProfileDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelProfileOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelProfileOn);
                }
            }
        }

        public bool CheckBoxChannelProfileOff
        {
            get => _loggingChannelStateDict[LoggingChannel.PROFILE] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PROFILE] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelProfileDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelProfileOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelProfileOn);
                }
            }
        }

        public bool CheckBoxChannelProfileOn
        {
            get => _loggingChannelStateDict[LoggingChannel.PROFILE] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PROFILE] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelProfileDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelProfileOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelProfileOn);
                }
            }
        }


        public bool CheckBoxChannelGameNetworkDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.GAME_NETWORK] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GAME_NETWORK] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelGameNetworkDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameNetworkOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameNetworkOn);
                }
            }
        }

        public bool CheckBoxChannelGameNetworkOff
        {
            get => _loggingChannelStateDict[LoggingChannel.GAME_NETWORK] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GAME_NETWORK] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelGameNetworkDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameNetworkOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameNetworkOn);
                }
            }
        }

        public bool CheckBoxChannelGameNetworkOn
        {
            get => _loggingChannelStateDict[LoggingChannel.GAME_NETWORK] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GAME_NETWORK] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelGameNetworkDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameNetworkOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameNetworkOn);
                }
            }
        }


        public bool CheckBoxChannelPakfileSystemDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.PAKFILE_SYSTEM] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PAKFILE_SYSTEM] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelPakfileSystemDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelPakfileSystemOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelPakfileSystemOn);
                }
            }
        }

        public bool CheckBoxChannelPakfileSystemOff
        {
            get => _loggingChannelStateDict[LoggingChannel.PAKFILE_SYSTEM] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PAKFILE_SYSTEM] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelPakfileSystemDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelPakfileSystemOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelPakfileSystemOn);
                }
            }
        }

        public bool CheckBoxChannelPakfileSystemOn
        {
            get => _loggingChannelStateDict[LoggingChannel.PAKFILE_SYSTEM] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PAKFILE_SYSTEM] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelPakfileSystemDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelPakfileSystemOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelPakfileSystemOn);
                }
            }
        }


        public bool CheckBoxChannelLootManagerDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.LOOT_MANAGER] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.LOOT_MANAGER] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelLootManagerDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelLootManagerOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelLootManagerOn);
                }
            }
        }

        public bool CheckBoxChannelLootManagerOff
        {
            get => _loggingChannelStateDict[LoggingChannel.LOOT_MANAGER] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.LOOT_MANAGER] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelLootManagerDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelLootManagerOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelLootManagerOn);
                }
            }
        }

        public bool CheckBoxChannelLootManagerOn
        {
            get => _loggingChannelStateDict[LoggingChannel.LOOT_MANAGER] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.LOOT_MANAGER] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelLootManagerDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelLootManagerOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelLootManagerOn);
                }
            }
        }


        public bool CheckBoxChannelGroupingSystemDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.GROUPING_SYSTEM] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GROUPING_SYSTEM] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelGroupingSystemDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGroupingSystemOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGroupingSystemOn);
                }
            }
        }

        public bool CheckBoxChannelGroupingSystemOff
        {
            get => _loggingChannelStateDict[LoggingChannel.GROUPING_SYSTEM] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GROUPING_SYSTEM] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelGroupingSystemDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGroupingSystemOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGroupingSystemOn);
                }
            }
        }

        public bool CheckBoxChannelGroupingSystemOn
        {
            get => _loggingChannelStateDict[LoggingChannel.GROUPING_SYSTEM] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GROUPING_SYSTEM] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelGroupingSystemDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGroupingSystemOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGroupingSystemOn);
                }
            }
        }


        public bool CheckBoxChannelProtobufDumperDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.PROTOBUF_DUMPER] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PROTOBUF_DUMPER] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelProtobufDumperDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelProtobufDumperOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelProtobufDumperOn);
                }
            }
        }

        public bool CheckBoxChannelProtobufDumperOff
        {
            get => _loggingChannelStateDict[LoggingChannel.PROTOBUF_DUMPER] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PROTOBUF_DUMPER] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelProtobufDumperDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelProtobufDumperOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelProtobufDumperOn);
                }
            }
        }

        public bool CheckBoxChannelProtobufDumperOn
        {
            get => _loggingChannelStateDict[LoggingChannel.PROTOBUF_DUMPER] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PROTOBUF_DUMPER] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelProtobufDumperDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelProtobufDumperOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelProtobufDumperOn);
                }
            }
        }


        public bool CheckBoxChannelGameDatabaseDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.GAME_DATABASE] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GAME_DATABASE] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDatabaseDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDatabaseOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDatabaseOn);
                }
            }
        }

        public bool CheckBoxChannelGameDatabaseOff
        {
            get => _loggingChannelStateDict[LoggingChannel.GAME_DATABASE] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GAME_DATABASE] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDatabaseDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDatabaseOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDatabaseOn);
                }
            }
        }

        public bool CheckBoxChannelGameDatabaseOn
        {
            get => _loggingChannelStateDict[LoggingChannel.GAME_DATABASE] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GAME_DATABASE] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDatabaseDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDatabaseOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGameDatabaseOn);
                }
            }
        }


        public bool CheckBoxChannelTransitionDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.TRANSITION] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.TRANSITION] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelTransitionDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelTransitionOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelTransitionOn);
                }
            }
        }

        public bool CheckBoxChannelTransitionOff
        {
            get => _loggingChannelStateDict[LoggingChannel.TRANSITION] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.TRANSITION] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelTransitionDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelTransitionOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelTransitionOn);
                }
            }
        }

        public bool CheckBoxChannelTransitionOn
        {
            get => _loggingChannelStateDict[LoggingChannel.TRANSITION] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.TRANSITION] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelTransitionDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelTransitionOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelTransitionOn);
                }
            }
        }


        public bool CheckBoxChannelAIDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.AI] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.AI] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelAIDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelAIOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelAIOn);
                }
            }
        }

        public bool CheckBoxChannelAIOff
        {
            get => _loggingChannelStateDict[LoggingChannel.AI] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.AI] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelAIDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelAIOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelAIOn);
                }
            }
        }

        public bool CheckBoxChannelAIOn
        {
            get => _loggingChannelStateDict[LoggingChannel.AI] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.AI] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelAIDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelAIOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelAIOn);
                }
            }
        }


        public bool CheckBoxChannelInventoryDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.INVENTORY] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.INVENTORY] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelInventoryDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelInventoryOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelInventoryOn);
                }
            }
        }

        public bool CheckBoxChannelInventoryOff
        {
            get => _loggingChannelStateDict[LoggingChannel.INVENTORY] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.INVENTORY] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelInventoryDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelInventoryOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelInventoryOn);
                }
            }
        }

        public bool CheckBoxChannelInventoryOn
        {
            get => _loggingChannelStateDict[LoggingChannel.INVENTORY] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.INVENTORY] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelInventoryDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelInventoryOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelInventoryOn);
                }
            }
        }


        public bool CheckBoxChannelMemoryDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.MEMORY] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.MEMORY] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelMemoryDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMemoryOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMemoryOn);
                }
            }
        }

        public bool CheckBoxChannelMemoryOff
        {
            get => _loggingChannelStateDict[LoggingChannel.MEMORY] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.MEMORY] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelMemoryDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMemoryOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMemoryOn);
                }
            }
        }

        public bool CheckBoxChannelMemoryOn
        {
            get => _loggingChannelStateDict[LoggingChannel.MEMORY] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.MEMORY] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelMemoryDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMemoryOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMemoryOn);
                }
            }
        }


        public bool CheckBoxChannelMissionsDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.MISSIONS] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.MISSIONS] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelMissionsDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMissionsOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMissionsOn);
                }
            }
        }

        public bool CheckBoxChannelMissionsOff
        {
            get => _loggingChannelStateDict[LoggingChannel.MISSIONS] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.MISSIONS] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelMissionsDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMissionsOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMissionsOn);
                }
            }
        }

        public bool CheckBoxChannelMissionsOn
        {
            get => _loggingChannelStateDict[LoggingChannel.MISSIONS] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.MISSIONS] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelMissionsDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMissionsOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMissionsOn);
                }
            }
        }


        public bool CheckBoxChannelPatcherDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.PATCHER] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PATCHER] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelPatcherDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelPatcherOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelPatcherOn);
                }
            }
        }

        public bool CheckBoxChannelPatcherOff
        {
            get => _loggingChannelStateDict[LoggingChannel.PATCHER] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PATCHER] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelPatcherDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelPatcherOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelPatcherOn);
                }
            }
        }

        public bool CheckBoxChannelPatcherOn
        {
            get => _loggingChannelStateDict[LoggingChannel.PATCHER] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.PATCHER] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelPatcherDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelPatcherOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelPatcherOn);
                }
            }
        }


        public bool CheckBoxChannelGenerationDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.GENERATION] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GENERATION] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelGenerationDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGenerationOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGenerationOn);
                }
            }
        }

        public bool CheckBoxChannelGenerationOff
        {
            get => _loggingChannelStateDict[LoggingChannel.GENERATION] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GENERATION] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelGenerationDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGenerationOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGenerationOn);
                }
            }
        }

        public bool CheckBoxChannelGenerationOn
        {
            get => _loggingChannelStateDict[LoggingChannel.GENERATION] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.GENERATION] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelGenerationDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelGenerationOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelGenerationOn);
                }
            }
        }


        public bool CheckBoxChannelRespawnDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.RESPAWN] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.RESPAWN] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelRespawnDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelRespawnOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelRespawnOn);
                }
            }
        }

        public bool CheckBoxChannelRespawnOff
        {
            get => _loggingChannelStateDict[LoggingChannel.RESPAWN] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.RESPAWN] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelRespawnDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelRespawnOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelRespawnOn);
                }
            }
        }

        public bool CheckBoxChannelRespawnOn
        {
            get => _loggingChannelStateDict[LoggingChannel.RESPAWN] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.RESPAWN] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelRespawnDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelRespawnOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelRespawnOn);
                }
            }
        }


        public bool CheckBoxChannelSaveloadDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.SAVELOAD] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.SAVELOAD] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelSaveloadDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelSaveloadOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelSaveloadOn);
                }
            }
        }

        public bool CheckBoxChannelSaveloadOff
        {
            get => _loggingChannelStateDict[LoggingChannel.SAVELOAD] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.SAVELOAD] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelSaveloadDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelSaveloadOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelSaveloadOn);
                }
            }
        }

        public bool CheckBoxChannelSaveloadOn
        {
            get => _loggingChannelStateDict[LoggingChannel.SAVELOAD] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.SAVELOAD] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelSaveloadDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelSaveloadOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelSaveloadOn);
                }
            }
        }


        public bool CheckBoxChannelFrontendDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.FRONTEND] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.FRONTEND] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelFrontendDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelFrontendOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelFrontendOn);
                }
            }
        }

        public bool CheckBoxChannelFrontendOff
        {
            get => _loggingChannelStateDict[LoggingChannel.FRONTEND] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.FRONTEND] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelFrontendDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelFrontendOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelFrontendOn);
                }
            }
        }

        public bool CheckBoxChannelFrontendOn
        {
            get => _loggingChannelStateDict[LoggingChannel.FRONTEND] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.FRONTEND] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelFrontendDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelFrontendOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelFrontendOn);
                }
            }
        }


        public bool CheckBoxChannelCommunityDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.COMMUNITY] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.COMMUNITY] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelCommunityDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCommunityOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCommunityOn);
                }
            }
        }

        public bool CheckBoxChannelCommunityOff
        {
            get => _loggingChannelStateDict[LoggingChannel.COMMUNITY] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.COMMUNITY] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelCommunityDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCommunityOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCommunityOn);
                }
            }
        }

        public bool CheckBoxChannelCommunityOn
        {
            get => _loggingChannelStateDict[LoggingChannel.COMMUNITY] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.COMMUNITY] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelCommunityDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCommunityOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCommunityOn);
                }
            }
        }


        public bool CheckBoxChannelAchievementsDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.ACHIEVEMENTS] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.ACHIEVEMENTS] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelAchievementsDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelAchievementsOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelAchievementsOn);
                }
            }
        }

        public bool CheckBoxChannelAchievementsOff
        {
            get => _loggingChannelStateDict[LoggingChannel.ACHIEVEMENTS] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.ACHIEVEMENTS] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelAchievementsDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelAchievementsOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelAchievementsOn);
                }
            }
        }

        public bool CheckBoxChannelAchievementsOn
        {
            get => _loggingChannelStateDict[LoggingChannel.ACHIEVEMENTS] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.ACHIEVEMENTS] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelAchievementsDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelAchievementsOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelAchievementsOn);
                }
            }
        }


        public bool CheckBoxChannelMetricsHttpUploadDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.METRICS_HTTP_UPLOAD] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.METRICS_HTTP_UPLOAD] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelMetricsHttpUploadDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMetricsHttpUploadOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMetricsHttpUploadOn);
                }
            }
        }

        public bool CheckBoxChannelMetricsHttpUploadOff
        {
            get => _loggingChannelStateDict[LoggingChannel.METRICS_HTTP_UPLOAD] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.METRICS_HTTP_UPLOAD] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelMetricsHttpUploadDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMetricsHttpUploadOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMetricsHttpUploadOn);
                }
            }
        }

        public bool CheckBoxChannelMetricsHttpUploadOn
        {
            get => _loggingChannelStateDict[LoggingChannel.METRICS_HTTP_UPLOAD] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.METRICS_HTTP_UPLOAD] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelMetricsHttpUploadDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMetricsHttpUploadOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMetricsHttpUploadOn);
                }
            }
        }


        public bool CheckBoxChannelCurrencyConversionDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.CURRENCY_CONVERSION] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CURRENCY_CONVERSION] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelCurrencyConversionDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCurrencyConversionOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCurrencyConversionOn);
                }
            }
        }

        public bool CheckBoxChannelCurrencyConversionOff
        {
            get => _loggingChannelStateDict[LoggingChannel.CURRENCY_CONVERSION] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CURRENCY_CONVERSION] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelCurrencyConversionDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCurrencyConversionOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCurrencyConversionOn);
                }
            }
        }

        public bool CheckBoxChannelCurrencyConversionOn
        {
            get => _loggingChannelStateDict[LoggingChannel.CURRENCY_CONVERSION] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.CURRENCY_CONVERSION] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelCurrencyConversionDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelCurrencyConversionOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelCurrencyConversionOn);
                }
            }
        }


        public bool CheckBoxChannelMobileDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.MOBILE] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.MOBILE] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelMobileDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMobileOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMobileOn);
                }
            }
        }

        public bool CheckBoxChannelMobileOff
        {
            get => _loggingChannelStateDict[LoggingChannel.MOBILE] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.MOBILE] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelMobileDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMobileOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMobileOn);
                }
            }
        }

        public bool CheckBoxChannelMobileOn
        {
            get => _loggingChannelStateDict[LoggingChannel.MOBILE] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.MOBILE] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelMobileDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelMobileOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelMobileOn);
                }
            }
        }


        public bool CheckBoxChannelUIDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.UI] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.UI] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelUIDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelUIOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelUIOn);
                }
            }
        }

        public bool CheckBoxChannelUIOff
        {
            get => _loggingChannelStateDict[LoggingChannel.UI] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.UI] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelUIDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelUIOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelUIOn);
                }
            }
        }

        public bool CheckBoxChannelUIOn
        {
            get => _loggingChannelStateDict[LoggingChannel.UI] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.UI] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelUIDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelUIOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelUIOn);
                }
            }
        }


        public bool CheckBoxChannelLeaderboardDefault
        {
            get => _loggingChannelStateDict[LoggingChannel.LEADERBOARD] == LoggingChannelState.Default;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.LEADERBOARD] = LoggingChannelState.Default;
                    NotifyOfPropertyChange(() => CheckBoxChannelLeaderboardDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelLeaderboardOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelLeaderboardOn);
                }
            }
        }

        public bool CheckBoxChannelLeaderboardOff
        {
            get => _loggingChannelStateDict[LoggingChannel.LEADERBOARD] == LoggingChannelState.Off;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.LEADERBOARD] = LoggingChannelState.Off;
                    NotifyOfPropertyChange(() => CheckBoxChannelLeaderboardDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelLeaderboardOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelLeaderboardOn);
                }
            }
        }

        public bool CheckBoxChannelLeaderboardOn
        {
            get => _loggingChannelStateDict[LoggingChannel.LEADERBOARD] == LoggingChannelState.On;
            set
            {
                if (value)
                {
                    _loggingChannelStateDict[LoggingChannel.LEADERBOARD] = LoggingChannelState.On;
                    NotifyOfPropertyChange(() => CheckBoxChannelLeaderboardDefault);
                    NotifyOfPropertyChange(() => CheckBoxChannelLeaderboardOff);
                    NotifyOfPropertyChange(() => CheckBoxChannelLeaderboardOn);
                }
            }
        }

        #endregion
    }
}
