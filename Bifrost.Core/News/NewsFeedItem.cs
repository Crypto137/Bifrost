namespace Bifrost.Core.News
{
    public class NewsFeedItem
    {
        public string Title { get; init; }
        public string Url { get; init; }
        public DateTime Timestamp { get; init; }

        public NewsFeedItem()
        {
        }

        public NewsFeedItem(string title, string url, DateTime timestamp)
        {
            Title = title;
            Url = url;
            Timestamp = timestamp;
        }
    }
}
