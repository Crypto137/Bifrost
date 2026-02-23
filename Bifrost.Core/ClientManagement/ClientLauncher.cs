using System.Diagnostics;
using Bifrost.Core.Models;
using Bifrost.Core.News;
using Bifrost.Core.Serialization;

namespace Bifrost.Core.ClientManagement
{
    public enum ClientLauncherInitializationResult
    {
        Success,
        ClientNotFound,
        ExecutableNotFound
    }

    public class ClientLauncher
    {
        private const string ServerListFileName = "Bifrost.ServerList.json";
        private const string LaunchConfigFileName = "Bifrost.LaunchConfig.json";
        private const string GuiConfigFileName = "Bifrost.GuiConfig.json";

        private readonly ClientDirectory _clientDirectory = new();

        public ServerManager ServerManager { get; } = new();
        public NewsFeed NewsFeed { get; } = new();

        public LaunchConfig LaunchConfig { get; private set; }
        public GuiConfig GuiConfig { get; private set; }

        public ClientMetadata ClientMetadata { get => _clientDirectory.ClientMetadata; }

        public ClientLauncher()
        {
        }

        public ClientLauncherInitializationResult Initialize(string clientPath)
        {
            // Configs
            LaunchConfig = JsonHelper.LoadOrCreate(LaunchConfigFileName, JsonContext.Default.LaunchConfig);
            GuiConfig = JsonHelper.LoadOrCreate(GuiConfigFileName, JsonContext.Default.GuiConfig);

            // ServerList
            List<ServerInfo> serverList = JsonHelper.LoadOrCreate(ServerListFileName, JsonContext.Default.ListServerInfo);
            ServerManager.SetServerList(serverList);

            if (ServerManager.ServerCount == 0)
                ServerManager.AddServer();

            // ClientDirectory
            ClientLauncherInitializationResult directoryResult = _clientDirectory.Initialize(clientPath);
            if (directoryResult != ClientLauncherInitializationResult.Success)
                return directoryResult;

            return ClientLauncherInitializationResult.Success;
        }

        public void RefreshNewsFeedSources()
        {
            NewsFeed.Clear();

            string defaultNewsFeedUrl = GuiConfig.DefaultNewsFeedUrl;
            if (string.IsNullOrWhiteSpace(defaultNewsFeedUrl) == false)
                NewsFeed.AddSource(defaultNewsFeedUrl, "Bifrost", NewsFeedSourceCategories.Default);

            foreach (ServerInfo server in ServerManager)
            {
                string serverNewsFeedUrl = server.NewsFeedUrl;
                if (string.IsNullOrWhiteSpace(serverNewsFeedUrl))
                    continue;

                NewsFeed.AddSource(serverNewsFeedUrl, server.Name, NewsFeedSourceCategories.Server);
            }
        }

        public void SaveData()
        {
            JsonHelper.Save(ServerManager.ServerList, ServerListFileName, JsonContext.Default.ListServerInfo);
            JsonHelper.Save(LaunchConfig, LaunchConfigFileName, JsonContext.Default.LaunchConfig);
            JsonHelper.Save(GuiConfig, GuiConfigFileName, JsonContext.Default.GuiConfig);
        }

        public bool Launch()
        {
            // Use the specified server if index is within range
            ServerInfo serverInfo = ServerManager.GetServer(LaunchConfig.ServerIndex);
            if (serverInfo == null)
                return false;

            // Prefer Win64 if supported and Win32 is not forced. Otherwise launch Win32.
            string executablePath = _clientDirectory.Supports64
                ? LaunchConfig.Force32Bit ? _clientDirectory.ExecutablePath32 : _clientDirectory.ExecutablePath64
                : _clientDirectory.ExecutablePath32;

            if (string.IsNullOrWhiteSpace(executablePath))
                return false;

            string[] args = LaunchConfig.ToLaunchArguments(serverInfo, _clientDirectory.ClientMetadata.Flags);
            Process.Start(executablePath, args);
            return true;
        }

        public string GetClientDebugInfo()
        {
            return _clientDirectory.GetVersionDebugInfo();
        }

        public static string GetInitializationResultText(ClientLauncherInitializationResult result)
        {
            return result switch
            {
                ClientLauncherInitializationResult.Success              => "Game directory initialized successfully.",
                ClientLauncherInitializationResult.ClientNotFound       => "Marvel Heroes not found.",
                ClientLauncherInitializationResult.ExecutableNotFound   => "Marvel Heroes executable not found.",
                _                                                       => "Unknown error.",
            };
        }
    }
}
