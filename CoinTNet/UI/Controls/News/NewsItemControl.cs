using CoinTNet.DO;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls.News
{

    /// <summary>
    /// Control displaying a single news item
    /// </summary>
    partial class NewsItemControl : UserControl
    {
        /// <summary>
        /// Event triggered when the user clicks on the news item
        /// </summary>
        public event EventHandler<NewsItem> NewsRead;

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        /// <param name="newsItem"></param>
        public NewsItemControl(NewsItem newsItem)
        {
            InitializeComponent();
            lblTitle.Text = newsItem.Title;
            NewsItem = newsItem;
            lblFrom.Text = FormatTime(newsItem) + " - " + newsItem.SourceName;
            if (newsItem.IsNew)
            {
                lblFrom.Text += " - NEW";
                lblFrom.ForeColor = Color.Red;
            }
            lblFrom.Click += lblTitle_Click;
            //lblSummary.Text = newsItem.Summary;
        }

        /// <summary>
        /// Gets the news item associated with the control
        /// </summary>
        public NewsItem NewsItem { get; private set; }

        #region Public methods

        /// <summary>
        /// Sets the control's max width
        /// </summary>
        /// <param name="width"></param>
        public void SetMaxWidth(int width)
        {
            this.MaximumSize = new Size(width, 0);
            this.tlpNewsItem.MaximumSize = this.MaximumSize;
            lblTitle.MaximumSize = this.MaximumSize;
        }

        /// <summary>
        /// Notifies the controls that the news has been read
        /// </summary>
        public void TagAsRead()
        {
            lblFrom.Text = lblFrom.Text.Replace(" - NEW", string.Empty);
            lblFrom.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        #endregion


        /// <summary>
        /// Formats the display of the time the news was published
        /// </summary>
        /// <param name="newsItem">the news item</param>
        /// <returns>A string like xx minutes/hours/days ago</returns>
        private static string FormatTime(NewsItem newsItem)
        {
            var diff = DateTime.Now - newsItem.DateTime;
            if (diff.TotalDays > 1)
            {
                return string.Format("{0} day(s) ago", (int)diff.TotalDays);
            }
            else if (diff.TotalHours > 1)
            {
                return string.Format("{0} hours(s) ago", (int)diff.TotalHours);
            }
            else
            {
                return string.Format("{0} minutes(s) ago", (int)diff.TotalMinutes);
            }
        }

        /// <summary>
        /// Opens a browser to display the news and triggers the NewsRead item when the user clicks on a news
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTitle_Click(object sender, EventArgs e)
        {
            TagAsRead();
            Process.Start(NewsItem.Link);
            if (this.NewsRead != null)
            {
                this.NewsRead(this, NewsItem);
            }
        }

    }
}
