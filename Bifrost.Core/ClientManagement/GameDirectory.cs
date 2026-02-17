using System.Diagnostics;
using System.Security.Cryptography;

namespace Bifrost.Core.ClientManagement
{
    public enum GameDirectoryInitializationResult
    {
        Success,
        ClientNotFound,
        ExecutableNotFound
    }

    public class GameDirectory
    {
        // Executables are listed in reverse order because users are more likely to use more recent versions.
        private static readonly string[] ExecutableNames =
        [
            "MarvelHeroesOmega.exe",    // From 1.52.0.1168 (2017-07-05)
            "MarvelHeroes2016.exe",     // From 1.42.0.59   (2016-01-22)
            "MarvelHeroes2015.exe",     // From 1.23.0.23   (2014-06-04)
            "MarvelGame.exe",           // Original name
        ];

        private static readonly byte[] ShippingSignature = Convert.FromHexString("5368697070696E675C"); // Shipping\

        private string _directoryPath = string.Empty;
        private string _executableDirectory32 = string.Empty;
        private string _executableDirectory64 = string.Empty;
        private string _executableName = string.Empty;

        internal string ExecutablePath32 { get => IsInitialized ? Path.Combine(_executableDirectory32, _executableName) : string.Empty; }
        internal string ExecutablePath64 { get => IsInitialized && Supports64 ? Path.Combine(_executableDirectory64, _executableName) : string.Empty; }
        internal bool Supports64 { get; private set; }  // Only versions 1.26.0.119 (2014-09-12) and up have Win64 executables

        public bool IsInitialized { get; private set; } = false;
        public ClientMetadata ClientMetadata { get; private set; } = ClientMetadata.Unknown;

        public GameDirectory()
        {
        }

        public GameDirectoryInitializationResult Initialize(string path)
        {
            // Detect executable directories
            _directoryPath = FindClientDirectory(path);
            _executableDirectory32 = Path.Combine(_directoryPath, "UnrealEngine3", "Binaries", "Win32");
            _executableDirectory64 = Path.Combine(_directoryPath, "UnrealEngine3", "Binaries", "Win64");

            if (Directory.Exists(_executableDirectory32) == false)
            {
                IsInitialized = false;
                return GameDirectoryInitializationResult.ClientNotFound;
            }

            // Detect executable name
            _executableName = DetectExecutableName(_executableDirectory32);
            if (_executableName == string.Empty)
            {
                IsInitialized = false;
                return GameDirectoryInitializationResult.ExecutableNotFound;
            }

            // Find client metadata and detect Win64 support
            ClientMetadata = GetClientMetadata(_executableDirectory32, _executableName);
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

            // Look for the shipping build signature.
            // HACK: speed this up by searching near the end of the executable where the build config signatures we are looking for should be.
            int signatureStart = executable.Length - executable.Length / 5;
            bool isShipping = executable.AsSpan(signatureStart).IndexOf(ShippingSignature) != -1;
            
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

        private static string DetectExecutableName(string directory)
        {
            foreach (string fileName in ExecutableNames)
            {
                string path = Path.Combine(directory, fileName);
                if (File.Exists(path))
                    return fileName;
            }

            return string.Empty;
        }

        private static ClientMetadata GetClientMetadata(string directory, string fileName)
        {
            string path = Path.Combine(directory, fileName);
            if (File.Exists(path) == false)
                return ClientMetadata.Unknown;

            byte[] executable = File.ReadAllBytes(path);
            string hash = Convert.ToHexString(SHA1.HashData(executable));

            return ClientMetadataManager.Instance.GetClientMetadata(hash);
        }

        private static string FindClientDirectory(string rootDirectory)
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
    }
}
