namespace Bifrost.Core
{
    public class ClientMetadata
    {
        public string Version { get; }

        public ClientMetadata(string version)
        {
            Version = version;
        }
    }
}
