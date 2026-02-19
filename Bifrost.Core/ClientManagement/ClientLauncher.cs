using System.Diagnostics;
using Bifrost.Core.Models;
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

        private readonly ClientDirectory _clientDirectory = new();

        public ServerManager ServerManager { get; } = new();
        public LaunchConfig Config { get; private set; }

        public ClientMetadata ClientMetadata { get => _clientDirectory.ClientMetadata; }

        public ClientLauncher()
        {
        }

        public ClientLauncherInitializationResult Initialize(string clientPath)
        {
            // LaunchConfig
            Config = JsonHelper.LoadOrCreate(LaunchConfigFileName, JsonContext.Default.LaunchConfig);

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

        public void SaveData()
        {
            JsonHelper.Save(ServerManager.ServerList, ServerListFileName, JsonContext.Default.ListServerInfo);
            JsonHelper.Save(Config, LaunchConfigFileName, JsonContext.Default.LaunchConfig);
        }

        public bool Launch()
        {
            // Use the specified server if index is within range
            ServerInfo serverInfo = ServerManager.GetServer(Config.ServerIndex);
            if (serverInfo == null)
                return false;

            // Prefer Win64 if supported and Win32 is not forced. Otherwise launch Win32.
            string executablePath = _clientDirectory.Supports64
                ? Config.Force32Bit ? _clientDirectory.ExecutablePath32 : _clientDirectory.ExecutablePath64
                : _clientDirectory.ExecutablePath32;

            if (string.IsNullOrWhiteSpace(executablePath))
                return false;

            string[] args = Config.ToLaunchArguments(serverInfo, _clientDirectory.ClientMetadata.Flags);
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
