namespace Bifrost.Core
{
    public enum Downloader
    {
        Robocopy,
        BitRaiderOrSolidState,     // SolidState replaced BitRaider in 1.11.0.281
        Steam
    }

    public enum LoggingLevel
    {
        NONE,
        CRITICAL,
        FATAL,
        ERROR,
        WARNING,
        INFORMATION,
        VERBOSE,
        EXTRA_VERBOSE,
        DEBUG
    }

    public enum LoggingChannel
    {
        ALL,
        ERROR,
        CORE,
        CORE_NET,
        CORE_JOBS_TP,
        GAME,
        PEER_CONNECTOR,
        DATASTORE,
        PROFILE,
        GAME_NETWORK,
        PAKFILE_SYSTEM,
        LOOT_MANAGER,
        GROUPING_SYSTEM,
        PROTOBUF_DUMPER,
        GAME_DATABASE,
        TRANSITION,
        AI,
        INVENTORY,
        MEMORY,
        MISSIONS,
        PATCHER,
        GENERATION,
        RESPAWN,
        SAVELOAD,
        FRONTEND,
        COMMUNITY,
        ACHIEVEMENTS,
        METRICS_HTTP_UPLOAD,
        CURRENCY_CONVERSION,
        MOBILE,
        UI,
        LEADERBOARD
    }

    public enum LoggingChannelState
    {
        Default = -1,
        Off = 0,
        On = 1
    }
}
