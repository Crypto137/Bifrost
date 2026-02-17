using Bifrost.Core.ClientManagement;

namespace Bifrost.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            ClientLauncher clientLauncher = new();

            string dir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            Console.WriteLine($"Directory: {dir}");

            ClientLauncherInitializationResult result = clientLauncher.Initialize(dir);
            Console.WriteLine(ClientLauncher.GetInitializationResultText(result));
            if (result != ClientLauncherInitializationResult.Success)
            {
                Console.ReadLine();
                return;
            };

            Console.WriteLine($"Version: {clientLauncher.ClientMetadata.Version}");

            foreach (string arg in clientLauncher.Config.ToLaunchArguments(new(), ClientFlags.None))
                Console.WriteLine(arg);

            Console.WriteLine("Launching...");

            if (clientLauncher.Launch())
                Console.WriteLine("Launched successfully");
            else
                Console.WriteLine("Launch failed. Make sure you initialized game directory!");

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
