using System.ServiceModel.Syndication;
using System.Xml;

namespace Bifrost.Core.News
{
    [Flags]
    public enum NewsFeedSourceCategories
    { 
        None    = 0,
        Default = 1 << 0,
        Server  = 1 << 1,
        All     = Default | Server,
    }

    public class NewsFeedSource
    {
        private readonly List<NewsFeedItem> _items = new();

        public NewsFeed Feed { get; }
        public string Url { get; }
        public string Name { get; }
        public NewsFeedSourceCategories Category { get; }

        public bool IsLoaded { get; private set; }

        public IReadOnlyList<NewsFeedItem> Items { get => _items; }

        internal NewsFeedSource(NewsFeed feed, string url, string name, NewsFeedSourceCategories category)
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
                {
                    string title = feedItem.Title.Text.Trim();
                    string url = feedItem.Links.Count > 0 ? feedItem.Links[0].Uri.AbsoluteUri : string.Empty;
                    DateTime timestamp = feedItem.LastUpdatedTime.LocalDateTime;

                    NewsFeedItem item = new(this, title, url, timestamp);
                    _items.Add(item);
                }

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
