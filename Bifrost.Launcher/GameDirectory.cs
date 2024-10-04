using System.Diagnostics;
using System.Security.Cryptography;

namespace Bifrost.Launcher
{
    public enum GameDirectoryInitializationResult
    {
        Success,
        ClientNotFound,
        ExecutableNotFound
    }

    public class GameDirectory
    {
        private static readonly string[] ExecutableNames = new string[]
        {
            "MarvelGame.exe",           // Original name
            "MarvelHeroes2015.exe",     // From 1.23.0.23   (2014-06-04)
            "MarvelHeroes2016.exe",     // From 1.42.0.59   (2016-01-22)
            "MarvelHeroesOmega.exe"     // From 1.52.0.1168 (2017-07-05)
        };

        private static readonly byte[] ShippingSignature = Convert.FromHexString("5368697070696E675C"); // Shipping\

        private string _directoryPath = string.Empty;
        private string _executableDirectory32 = string.Empty;
        private string _executableDirectory64 = string.Empty;
        private string _executableName = string.Empty;

        internal string ExecutablePath32 { get => IsInitialized ? Path.Combine(_executableDirectory32, _executableName) : string.Empty; }
        internal string ExecutablePath64 { get => IsInitialized && Supports64 ? Path.Combine(_executableDirectory64, _executableName) : string.Empty; }
        internal bool Supports64 { get; private set; }  // Only versions 1.26.0.119 (2014-09-12) and up have Win64 executables

        public bool IsInitialized { get; private set; } = false;
        public string Version { get; private set; } = "Unknown Version";

        public GameDirectoryInitializationResult Initialize(string path)
        {
            // Determine executable directories
            _directoryPath = FindClientDirectory(path);
            _executableDirectory32 = Path.Combine(_directoryPath, "UnrealEngine3", "Binaries", "Win32");
            _executableDirectory64 = Path.Combine(_directoryPath, "UnrealEngine3", "Binaries", "Win64");

            // Fail initialization if Win32 executable directory does not exist
            if (Directory.Exists(_executableDirectory32) == false)
            {
                IsInitialized = false;
                return GameDirectoryInitializationResult.ClientNotFound;
            }

            // Detect executable name
            _executableName = DetectExecutableName(_executableDirectory32);

            // Fail initialization if could not detect executable name
            if (_executableName == string.Empty)
            {
                IsInitialized = false;
                return GameDirectoryInitializationResult.ExecutableNotFound;
            }

            // Detect version and Win64 support
            Version = DetectExecutableVersion();
            Supports64 = Directory.Exists(_executableDirectory64) && File.Exists(Path.Combine(_executableDirectory64, _executableName));

            // Finish initialization
            IsInitialized = true;
            return GameDirectoryInitializationResult.Success;
        }

        public string GetVersionDebugInfo()
        {
            if (IsInitialized == false)
                return "Not Initialized";

            var versionInfo = FileVersionInfo.GetVersionInfo(ExecutablePath32);
            byte[] executable = File.ReadAllBytes(ExecutablePath32);
            bool isShipping = CheckExecutableSignature(executable, ShippingSignature);
            string hash = Convert.ToHexString(SHA1.HashData(executable));

            return $"{_executableName}\n{hash}\n{versionInfo.FileVersion.Replace(',', '.')}\nIsShipping: {isShipping}";
        }

        public static string GetInitializationResultText(GameDirectoryInitializationResult result)
        {
            return result switch
            {
                GameDirectoryInitializationResult.Success               => "Game directory initialized successfully.",
                GameDirectoryInitializationResult.ClientNotFound        => "Marvel Heroes not found.",
                GameDirectoryInitializationResult.ExecutableNotFound    => "Marvel Heroes executable not found.",
                _                                                       => "Unknown error.",
            };
        }
        
        private string FindClientDirectory(string rootDirectory)
        {
            // We are in the right directory - no adjustments needed
            if (Directory.Exists(Path.Combine(rootDirectory, "UnrealEngine3")))
                return rootDirectory;

            // We are in the binaries directory - go up three levels
            if (DetectExecutableName(rootDirectory) != string.Empty)
                return Path.GetFullPath(Path.Combine(rootDirectory, "..", "..", ".."));

            // As a last resort try going up level (if the launcher is in a subdirectory with the client folder)
            return Path.GetFullPath(Path.Combine(rootDirectory, ".."));
        }

        private string DetectExecutableName(string binariesDirectory)
        {
            // Check executable names in reverse order (because users are more likely to use later versions)
            for (int i = ExecutableNames.Length - 1; i >= 0; i--)
            {
                if (File.Exists(Path.Combine(binariesDirectory, ExecutableNames[i])))
                    return ExecutableNames[i];
            }

            return string.Empty;
        }

        private string DetectExecutableVersion()
        {
            byte[] executable = File.ReadAllBytes(Path.Combine(_executableDirectory32, _executableName));
            string hash = Convert.ToHexString(SHA1.HashData(executable));

            // Quick check for the most likely version
            if (hash == "AABFC231A0BA96229BCAC1C931EAEA777B7470EC")
                return "1.52.0.1700";

            // Slow check for all possible versions
            return ClientMetadataManager.Instance.GetVersionNumberFromExecutableHash(hash);
        }

        private bool CheckExecutableSignature(byte[] executable, byte[] signature)
        {
            // HACK: speed this up by starting near the end of the executable
            // where the build config signatures we are looking for should be.
            for (int i = executable.Length - executable.Length / 5; i < executable.Length; i++)
            {
                if (executable[i] != signature[0]) continue;  // Check the entire signature only if the first character matches

                if (signature.SequenceEqual(executable.Skip(i).Take(signature.Length)))
                    return true;
            }

            return false;
        }
    }
}
