using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CoinTNet.DO
{
    /// <summary>
    /// Represents a tweet retrieved from Twitter
    /// </summary>
    class Tweet
    {
        /// <summary>
        /// The Date format of the retrieved tweet
        /// </summary>
        public const string DateFormat = "ddd MMM dd HH:mm:ss zzzzz yyyy";
        /// <summary>
        /// The Tweet's creation date
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// The Tweet's ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// The Tweet's content
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns a Tweet from a Json object
        /// </summary>
        /// <param name="o">The Json object</param>
        /// <returns>An isntance of Tweet</returns>
        public static Tweet ReadFromJObject(JObject o)
        {
            if (o == null)
                return null;

            var r = new Tweet()
            {
                ID = o.Value<string>("id_str"),
                Text = o.Value<string>("text"),
                CreatedAt = DateTimeOffset.ParseExact(o.Value<string>("created_at"), DateFormat, CultureInfo.InvariantCulture)
            };

            return r;
        }

    }
}
