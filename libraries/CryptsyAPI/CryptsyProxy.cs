using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CryptsyAPI
{
    /// <summary>
    /// Defines order types
    /// </summary>
    public class OrderTypes
    {
        /// <summary>
        /// Buy order
        /// </summary>
        public const string Buy = "Buy";
        /// <summary>
        /// Sell order
        /// </summary>
        public const string Sell = "Sell";
    }

    /// <summary>
    /// Constant for error codes
    /// </summary>
    public class ErrorCodes
    {
        /// <summary>
        /// We don't handle that error 
        /// </summary>
        public const int UnknownError = 0;
        /// <summary>
        /// Invalid/empty API keys
        /// </summary>
        public const int InvalidAPIKeys = -1;
    }

    /// <summary>
    /// Proxy for Cryptsy's API
    /// </summary>
    public class CryptsyProxy
    {
        #region Private members

        /// <summary>
        /// The user's public api key
        /// </summary>
        private string _publicKey;
        /// <summary>
        /// The user's private api key
        /// </summary>
        private string _privateKey;
        /// <summary>
        /// Cryptsy's public api base url
        /// </summary>
        private string _publicAPIBaseUrl;
        /// <summary>
        /// Cryptsy's private api base url
        /// </summary>
        private string _privateAPIBaseUrl;
        /// <summary>
        /// Used to compute hmac signature to sign data sent to the private API
        /// </summary>
        private HMACSHA512 _hmac;
        /// <summary>
        /// Holds the cookies returned by the server, as recommended on the APi's page
        /// </summary>
        private CookieContainer _container;
        /// <summary>
        /// Diff in hours between Cryptsy's server time and UTC time
        /// </summary>
        private int _hoursDiffToUtc;
        /// <summary>
        /// Message displayed when there are no/invalid API keys
        /// </summary>
        private const string NoKeysErrMsg = "Unable to Authorize Request - Check Your Post Data";
        #endregion

        /// <summary>
        /// Initialises a new instance of the class with specified parameters
        /// </summary>
        /// <param name="apiKey">The user's public api key</param>
        /// <param name="privateKey">The user's private api key</param>
        /// <param name="publicAPIBaseUrl">The public API's base url</param>
        /// <param name="privateAPIBaseUrl">The private API's base url</param>
        public CryptsyProxy(string apiKey, string privateKey, string publicAPIBaseUrl, string privateAPIBaseUrl, int hoursDiffToUtc = 4)
        {
            _hoursDiffToUtc = hoursDiffToUtc;
            _publicKey = apiKey;
            _privateKey = privateKey;
            _publicAPIBaseUrl = publicAPIBaseUrl;
            _privateAPIBaseUrl = privateAPIBaseUrl;
            _hmac = new HMACSHA512(Encoding.ASCII.GetBytes(_privateKey != null ? _privateKey : string.Empty));
            _container = new CookieContainer();
        }

        /// <summary>
        /// Gets the ticker for a given currency pair
        /// </summary>
        /// <param name="currencyPair"></param>
        /// <returns></returns>
        public CallResult<Ticker> GetTicker(CurrencyPair currencyPair)
        {
            return MakeGetRequest<Ticker>("?method=singlemarketdata&marketid=" + currencyPair.ID, result =>
                {
                    return Ticker.CreateFromJObject(result["markets"][currencyPair.Item1] as JObject);
                });
        }

        /// <summary>
        /// Gets the order book for a given currency pair
        /// </summary>
        /// <param name="currencyPair"></param>
        /// <returns></returns>
        public CallResult<OrderBook> GetOrderBook(CurrencyPair currencyPair)
        {
            return MakeGetRequest<OrderBook>("?method=singleorderdata&marketid=" + currencyPair.ID, result => OrderBook.CreateFromJObject(result[currencyPair.Item1] as JObject, true));
        }

        /// <summary>
        /// Retrieves a list of all currency pairs traded in Cryptsy
        /// </summary>
        /// <returns></returns>
        public CallResult<CurrencyPair[]> GetAllCurrencyPairs()
        {
            return MakeGetRequest<CurrencyPair[]>("?method=marketdatav2", result =>
                {
                    List<CurrencyPair> pairs = new List<CurrencyPair>();

                    foreach (var ma in result["markets"])
                    {
                        var m = ma.First;
                        var c = new CurrencyPair
                        {
                            ID = m.Value<string>("marketid"),
                            Item1 = m.Value<string>("primarycode"),
                            Item2 = m.Value<string>("secondarycode"),
                        };
                        pairs.Add(c);
                    }
                    return pairs.ToArray();
                });
        }

        /// <summary>
        /// Gets the order book for a given currency pair
        /// </summary>
        /// <param name="currencyPair"></param>
        /// <returns></returns>
        public CallResult<OrderBook> GetFullOrderBook(CurrencyPair currencyPair)
        {
            return MakePostRequest<OrderBook>("marketorders", result => OrderBook.CreateFromJObject(result as JObject, false), new Dictionary<string, string> { { "marketid", currencyPair.ID } });
        }

        /// <summary>
        /// Retrieves the user's balance and server info (time)
        /// </summary>
        /// <returns></returns>
        public CallResult<Info> GetInfo()
        {
            return MakePostRequest("getinfo", result => Info.CreateFromJObject(result as JObject));
        }

        /// <summary>
        /// Returns the list of open orders for a given currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns></returns>
        public CallResult<List<OrderDetails>> GetOpenOrders(CurrencyPair pair)
        {
            return MakePostRequest("myorders", result =>
                {
                    List<OrderDetails> orders = new List<OrderDetails>();
                    foreach (var o in result)
                    {
                        orders.Add(OrderDetails.CreateFromJObject(o as JObject, _hoursDiffToUtc));
                    }
                    return orders;
                },
                new Dictionary<string, string> { { "marketid", pair.ID } });
        }


        /// <summary>
        /// Cancels an open order
        /// </summary>
        /// <param name="orderId">The order's ID</param>
        /// <returns></returns>
        public CallResult<bool> CancelOrder(long orderId)
        {
            return MakePostRequest("cancelorder", r => true, new Dictionary<string, string> { { "orderid", orderId.ToString() } });
        }


        /// <summary>
        /// Places a trade order
        /// </summary>
        /// <param name="price">The order's price</param>
        /// <param name="amount">The amount</param>
        /// <param name="orderType">The type of order (Buy/Sell)</param>
        /// <param name="pair">The currency pair</param>
        /// <returns></returns>
        public CallResult<long> PlaceOrder(decimal price, decimal amount, string orderType, CurrencyPair pair)
        {
            return MakePostRequest("createorder", result => 10L,
                new Dictionary<string, string>
                {
                    {"price", price.ToString(CultureInfo.InvariantCulture)},
                    {"quantity", amount.ToString(CultureInfo.InvariantCulture)},
                    {"marketid", pair.ID},
                    {"ordertype", orderType}
                    
                });
        }

        /// <summary>
        /// Gets the 1000 most recent trades for a given currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns></returns>
        public CallResult<List<Trade>> GetRecentTrades(CurrencyPair pair)
        {
            return MakePostRequest("markettrades", result =>
            {
                List<Trade> orders = new List<Trade>();
                foreach (var o in result)
                {
                    orders.Add(Trade.CreateFromJObject(o as JObject, _hoursDiffToUtc));
                }
                return orders;
            },
            new Dictionary<string, string> { { "marketid", pair.ID } });
        }

        /// <summary>
        /// Makes a GET request and parses the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="conversion"></param>
        /// <returns></returns>
        private CallResult<T> MakeGetRequest<T>(string url, Func<JToken, T> conversion)
        {
            try
            {
                var resultStr = SendGETRequest(url);
                var result = JToken.Parse(resultStr);
                return ParseCallResult(result, r => conversion(r));
            }
            catch (Exception e)
            {
                return new CallResult<T> { ErrorMessage = e.Message, Exception = e };
            }
        }
        /// <summary>
        /// Sends a GET Request
        /// </summary>
        /// <param name="url">The relative url to which the request will be sent</param>
        /// <returns>The request's result</returns>
        private string SendGETRequest(string url)
        {
            url = _publicAPIBaseUrl + url;
            var request = WebRequest.Create(new Uri(url));
            var response = request.GetResponse();
            using (var resStream = response.GetResponseStream())
            {
                using (var resStreamReader = new StreamReader(resStream))
                {
                    string resString = resStreamReader.ReadToEnd();
                    return resString;
                }
            }
        }

        /// <summary>
        /// Parses the result of an API call
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private CallResult<T> ParseCallResult<T>(JToken token, Func<JToken, T> func)
        {
            JToken val;
            string error = null;
            var result = token as JObject;

            if (result != null && result.TryGetValue("success", out val))
            {
                int status = Convert.ToInt32(((val as JValue).Value));
                if (status != 1)
                {
                    error = "Unknown error";
                    if (result.TryGetValue("error", out val))
                    {
                        error = (string)(string)(val as JValue).Value;
                    }
                }

            }

            var r = new CallResult<T>
            {
                ErrorMessage = error,
                ErrorCode = error == NoKeysErrMsg ? ErrorCodes.InvalidAPIKeys : ErrorCodes.UnknownError,
                Result = string.IsNullOrEmpty(error) ? func(token["return"]) : default(T)
            };
            return r;
        }


        /// <summary>
        /// Makes a Post request and returns the response as an object wrapped in a CallResult
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="url">The relative URL to make the call to</param>
        /// <param name="conversion">The function used to convert the response into an object</param>
        /// <param name="postArgs">The extra parameters to pass to the POST request</param>
        /// <returns></returns>
        private CallResult<T> MakePostRequest<T>(string method, Func<JToken, T> conversion, Dictionary<string, string> postArgs = null)
        {
            try
            {
                var resultStr = SendPostRequest(method, postArgs);
                var result = JToken.Parse(resultStr);
                return ParseCallResult(result, r => conversion(r));
            }
            catch (Exception e)
            {
                return new CallResult<T>
                {
                    ErrorMessage = e.Message,
                    Exception = e,
                };
            }
        }

        /// <summary>
        /// Sends a post request to the private API
        /// </summary>
        /// <param name="method">The method to call</param>
        /// <param name="postArgs">Extra post parameters</param>
        /// <returns></returns>
        private string SendPostRequest(string method, Dictionary<string, string> postArgs)
        {
            string url = _privateAPIBaseUrl;
            if (postArgs == null)
            {
                postArgs = new Dictionary<string, string>();
            }
            postArgs["nonce"] = DateTime.Now.Ticks.ToString();
            postArgs["method"] = method;
            var dataStr = BuildPostData(postArgs);
            var postDataBytes = Encoding.ASCII.GetBytes(dataStr);
            var request = WebRequest.Create(new Uri(url));

            var hash = _hmac.ComputeHash(postDataBytes);
            var signature = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            request.Headers.Add("Key", _publicKey);
            request.Headers.Add("Sign", signature.ToLower());

            request.Method = "POST";
            request.Timeout = 15000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataBytes.Length;
            (request as HttpWebRequest).CookieContainer = _container;

            using (var reqStream = request.GetRequestStream())
            {
                reqStream.Write(postDataBytes, 0, postDataBytes.Length);
                reqStream.Close();
            }
            var response = request.GetResponse();
            using (var resStream = response.GetResponseStream())
            {
                using (var resStreamReader = new StreamReader(resStream))
                {
                    string resString = resStreamReader.ReadToEnd();
                    return resString;
                }
            }
        }

        /// <summary>
        /// Builds post data for a request
        /// </summary>
        /// <param name="dataDic"></param>
        /// <returns></returns>
        private static string BuildPostData(Dictionary<string, string> dataDic)
        {
            var p = dataDic.Keys.Select(key => String.Format("{0}={1}", key, HttpUtility.UrlEncode(dataDic[key]))).ToArray();
            return string.Join("&", p);
        }
    }
}
