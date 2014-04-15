using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BitcoinWisdomAPI
{
    public class BitcoinWisdomProxy
    {
        private string _baseURL;
        public BitcoinWisdomProxy(string baseUrl = "")
        {
            _baseURL = string.IsNullOrEmpty(baseUrl) ? "https://s2.bitcoinwisdom.com" : baseUrl;
            //https://s2.bitcoinwisdom.com
        }
        public IList<Candle> GetCandles(DateTimeOffset from, int periodInMin, string market, string item1, string item2)
        {
            string command = string.Format("/period?step={0}&symbol={1}{2}{3}&nonce={4}", periodInMin * 60, market.ToLower(), item1.ToLower(), item2.ToLower(), DateTime.Now.Ticks);
            var resultStr = SendGETRequest(command);
            var array = JArray.Parse(resultStr);

            List<Candle> candles = new List<Candle>();
            foreach (var c in array)
            {
                var timeStamp = c.Value<uint>(0);
                var candle = new Candle
                {
                    DateTime = UnixTime.ConvertToDateTime(timeStamp),
                    TimeStamp=timeStamp,
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
            //var request = WebRequest.Create(new Uri(command));

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
