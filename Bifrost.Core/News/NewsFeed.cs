namespace Bifrost.Core.News
{
    public class NewsFeed
    {
        private readonly Dictionary<string, NewsFeedSource> _sources = new(StringComparer.InvariantCultureIgnoreCase);

        public IReadOnlyDictionary<string, NewsFeedSource> Sources { get => _sources; }

        public NewsFeed()
        {
        }

        public NewsFeedSource AddSource(string url, string name, NewsFeedSourceCategories category)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(url);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            NewsFeedSource source = new(this, url, name, category);
            _sources[url] = source;
            return source;
        }

        public NewsFeedSource GetSource(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;

            if (_sources.TryGetValue(url, out NewsFeedSource source) == false)
                return null;

            return source;
        }

        public NewsFeedSource GetDefaultSource()
        {
            foreach (NewsFeedSource source in _sources.Values)
            {
                if (source.Category == NewsFeedSourceCategories.Default)
                    return source;
            }

            return null;
        }

        public void GetNews(List<NewsFeedItem> items, NewsFeedSourceCategories categories = NewsFeedSourceCategories.All)
        {
            foreach (NewsFeedSource source in _sources.Values)
            {
                if (categories.HasFlag(source.Category) == false)
                    continue;

                items.AddRange(source.Items);
            }

            // Recent news go first
            items.Sort((a, b) => b.Timestamp.CompareTo(a.Timestamp));
        }

        public void Clear()
        {
            _sources.Clear();
        }
    }
}
