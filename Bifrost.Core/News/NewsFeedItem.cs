using System.ServiceModel.Syndication;

namespace Bifrost.Core.News
{
    public class NewsFeedItem
    {
        private SyndicationItem _syndicationItem;

        public string Title { get => _syndicationItem.Title.Text; }
        public string Url { get => _syndicationItem.Links[0]?.Uri.AbsoluteUri; }

        internal NewsFeedItem(SyndicationItem syndicationItem)
        {
            _syndicationItem = syndicationItem;
        }
    }
}
