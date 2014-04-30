using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BitcoinWisdomAPI
{
    /// <summary>
    /// Proxy to access bitcoin wisdom
    /// </summary>
    public class BitcoinWisdomProxy
    {
        #region Private members

        /// <summary>
        /// Bitcoinwisdom base url (for retrieving candles)
        /// </summary>
        private string _baseURL;

        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        /// <param name="baseUrl">The base url for retrieving data</param>
        public BitcoinWisdomProxy(string baseUrl = "")
        {
            _baseURL = string.IsNullOrEmpty(baseUrl) ? "https://s2.bitcoinwisdom.com" : baseUrl;
        }
        /// <summary>
        /// Retrieves a list of candles
        /// </summary>
        /// <param name="from"></param>
        /// <param name="periodInMin"></param>
        /// <param name="market"></param>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        public IList<Candle> GetCandles(DateTimeOffset from, int periodInMin, string market, string item1, string item2)
        {
            string command = string.Format("/period?step={0}&symbol={1}{2}{3}&nonce={4}", periodInMin * 60, market.ToLower(), item1.ToLower(), item2.ToLower(), DateTime.Now.Ticks);
            var resultStr = SendGETRequest(command);
            if (string.IsNullOrEmpty(resultStr))
            {
                return new List<Candle>();
            }
            var array = JArray.Parse(resultStr);

            List<Candle> candles = new List<Candle>();
            foreach (var c in array)
            {
                var timeStamp = c.Value<uint>(0);
                var candle = new Candle
                {
                    DateTime = UnixTime.ConvertToDateTime(timeStamp),
                    TimeStamp = timeStamp,
                    Close = c.Value<decimal>(4),
                    Open = c.Value<decimal>(3),
                    High = c.Value<decimal>(5),
                    Low = c.Value<decimal>(6)
                };
                candles.Add(candle);
            }

            return candles.Where(c => c.DateTime > from).ToList();

        }

        /// <summary>
        /// Sends a GET Request
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private string SendGETRequest(string command)
        {
            command = _baseURL + command;
            HttpWebRequest req = HttpWebRequest.Create(command) as HttpWebRequest;
            req.Method = "GET";
            req.Headers.Add("Accept-Encoding", "gzip,deflate");
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            var response = req.GetResponse();
            var resStream = response.GetResponseStream();
            using (var resStreamReader = new StreamReader(resStream))
            {
                string resString = resStreamReader.ReadToEnd();
                return resString;
            }
        }

    }

    /// <summary>
    /// Represents a candle
    /// </summary>
    public class Candle
    {
        public DateTimeOffset DateTime { get; set; }
        public uint TimeStamp { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
    }
}
