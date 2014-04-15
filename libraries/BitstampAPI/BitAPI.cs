using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

// This libarary is written by Haasonline 2013
// You may use this open-source code for personal use only

namespace BitstampAPI
{
    public class BitAPI
    {
        // Private variable
        private string _apiKey = "";
        private string _password = "";
        private decimal _fee = 0.0M;
        const int _antifloodProtectionSec = -6; // Bitstamp Exchange: no more then 600 requests per 10 minutes (my advice: buffer simple pages 6sec at least)
        private List<CachedPage> _changedPages = new List<CachedPage>();

        // Public variable
        public string LastMessage = "";

        // Constructor
        public BitAPI(string apiKey)
        {
            _apiKey = apiKey;
        }

        // Functions
        public void SetNew(string customerID, string password)
        {
            _apiKey = customerID;
            _password = password;
        }
        public Ticker GetTicker()
        {
            var resultStr = SimpleQuery("https://www.bitstamp.net/api/ticker/");
            LastMessage = resultStr; 

            var result = JObject.Parse(resultStr);

            return Ticker.ReadFromJObject(result);
        }
        public Balance GetBalance()
        {
            var args = new Dictionary<string, string>()
            {
                { "key", _apiKey },
                { "password", _password }
            };
            var resultStr = Query("https://www.bitstamp.net/api/balance/", args);
            LastMessage = resultStr;

            var result = JObject.Parse(resultStr);

            Balance retResult = Balance.ReadFromJObject(result);

            if (retResult != null)
            {
                _fee = retResult.fee;
            }
            else
            {
                _fee = 0.0M;
            }
            return retResult;
        }
        public OrderDetails PlaceBuyOrder(decimal amount, decimal price)
        {
            var args = new Dictionary<string, string>()
            {
                { "user", _apiKey },
                { "password", _password },
                { "amount", amount.ToString() },
                { "price", price.ToString() }
            };

            var resultStr = Query("https://www.bitstamp.net/api/buy/", args);
            LastMessage = resultStr;

            var result = JObject.Parse(resultStr);

            return OrderDetails.ReadFromJObject(result);
        }
        public OrderDetails PlaceSellOrder(decimal amount, decimal price)
        {
            var args = new Dictionary<string, string>()
            {
                { "user", _apiKey },
                { "password", _password },
                { "amount", amount.ToString() },
                { "price", price.ToString() }
            };
            var resultStr = Query("https://www.bitstamp.net/api/sell/", args);
            LastMessage = resultStr;

            var result = JObject.Parse(resultStr);

            return OrderDetails.ReadFromJObject(result);
        }
        public decimal GetFee()
        {
            if (decimal.Compare(_fee, 0.0M) == 0) { GetBalance(); }
            return _fee;
        }
        public OpenOrders GetOpenOrders()
        {
            var args = new Dictionary<string, string>()
            {
                { "user", _apiKey },
                { "password", _password }
            };

            string resultStr = Query("https://www.bitstamp.net/api/open_orders/", args);
            LastMessage = resultStr;

            return OpenOrders.ReadFromJObject(JArray.Parse(resultStr));    
        }
        public Depth GetOrderBook()
        {
            var resultStr = SimpleQuery("https://www.bitstamp.net/api/order_book/");
            LastMessage = resultStr;

            var result = JObject.Parse(resultStr);

            return Depth.ReadFromJObject(result);
        }

        public TransactionList GetTransactions()
        {
            var resultStr = SimpleQuery("https://www.bitstamp.net/api/transactions/");
            LastMessage = resultStr;

            var result = JArray.Parse(resultStr);

            return TransactionList.ReadFromJObject(result);
        }
        
        // Execute commands
        static string BuildPostData(Dictionary<string, string> d)
        {
            string s = "";
            for (int i = 0; i < d.Count; i++)
            {
                var item = d.ElementAt(i);
                var key = item.Key;
                var val = item.Value;

                s += String.Format("{0}={1}", key, HttpUtility.UrlEncode(val));

                if (i != d.Count - 1)
                    s += "&";
            }
            return s;
        }
        string Query(string command, Dictionary<string, string> args)
        {
            var dataStr = BuildPostData(args);
            var data = Encoding.ASCII.GetBytes(dataStr);

            // Anti-flood (disabled here)
            string resString = "";
            //bool containsReleventData = false;
            //foreach (var item in _changedPages)
            //{
            //    if (DateTime.Compare(item.TimeStamp, DateTime.Now.AddSeconds(_antifloodProtectionSec)) > 0)
            //    {
            //        containsReleventData = true;

            //        if (item.RequestURL == command)
            //        {
            //            resString = item.CahedData;
            //            break;
            //        }
            //    }
            //}

            //// Cache cleanup
            //if (containsReleventData == false)
            //    _changedPages.Clear();

            //if (resString == "")
            //{
                var request = WebRequest.Create(new Uri(command));
                if (request == null)
                    throw new Exception("Non HTTP WebRequest");

                request.Method = "POST";
                request.Timeout = 15000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                var reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                var response = request.GetResponse();
                var resStream = response.GetResponseStream();
                var resStreamReader = new StreamReader(resStream);
                resString = resStreamReader.ReadToEnd();

                //_changedPages.Add(new CachedPage { TimeStamp = DateTime.Now, RequestURL = command, CahedData = resString });
            //}

            return resString;
        }
        string SimpleQuery(string command)
        {
            var request = WebRequest.Create(new Uri(command));
            if (request == null)
                throw new Exception("Non HTTP WebRequest");

            // Anti-flood
            string resString = "";
            bool containsReleventData = false;
            foreach (var item in _changedPages)
            {
                if (DateTime.Compare(item.TimeStamp, DateTime.Now.AddSeconds(_antifloodProtectionSec)) > 0)
                {
                    containsReleventData = true;

                    if (item.RequestURL == command)
                    {
                        resString = item.CachedData;
                        break;
                    }
                }
            }

            if (containsReleventData == false)
                _changedPages.Clear(); 

            if (resString == "")
            {
                var response = request.GetResponse();
                var resStream = response.GetResponseStream();
                var resStreamReader = new StreamReader(resStream);
                resString = resStreamReader.ReadToEnd();

                _changedPages.Add(new CachedPage { TimeStamp = DateTime.Now, RequestURL = command, CachedData = resString });
            }

            return resString;
        }
    }

    class CachedPage
    {
        public DateTime TimeStamp { get; set; }
        public string RequestURL { get; set; }
        public string CachedData { get; set; }
    }
}
