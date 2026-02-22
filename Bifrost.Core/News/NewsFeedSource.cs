using System.ServiceModel.Syndication;
using System.Xml;

namespace Bifrost.Core.News
{
    public enum NewsFeedSourceCategory
    { 
        Default,
        CommunityServer,
    }

    public class NewsFeedSource
    {
        private readonly List<NewsFeedItem> _items = new();

        public NewsFeed Feed { get; }
        public string Url { get; }
        public string Name { get; }
        public NewsFeedSourceCategory Category { get; }

        public bool IsLoaded { get; private set; }

        public IReadOnlyList<NewsFeedItem> Items { get => _items; }

        internal NewsFeedSource(NewsFeed feed, string url, string name, NewsFeedSourceCategory category)
        {
            Feed = feed;
            Url = url;
            Name = name;
            Category = category;
        }

        public override string ToString()
        {
            return $"[{Category}] {Name} ({Url})";
        }

        public void Load(Action<NewsFeedSource> callback = null)
        {
            _items.Clear();

            try
            {
                XmlReader reader = XmlReader.Create(Url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();

                foreach (SyndicationItem feedItem in feed.Items)
                    _items.Add(new(feedItem));

                IsLoaded = true;
            }
            catch
            {
                return;
            }
            finally
            {
                callback?.Invoke(this);
            }
        }
    }
}
