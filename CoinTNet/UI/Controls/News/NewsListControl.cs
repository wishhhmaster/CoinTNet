using CoinTNet.BLL;
using CoinTNet.DO;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using CoinTNet.UI.Controls.News;
using CoinTNet.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls
{
    /// <summary>
    /// Control displaying a list of news items from various sources (twitter, RSS)
    /// </summary>
    public partial class NewsListControl : UserControl
    {
        #region private members

        /// <summary>
        /// The news service
        /// </summary>
        private NewsService _newsService;

        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public NewsListControl()
        {
            InitializeComponent();
            _newsService = new NewsService();
            tmrRefresh.Tick += (s, e) => UpdateNewsItems();
            EventAggregator.Instance.Subscribe<ApplicationStartMessage>(m =>
            {
                UpdateNewsItems();
                tmrRefresh.Start();
            });
            EventAggregator.Instance.Subscribe<NotificationReadMessage<NewsItem>>(m =>
            {
                NotificationPopupClicked(m.Object);
            });
        }

        /// <summary>
        /// Tags a nes item as read whenever the user clicks on the notificatin popup
        /// </summary>
        /// <param name="item">The news item that was displayed in the popup</param>
        private void NotificationPopupClicked(NewsItem item)
        {
            tlpNewsItems.Controls.Cast<NewsItemControl>().Where(c => c.NewsItem == item).First().TagAsRead();
            _newsService.NotifyNewsItemRead(item);
            item.IsNew = false;
            int nbNews = tlpNewsItems.Controls.Cast<NewsItemControl>().Where(c => c.NewsItem.IsNew).Count();
            UpdateTabText(nbNews > 0);
        }

        /// <summary>
        /// Updates the list of news items
        /// </summary>
        private void UpdateNewsItems()
        {
            var updateTask = Task.Factory.StartNew(() => { return FetchNewsItems(); });
            updateTask.ContinueWith(t =>
            {
                DisplayNewsItem(t.Result);
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Gets the list of news items
        /// </summary>
        /// <returns></returns>
        private List<NewsItem> FetchNewsItems()
        {

            using (var dbContext = new CoinTNetContext())
            {
                var newsSources = (from ns in dbContext.NewsSources
                                   select ns).ToList();

                List<NewsItem> items = new List<NewsItem>();

                foreach (var ns in newsSources.Where(n => n.Type == (int)NewsSourceType.RSS))
                {
                    items = items.Union(_newsService.GetItemsFromRSS(ns.Name, ns.Url, DateTime.Now.AddHours(-ns.ExpiryInHours))).ToList();
                }

                foreach (var ns in newsSources.Where(n => n.Type == (int)NewsSourceType.Twitter))
                {
                    items = items.Union(_newsService.GetItemsFromTwitter(ns.Name, ns.NbItems, DateTime.Now.AddHours(-ns.ExpiryInHours)))
                        .ToList();
                }

                return items.OrderByDescending(i => i.DateTime).ToList();
            }
        }

        /// <summary>
        /// Displays a list of news items
        /// </summary>
        /// <param name="items"></param>
        private void DisplayNewsItem(List<NewsItem> items)
        {
            tlpNewsItems.SuspendLayout();
            tlpNewsItems.Controls.Clear();
            tlpNewsItems.RowStyles.Clear();

            int rowIndex = 0;
            foreach (var i in items)
            {
                this.tlpNewsItems.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.AutoSize));
                var c = new NewsItemControl(i);
                c.NewsRead += (s, e) => _newsService.NotifyNewsItemRead(e);
                c.SetMaxWidth(tlpNewsItems.Width - 3);

                c.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                tlpNewsItems.Controls.Add(c, 0, rowIndex++);
            }
            tlpNewsItems.ResumeLayout();


            var newsItem = items.FirstOrDefault(i => i.IsNew);
            if (newsItem != null)
            {
                new NotificationPopUp<NewsItem>(newsItem.Title, newsItem.SourceName, newsItem.Link, DO.MessageType.Information, 10 * 1000, newsItem).Show();
            }

            UpdateTabText(newsItem != null);
        }

        /// <summary>
        /// Forces the refresh when the user clicks on the REfresh button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateNewsItems();
        }

        /// <summary>
        /// Updates the maximum size of the news controls when this control is resized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlpNewsItems_SizeChanged(object sender, EventArgs e)
        {
            tlpNewsItems.SuspendLayout();
            foreach (var c in tlpNewsItems.Controls)
            {
                (c as NewsItemControl).SetMaxWidth(tlpNewsItems.Width - 3);
            }
            tlpNewsItems.ResumeLayout();
        }

        private void btnMarkAllRead_Click(object sender, EventArgs e)
        {
            tlpNewsItems.Controls.Cast<NewsItemControl>().ToList().ForEach(c =>
                {
                    c.TagAsRead();
                    _newsService.NotifyNewsItemRead(c.NewsItem);
                });
            UpdateTabText(false);
        }

        private void btnNews_Click(object sender, EventArgs e)
        {
            using (var nsf = new NewsSourcesForm())
            {
                nsf.ShowDialog();
            }
        }

        private void UpdateTabText(bool hasNews)
        {
            var parentTab = this.FindParentControl<TabPage>();
            if (parentTab != null)
            {
                string text = hasNews ? "News*" : "News";
                if (parentTab.Text != text)//To avoid flickering
                {
                    parentTab.Text = text;
                }
            }
        }

    }
}
