using System.Diagnostics;
using Bifrost.Core.Serialization;

namespace Bifrost.Core
{
    public class LaunchManager
    {
        private const string ServerListFileName = "Bifrost.ServerList.json";
        private const string LaunchConfigFileName = "Bifrost.LaunchConfig.json";

        public GameDirectory GameDirectory { get; }
        public List<Server> ServerList { get; }
        public LaunchConfig LaunchConfig { get; }

        public LaunchManager()
        {
            GameDirectory = new();
            ServerList = JsonHelper.LoadOrCreate(ServerListFileName, JsonContext.Default.ListServer);
            LaunchConfig = JsonHelper.LoadOrCreate(LaunchConfigFileName, JsonContext.Default.LaunchConfig);

            if (ServerList.Count == 0)
                ServerList.Add(new());
        }

        public void SaveData()
        {
            JsonHelper.Save(ServerList, ServerListFileName, JsonContext.Default.ListServer);
            JsonHelper.Save(LaunchConfig, LaunchConfigFileName, JsonContext.Default.LaunchConfig);
        }

        public bool Launch()
        {
            // Use the specified server if index is within range
            Server server = (LaunchConfig.ServerIndex >= 0 && LaunchConfig.ServerIndex < ServerList.Count)
                ? ServerList[LaunchConfig.ServerIndex]
                : null;

            // Prefer Win64 if supported and Win32 is not forced. Otherwise launch Win32.
            string executablePath = GameDirectory.Supports64
                ? LaunchConfig.Force32Bit ? GameDirectory.ExecutablePath32 : GameDirectory.ExecutablePath64
                : GameDirectory.ExecutablePath32;

            if (string.IsNullOrWhiteSpace(executablePath))
                return false;

            string[] args = LaunchConfig.ToLaunchArguments(server, GameDirectory.ClientMetadata.Flags);
            Process.Start(executablePath, args);
            return true;
        }
    }
}
