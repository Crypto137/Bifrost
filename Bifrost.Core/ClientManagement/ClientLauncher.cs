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

        private ClientDirectory _clientDirectory = new();

        public List<Server> ServerList { get; }
        public LaunchConfig Config { get; }

        public ClientMetadata ClientMetadata { get => _clientDirectory.ClientMetadata; }

        public ClientLauncher()
        {
            ServerList = JsonHelper.LoadOrCreate(ServerListFileName, JsonContext.Default.ListServer);
            Config = JsonHelper.LoadOrCreate(LaunchConfigFileName, JsonContext.Default.LaunchConfig);

            if (ServerList.Count == 0)
                ServerList.Add(new());
        }

        public ClientLauncherInitializationResult Initialize(string clientPath)
        {
            ClientLauncherInitializationResult directoryResult = _clientDirectory.Initialize(clientPath);
            if (directoryResult != ClientLauncherInitializationResult.Success)
                return directoryResult;

            return ClientLauncherInitializationResult.Success;
        }

        public void SaveData()
        {
            JsonHelper.Save(ServerList, ServerListFileName, JsonContext.Default.ListServer);
            JsonHelper.Save(Config, LaunchConfigFileName, JsonContext.Default.LaunchConfig);
        }

        public bool Launch()
        {
            // Use the specified server if index is within range
            Server server = Config.ServerIndex >= 0 && Config.ServerIndex < ServerList.Count
                ? ServerList[Config.ServerIndex]
                : null;

            // Prefer Win64 if supported and Win32 is not forced. Otherwise launch Win32.
            string executablePath = _clientDirectory.Supports64
                ? Config.Force32Bit ? _clientDirectory.ExecutablePath32 : _clientDirectory.ExecutablePath64
                : _clientDirectory.ExecutablePath32;

            if (string.IsNullOrWhiteSpace(executablePath))
                return false;

            string[] args = Config.ToLaunchArguments(server, _clientDirectory.ClientMetadata.Flags);
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
