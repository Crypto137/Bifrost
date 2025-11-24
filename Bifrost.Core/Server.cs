namespace Bifrost.Core
{
    public class Server
    {
        public string Name { get; set; } = "Local Server";
        public string SiteConfigUrl { get; set; } = "localhost/SiteConfig.xml";

        public Server(string name, string siteConfigUrl)
        {
            Name = name;
            SiteConfigUrl = siteConfigUrl;
        }

        public Server() { }
    }
}
