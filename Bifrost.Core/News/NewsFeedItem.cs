namespace Bifrost.Core.News
{
    public class NewsFeedItem
    {
        public NewsFeedSource Source { get; }
        public string Title { get; }
        public string Url { get; }
        public DateTime Timestamp { get; }

        public NewsFeedItem(NewsFeedSource source, string title, string url, DateTime timestamp)
        {
            Source = source;
            Title = title;
            Url = url;
            Timestamp = timestamp;
        }
    }
}
