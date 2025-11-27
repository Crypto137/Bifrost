using System.Reflection;

namespace Bifrost.Core
{
    public sealed class ClientMetadataManager
    {
        private const string MetadataEmbeddedResourceName = "Bifrost.Core.Data.ClientVersions.tsv";
        private const string DefaultExecutableHash = "AABFC231A0BA96229BCAC1C931EAEA777B7470EC";

        // Contains SHA1 hashes of Win32 executables for detecting version.
        // Potentially can be expanded to include other metadata.
        private readonly Dictionary<string, ClientMetadata> _metadataDict = new();

        public static ClientMetadataManager Instance { get; } = new();

        private ClientMetadataManager() { }

        public ClientMetadata GetClientMetadata(string executableHash)
        {
            // Quick check for the most likely version (1.52.0.1700)
            if (string.Equals(executableHash, DefaultExecutableHash, StringComparison.OrdinalIgnoreCase))
                return ClientMetadata.Default;

            // Slow check for all possible versions
            if (_metadataDict.Count == 0)
                InitializeMetadata();

            if (_metadataDict.TryGetValue(executableHash, out ClientMetadata clientMetadata) == false)
                return ClientMetadata.Unknown;

            return clientMetadata;
        }

        private void InitializeMetadata()
        {
            _metadataDict.Clear();

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
                    string hash = columns[0];
                    string version = columns[1];

                    ClientMetadata clientMetadata = new(version);
                    _metadataDict.Add(hash, clientMetadata);
                }
                catch
                {
                    continue;
                }
            }

            // Hardcoded default metadata has priority over what we load for consistency.
            _metadataDict[DefaultExecutableHash] = ClientMetadata.Default;
        }
    }
}
