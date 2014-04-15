using CoinTNet.DO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CoinTNet.DAL
{
    /// <summary>
    /// Proxy for accessing Tweets
    /// </summary>
    class TwitterProxy
    {
        #region Private members
        /// <summary>
        /// The consumer key
        /// </summary>
        private string _consumerKey;
        /// <summary>
        /// The secret key
        /// </summary>
        private string _secretKey;
        /// <summary>
        /// Twitter's base URL
        /// </summary>
        private string _apiBaseUrl;
        /// <summary>
        /// The access token (OAuth 2)
        /// </summary>
        private string _accessToken;

        #endregion

        /// <summary>
        /// Initialises a new instance
        /// </summary>
        /// <param name="consumerKey">The consumer key</param>
        /// <param name="secretKey">The secret key</param>
        /// <param name="apiBaseUrl">The API's base URL</param>
        public TwitterProxy(string consumerKey, string secretKey, string apiBaseUrl)
        {
            _consumerKey = consumerKey;
            _secretKey = secretKey;
            _apiBaseUrl = apiBaseUrl;
        }
        /// <summary>
        /// Gets the access token
        /// </summary>
        private string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_accessToken))
                {
                    GetAccessToken();
                }
                return _accessToken;
            }
        }

        /// <summary>
        /// Gets the access token
        /// </summary>
        private void GetAccessToken()
        {
            //Token URL
            var oauth_url = _apiBaseUrl + "/oauth2/token";
            var headerFormat = "Basic {0}";
            var authHeader = string.Format(headerFormat,
                        Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(_consumerKey) + ":" +
                        Uri.EscapeDataString((_secretKey)))
                        ));

            var postBody = "grant_type=client_credentials";

            ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(oauth_url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }

            request.Headers.Add("Accept-Encoding", "gzip");

            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string respString = streamReader.ReadToEnd();
                var obj = JObject.Parse(respString);
                _accessToken = obj.Value<string>("access_token");
            }
        }

        /// <summary>
        /// Gets tweets for a user
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <param name="maxNbTweets">The max number of tweets to retieve</param>
        /// <returns>A list of tweets</returns>
        public List<Tweet> GetTweets(string userName, int maxNbTweets)
        {
            //Token URL
            var timeLineURL = _apiBaseUrl + "/1.1/statuses/user_timeline.json?screen_name={0}&count={1}";
            timeLineURL = string.Format(timeLineURL, userName, maxNbTweets);
            var authHeader = string.Format("Bearer {0}", AccessToken);

            ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(timeLineURL);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Headers.Add("Accept-Encoding", "gzip");

            List<Tweet> tweets = new List<Tweet>();
            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string respString = streamReader.ReadToEnd();
                var array = JArray.Parse(respString);
                foreach (var obj in array)
                {
                    tweets.Add(Tweet.ReadFromJObject(obj as JObject));
                }
            }
            return tweets;
        }
    }
}
