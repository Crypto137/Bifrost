namespace Bifrost.Core
{
    [Flags]
    public enum ClientFlags
    {
        None        = 0,
        BitRaider   = 1 << 0,
    }

    public class ClientMetadata
    {
        public static readonly ClientMetadata Unknown = new("Unknown", ClientFlags.None);
        public static readonly ClientMetadata Default = new("1.52.0.1700", ClientFlags.None);

        public string Version { get; }
        public ClientFlags Flags { get; }

        public ClientMetadata(string version, ClientFlags flags)
        {
            Version = version;
            Flags = flags;
        }
    }
}
