using System.Text;

namespace Bifrost.Launcher
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

        public string[] ToLaunchArguments(Server server)
        {
            List<string> argumentList = new();

            switch (Downloader)
            {
                case Downloader.Robocopy:
                    argumentList.Add("-robocopy");
                    argumentList.Add("-nosolidstate");
                    argumentList.Add("-nosteam");
                    break;

                case Downloader.SolidState:
                    argumentList.Add("-solidstate");
                    argumentList.Add("-nosteam");
                    break;

                case Downloader.Steam:
                    argumentList.Add("-steam");
                    argumentList.Add("-nosolidstate");
                    break;
            }

            if (server != null) argumentList.Add($"-siteconfigurl={server.SiteConfigUrl}");

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
