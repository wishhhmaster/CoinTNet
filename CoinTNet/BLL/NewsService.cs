using CoinTNet.Common;
using CoinTNet.DAL;
using CoinTNet.DO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace CoinTNet.BLL
{
    /// <summary>
    /// Service in charge of retrieving news items from various sources (RSS feeds and Twitter for now)
    /// </summary>
    class NewsService
    {
        #region Private members
        /// <summary>
        /// The DB context
        /// </summary>
        private CoinTNetContext _dbContext;

        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public NewsService()
        {
            _dbContext = new CoinTNetContext();
        }

        /// <summary>
        /// Gets latest tweets from a user
        /// </summary>
        /// <param name="userName">The user to retrieve the twweets from</param>
        /// <param name="count">The max number of tweets to retrieve</param>
        /// <param name="minDateTime">The min date time</param>
        /// <returns>A list of NewsItems</returns>
        public List<NewsItem> GetItemsFromTwitter(string userName, int count, DateTimeOffset minDateTime)
        {
            var proxy = TwitterProxyFactory.GetProxy();
            if (!proxy.HasKeys)
            {
                return new List<NewsItem>();
            }
            try
            {
                var lines = GetReadNews();
                var items = proxy.GetTweets(userName, count).Where(t => t.CreatedAt >= minDateTime).Select(t => new NewsItem
                {
                    DateTime = t.CreatedAt,
                    Title = t.Text,
                    Link = string.Format("{0}/{1}/status/{2}", "https://twitter.com", userName, t.ID),
                    SourceName = "Twitter - " + userName

                }).ToList();
                items.ForEach(i =>
                {
                    i.Tag = GetSavedFormat(i);
                    i.IsNew = !lines.Any(l => l == i.Tag);
                });
                return items;
            }
            catch (Exception ex)
            {
                if (CoinTNet.Properties.Settings.Default.LogNewsErrors)
                {
                    Logger.Log(ex);
                }
            }
            return new List<NewsItem>();
        }

        /// <summary>
        /// Gets latest news from RSS feeds
        /// </summary>
        /// <param name="sourceName">The name of the source</param>
        /// <param name="feedUrl">The feed' url</param>
        /// <param name="minDate">The min date</param>
        /// <returns>A list of news items</returns>
        public List<NewsItem> GetItemsFromRSS(string sourceName, string feedUrl, DateTimeOffset minDate)
        {
            try
            {
                var lines = GetReadNews();
                //SyndicationFeed does not work with CoinDesk

                HttpWebRequest req = HttpWebRequest.Create(feedUrl) as HttpWebRequest;
                req.Method = "GET";
                req.Headers.Add("Accept-Encoding", "gzip,deflate");
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                string content = string.Empty;
                using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    //XDocument.Load(url) sometimes fails... maybe because of the compression
                    var d = XDocument.Load(reader);
                    DateTimeOffset dt;

                    var q = from item in d.Descendants("item")
                            select new NewsItem
                            {

                                //  Description = item.Descendants(cn + "encoded").First().Value,
                                Title = item.Descendants("title").First().Value,
                                Summary = item.Descendants("description").First().Value,
                                Link = item.Descendants("comments").First().Value.Replace("#comments", string.Empty),
                                DateTime = DateTimeOffset.TryParse(item.Descendants("pubDate").First().Value, out dt) ? dt : DateTimeOffset.Now.Date,
                                SourceName = sourceName
                            };
                    var items = q.Where(i => i.DateTime > minDate).ToList();
                    items.ForEach(i =>
                        {
                            i.Tag = GetSavedFormat(i);
                            i.IsNew = !lines.Any(l => l == i.Tag);
                        });
                    return items;
                }
            }
            catch (Exception ex)
            {
                if (CoinTNet.Properties.Settings.Default.LogNewsErrors)
                {
                    Logger.Log(ex);
                }
            }
            return new List<NewsItem>();

        }

        /// <summary>
        /// Returns a list of strings identifying all read news
        /// </summary>
        /// <returns></returns>
        private string[] GetReadNews()
        {
            return File.Exists(GetStoreFile()) ? File.ReadAllLines(GetStoreFile()) : new string[0];
        }
        public bool CheckNewsItemHasBeenRead(NewsItem item)
        {
            if (!File.Exists(GetStoreFile()))
            {
                return false;
            }

            var lines = File.ReadAllLines(GetStoreFile());
            return lines.Any(l => l == item.Tag);
        }
        public void NotifyNewsItemRead(NewsItem item)
        {
            string[] lines = new string[0];
            if (File.Exists(GetStoreFile()))
            {
                lines = File.ReadAllLines(GetStoreFile());
            }
            File.WriteAllLines(GetStoreFile(), new string[] { item.Tag });
            File.AppendAllLines(GetStoreFile(), lines);
        }
        /// <summary>
        /// Returns a string
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetSavedFormat(NewsItem item)
        {
            return item.DateTime.ToString(CultureInfo.InvariantCulture) + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(item.Title));
        }

        /// <summary>
        /// Gets the file when read news items are stored
        /// </summary>
        /// <returns></returns>
        private string GetStoreFile()
        {
            return Path.Combine(Environment.CurrentDirectory, "news.txt");
        }
    }
}
