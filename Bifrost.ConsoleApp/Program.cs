using Bifrost.Core;

namespace Bifrost.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            LaunchManager launchManager = new();

            string dir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            Console.WriteLine($"Directory: {dir}");

            GameDirectoryInitializationResult result = launchManager.GameDirectory.Initialize(dir);
            Console.WriteLine(GameDirectory.GetInitializationResultText(result));
            if (result != GameDirectoryInitializationResult.Success)
            {
                Console.ReadLine();
                return;
            };

            Console.WriteLine($"Version: {launchManager.GameDirectory.Version}");

            foreach (string arg in launchManager.LaunchConfig.ToLaunchArguments(new()))
                Console.WriteLine(arg);

            Console.WriteLine("Launching...");

            if (launchManager.Launch())
                Console.WriteLine("Launched successfully");
            else
                Console.WriteLine("Launch failed. Make sure you initialized game directory!");

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
