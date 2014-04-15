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

        public NewsItem NewsItem { get; private set; }

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

        private void lblTitle_Click(object sender, EventArgs e)
        {
            TagAsRead();
            Process.Start(NewsItem.Link);
            if (this.NewsRead != null)
            {
                this.NewsRead(this, NewsItem);
            }
        }

        public void SetMaxWidth(int width)
        {
            this.MaximumSize = new Size(width, 0);
            this.tlpNewsItem.MaximumSize = this.MaximumSize;
            lblTitle.MaximumSize = this.MaximumSize;
        }

        public void TagAsRead()
        {
            lblFrom.Text = lblFrom.Text.Replace(" - NEW", string.Empty);
            lblFrom.ForeColor = System.Drawing.SystemColors.ControlText;
        }
    }
}
