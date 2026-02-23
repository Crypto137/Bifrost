namespace Bifrost.Core.Models
{
    public class ServerInfo
    {
        public string Name { get; set; } = "Local Server";
        public string SiteConfigUrl { get; set; } = "localhost/SiteConfig.xml";
        public string NewsFeedUrl { get; set; } = string.Empty;

        public ServerInfo() { }
    }
}
