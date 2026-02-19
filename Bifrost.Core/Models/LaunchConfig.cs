using Bifrost.Core.ClientManagement;
using System.Text;

namespace Bifrost.Core.Models
{
    public class LaunchConfig
    {
        public int ServerIndex { get; set; }

        // General
        public bool NoStartupMovies { get; set; } = false;
        public bool NoSplash { get; set; } = false;
        public bool ForceCustomResolution { get; set; } = false;
        public int CustomResolutionX { get; set; } = 1920;
        public int CustomResolutionY { get; set; } = 1080;
        
        public bool EnableAutoLogin { get; set; } = false;
        public string AutoLoginEmailAddress { get; set; } = "test1@test.com";
        public string AutoLoginPassword { get; set; } = "123";

        public string CustomArguments { get; set; } = string.Empty;

        // Logging
        public bool EnableLogging { get; set; } = false;
        public bool OverrideLoggingLevel { get; set; } = false;
        public LoggingLevel LoggingLevel { get; set; } = LoggingLevel.NONE;
        public Dictionary<LoggingChannel, LoggingChannelState> LoggingChannelStateDict { get; set; } = new();

        // Advanced
        public Downloader Downloader { get; set; } = Downloader.Robocopy;
        public bool Force32Bit { get; set; } = false;

        public bool NoSound { get; set; } = false;
        public bool NoAccount { get; set; } = false;
        public bool NoOptions { get; set; } = false;
        public bool NoStore { get; set; } = false;
        public bool NoCatalog { get; set; } = false;
        public bool NoNews { get; set; } = false;
        public bool NoLogout { get; set; } = false;

        public LaunchConfig()
        {
            // Initialize logging channels
            foreach (LoggingChannel channel in Enum.GetValues<LoggingChannel>())
                LoggingChannelStateDict.Add(channel, LoggingChannelState.Default);
        }

        public string[] ToLaunchArguments(ServerInfo serverInfo, ClientFlags flags)
        {
            List<string> argumentList = new();

            if (flags.HasFlag(ClientFlags.BitRaider))
            {
                // Treat Solid State as BitRaider for older versions of the game. Also do not use -robocopy, since it didn't exist back then.
                switch (Downloader)
                {
                    case Downloader.Robocopy:
                        argumentList.AddRange(["-nobitraider", "-nosteam"]);
                        break;

                    case Downloader.BitRaiderOrSolidState:
                        argumentList.AddRange(["-bitraider", "-nosteam"]);
                        break;

                    case Downloader.Steam:
                        argumentList.AddRange(["-steam", "-nobitraider"]);
                        break;
                }
            }
            else
            {
                switch (Downloader)
                {
                    case Downloader.Robocopy:
                        argumentList.AddRange(["-robocopy", "-nosolidstate", "-nosteam"]);
                        break;

                    case Downloader.BitRaiderOrSolidState:
                        argumentList.AddRange(["-solidstate", "-nosteam"]);
                        break;

                    case Downloader.Steam:
                        argumentList.AddRange(["-steam", "-nosolidstate"]);
                        break;
                }
            }

            if (serverInfo != null) argumentList.Add($"-siteconfigurl={serverInfo.SiteConfigUrl}");

            // General
            if (NoStartupMovies) argumentList.Add("-nostartupmovies");
            if (NoSplash) argumentList.Add("-nosplash");

            if (ForceCustomResolution)
            {
                argumentList.Add($"-ResX={CustomResolutionX}");
                argumentList.Add($"-ResY={CustomResolutionY}");
            }

            if (EnableAutoLogin)
            {
                argumentList.Add($"-emailaddress={AutoLoginEmailAddress}");
                argumentList.Add($"-password={AutoLoginPassword}");
            }

            if (string.IsNullOrWhiteSpace(CustomArguments) == false)
                argumentList.AddRange(CustomArguments.Split(' '));

            // Logging
            if (EnableLogging)
            {
                argumentList.Add("-log");

                if (OverrideLoggingLevel)
                    argumentList.Add($"-LoggingLevel={LoggingLevel}");

                string loggingChannelFilter = BuildLoggingChannelFilter();
                if (loggingChannelFilter.Length > 0)
                    argumentList.Add($"-LoggingChannels={loggingChannelFilter}");
            }

            // Advanced
            if (NoSound) argumentList.Add("-nosound");
            if (NoAccount) argumentList.Add("-noaccount");
            if (NoOptions) argumentList.Add("-nooptions");
            if (NoStore) argumentList.Add("-nostore");
            if (NoCatalog) argumentList.Add("-nocatalog");
            if (NoNews) argumentList.Add("-nonews");
            if (NoLogout) argumentList.Add("-nologout");

            return argumentList.ToArray();
        }

        private string BuildLoggingChannelFilter()
        {
            StringBuilder sb = new();

            foreach (var kvp in LoggingChannelStateDict)
            {
                if (kvp.Value > LoggingChannelState.Default)
                {
                    sb.Append(kvp.Value > 0 ? '+' : '-');
                    sb.Append(kvp.Key.ToString());
                    sb.Append(',');
                }
            }

            if (sb.Length > 0) sb.Length--;     // Remove the last comma
            return sb.ToString();
        }
    }
}
