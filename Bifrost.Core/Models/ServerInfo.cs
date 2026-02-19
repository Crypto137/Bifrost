namespace Bifrost.Core.Models
{
    public class ServerInfo
    {
        public string Name { get; set; } = "Local Server";
        public string SiteConfigUrl { get; set; } = "localhost/SiteConfig.xml";

        public ServerInfo(string name, string siteConfigUrl)
        {
            Name = name;
            SiteConfigUrl = siteConfigUrl;
        }

        public ServerInfo() { }
    }
}
