using System.Reflection;

namespace Bifrost.Launcher
{
    public sealed class ClientMetadataManager
    {
        // Contains SHA1 hashes of Win32 executables for detecting version.
        // Potentially can be expanded to include other metadata.
        private readonly Dictionary<string, string> _versionMetadataDict = new();

        public static ClientMetadataManager Instance { get; } = new();

        private ClientMetadataManager() { }

        public string GetVersionNumberFromExecutableHash(string hash)
        {
            if (_versionMetadataDict.Count == 0)
                InitializeMetadata();

            if (_versionMetadataDict.TryGetValue(hash, out string versionNumber) == false)
                return "Unknown";

            return versionNumber;
        }

        private void InitializeMetadata()
        {
            _versionMetadataDict.Clear();

            string launcherDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string metadataFilePath = Path.Combine(launcherDirectory, "Bifrost.ClientMetadata.tsv");
            
            if (File.Exists(metadataFilePath) == false)
                return;

            string[] rows = File.ReadAllLines(metadataFilePath);
            foreach (string row in rows)
            {
                string[] columns = row.Split('\t');

                if (columns.Length < 2)
                    continue;

                try
                {
                    _versionMetadataDict.Add(columns[0], new(columns[1]));
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
