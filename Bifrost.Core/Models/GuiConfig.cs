using Bifrost.Core.News;

namespace Bifrost.Core.Models
{
    public class GuiConfig
    {
        public string ThemeOverride { get; set; } = string.Empty;
        public NewsFeedSourceCategories NewsCategoryFilter { get; set; } = NewsFeedSourceCategories.All;
        public string DefaultNewsFeedUrl { get; set; } = "https://crypto137.github.io/MHServerEmu/feed.xml";

        public GuiConfig Clone()
        {
            return (GuiConfig)MemberwiseClone();
        }
    }
}
