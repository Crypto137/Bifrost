using System.Diagnostics;
using System.Text.Json;

namespace Bifrost.Launcher
{
    public class LaunchManager
    {
        public GameDirectory GameDirectory { get; }
        public List<Server> ServerList { get; }
        public LaunchConfig LaunchConfig { get; }

        public LaunchManager()
        {
            GameDirectory = new();

            string dir = Directory.GetCurrentDirectory();
            string serverListPath = Path.Combine(dir, "Bifrost.ServerList.json");
            string launchConfigPath = Path.Combine(dir, "Bifrost.LaunchConfig.json");

            ServerList = File.Exists(serverListPath)
                ? JsonSerializer.Deserialize<List<Server>>(File.ReadAllText(serverListPath)) ?? throw new("Invalid server list json.")
                : new() { new() };

            LaunchConfig = File.Exists(launchConfigPath)
                ? JsonSerializer.Deserialize<LaunchConfig>(File.ReadAllText(launchConfigPath)) ?? throw new("Invalid launch config json.")
                : new();
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

            Process.Start(executablePath, LaunchConfig.ToLaunchArguments(server));
            return true;
        }

        public void SaveData()
        {
            string dir = Directory.GetCurrentDirectory();
            JsonSerializerOptions jsonOptions = new() { WriteIndented = true };

            File.WriteAllText(Path.Combine(dir, "Bifrost.ServerList.json"), JsonSerializer.Serialize(ServerList, jsonOptions));
            File.WriteAllText(Path.Combine(dir, "Bifrost.LaunchConfig.json"), JsonSerializer.Serialize(LaunchConfig, jsonOptions));
        }
    }
}
