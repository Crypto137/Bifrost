namespace Bifrost.Core
{
    public class ClientMetadata
    {
        public static readonly ClientMetadata Unknown = new("Unknown");
        public static readonly ClientMetadata Default = new("1.52.0.1700");

        public string Version { get; }

        public ClientMetadata(string version)
        {
            Version = version;
        }
    }
}
