using System;

namespace CoinTNet.DO
{
    /// <summary>
    /// Defines types of news sources
    /// </summary>
    enum NewsSourceType
    {
        /// <summary>
        /// The news source if a Twitter account
        /// </summary>
        RSS,
        /// <summary>
        /// The news source is a RSS feed
        /// </summary>
        Twitter
    }

    /// <summary>
    /// Represents a news item
    /// </summary>
    class NewsItem
    {
        /// <summary>
        /// Gets or sets the news title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the Date/time the news was published
        /// </summary>
        public DateTimeOffset DateTime { get; set; }
        /// <summary>
        /// Gets or sets the news' summary
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// Gets or sets a link to the news
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Gets or sets the name of the source (e.g. twitter account, website name)
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the news has not been read yet
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// Gets or sets the tag identifying the news
        /// </summary>
        public string Tag { get; set; }
    }
}
