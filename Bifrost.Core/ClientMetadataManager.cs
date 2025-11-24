using System.Reflection;

namespace Bifrost.Core
{
    public sealed class ClientMetadataManager
    {
        private const string MetadataEmbeddedResourceName = "Bifrost.Core.Data.ClientVersions.tsv";

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

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(MetadataEmbeddedResourceName);
            using StreamReader reader = new(stream);

            string row;
            while ((row = reader.ReadLine()) != null)
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
