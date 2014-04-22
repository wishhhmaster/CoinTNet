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

namespace BitstampAPI
{
    /// <summary>
    /// Proxy for making calls to the  Bitsamp API
    /// </summary>
    public class BitstampProxy
    {
        #region Private Members
        /// <summary>
        /// The Secret key
        /// </summary>
        private string _secretKey;
        /// <summary>
        /// The API Key
        /// </summary>
        private string _apiKey;
        /// <summary>
        /// The client ID
        /// </summary>
        private string _clientID;
        /// <summary>
        /// The API's base URL
        /// </summary>
        private string _baseURL;
        /// <summary>
        /// The current fee
        /// </summary>
        private decimal _fee = 0.0M;
        /// <summary>
        /// Used to compute hmac signature to sign data sent to the private API
        /// </summary>
        private HMACSHA256 _hmac;
        #endregion

        /// <summary>
        /// Initialises a new instance of the BitstampProxy class
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="clientID"></param>
        /// <param name="apiKey"></param>
        /// <param name="privateKey"></param>
        public BitstampProxy(string baseURL, string clientID, string apiKey, string privateKey)
        {
            _clientID = clientID;
            _apiKey = apiKey;
            _secretKey = privateKey;
            _baseURL = baseURL;

            _hmac = new HMACSHA256(UTF8Encoding.UTF8.GetBytes(_secretKey != null ? _secretKey : string.Empty));
        }

        /// <summary>
        /// Gets Bitstamp's ticker
        /// </summary>
        /// <returns></returns>
        public CallResult<Ticker> GetTicker()
        {
            return MakeGetRequest<Ticker>("ticker/", result => Ticker.CreateFromJObject(result as JObject));
        }

        /// <summary>
        /// Gets the full order book
        /// </summary>
        /// <returns></returns>
        public CallResult<OrderBook> GetOrderBook()
        {
            return MakeGetRequest<OrderBook>("order_book/", result => OrderBook.CreateFromJObject(result as JObject));
        }

        /// <summary>
        /// Retrives all public transactions for the past minute/hour
        /// </summary>
        /// <param name="lastMinuteOnly">True if we want the transactions for the last minute only</param>
        /// <returns></returns>
        public CallResult<TransactionList> GetTransactions(bool lastMinuteOnly = false)
        {
            string url = "transactions/" + (lastMinuteOnly ? "?time=minute" : string.Empty);
            return MakeGetRequest(url, result => TransactionList.ReadFromJObject(result as JArray));
        }

        /// <summary>
        /// Gets the user's balance
        /// </summary>
        /// <returns></returns>
        public CallResult<Balance> GetBalance()
        {
            return MakePostRequest("balance/", r =>
            {
                Balance balance = Balance.CreateFromJObject(r as JObject);
                _fee = balance != null ? balance.Fee : 0.0M;
                return balance;
            });
        }

        /// <summary>
        /// Places a buy order
        /// </summary>
        /// <param name="amount">The amount of BTCs to buy</param>
        /// <param name="price">The price  per BTC</param>
        /// <returns></returns>
        public CallResult<OrderDetails> PlaceBuyOrder(decimal amount, decimal price)
        {
            return MakePostRequest("buy/",
                r => OrderDetails.CreateFromJObject(r as JObject),

                new Dictionary<string, string> {
                {"amount", amount.ToString(CultureInfo.InvariantCulture)},
                    {"price",price.ToString(CultureInfo.InvariantCulture)}
                });
        }

        /// <summary>
        /// Places a sell order
        /// </summary>
        /// <param name="amount">The amount of BTCs to sell</param>
        /// <param name="price">The price per BTC</param>
        /// <returns></returns>
        public CallResult<OrderDetails> PlaceSellOrder(decimal amount, decimal price)
        {
            return MakePostRequest("sell/",
                r => OrderDetails.CreateFromJObject(r as JObject),

                new Dictionary<string, string> {
                {"amount", amount.ToString(CultureInfo.InvariantCulture)},
                    {"price",price.ToString(CultureInfo.InvariantCulture)}
                });
        }

        /// <summary>
        /// Cancels an open order
        /// </summary>
        /// <param name="orderId">The order's ID</param>
        /// <returns></returns>
        public CallResult<bool> CancelOrder(long orderId)
        {
            var args = GetAuthenticationArgs();
            args["id"] = orderId.ToString();
            var resultStr = SendPostRequest("cancel_order/", args);
            var result = resultStr == "true" ? new JObject() : JObject.Parse(resultStr);
            return ParseCallResult<bool>(result, r => resultStr == "true");
        }

        /// <summary>
        /// Gets the fee associated with the user's account
        /// </summary>
        /// <returns></returns>
        public CallResult<decimal> GetFee()
        {
            if (decimal.Compare(_fee, 0.0M) == 0)
            {
                GetBalance();
            }
            return new CallResult<decimal>(_fee);
        }
        /// <summary>
        /// Gets a list of all the user's open orders
        /// </summary>
        /// <returns></returns>
        public CallResult<OpenOrdersContainer> GetOpenOrders()
        {
            return MakePostRequest("open_orders/", t => OpenOrdersContainer.CreateFromJObject(t as JArray));
        }

        #region Private methods

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

        /// <summary>
        /// Parses the result of a call to the API and converts that result into an object, wrapped in a CallResult object
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

            if (result != null && result.TryGetValue("error", out val))
            {
                var jValue = val as JValue;
                if (jValue != null)
                {
                    error = (string)jValue.Value;
                }
                else
                {
                    error = string.Join("\n", val["__all__"].Select(jt => ((JValue)jt).Value));
                }
            }

            var r = new CallResult<T>
            {
                ErrorMessage = error,
                Result = string.IsNullOrEmpty(error) ? func(token) : default(T)
            };
            return r;
        }

        /// <summary>
        /// Sends a post request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private string SendPostRequest(string url, Dictionary<string, string> args)
        {
            url = _baseURL + url;
            var dataStr = BuildPostData(args);
            var data = Encoding.ASCII.GetBytes(dataStr);
            var request = WebRequest.Create(new Uri(url));

            request.Method = "POST";
            request.Timeout = 15000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
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
        /// Sends a GET Request
        /// </summary>
        /// <param name="url">The relative url to which the request will be sent</param>
        /// <returns>The request's result</returns>
        private string SendGETRequest(string url)
        {
            url = _baseURL + url;
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
        /// Makes a GET request and returns the response as an object wrapped in a CallResult
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="url">The relative URL to make the call to</param>
        /// <param name="conversion">The function used to convert the response into an object</param>
        /// <returns></returns>
        private CallResult<T> MakeGetRequest<T>(string url, Func<JToken, T> conversion)
        {
            try
            {
                var resultStr = SendGETRequest(url);
                var result = JToken.Parse(resultStr);
                return ParseCallResult(result, r => conversion(result));
            }
            catch (Exception e)
            {
                return new CallResult<T> { ErrorMessage = e.Message, Exception = e };
            }
        }

        /// <summary>
        /// Makes a Post request and returns the response as an object wrapped in a CallResult
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="url">The relative URL to make the call to</param>
        /// <param name="conversion">The function used to convert the response into an object</param>
        /// <param name="extraArgs">The extra parameters to pass to the POST request</param>
        /// <returns></returns>
        private CallResult<T> MakePostRequest<T>(string url, Func<JToken, T> conversion, Dictionary<string, string> extraArgs = null)
        {
            try
            {
                var args = GetAuthenticationArgs();
                if (extraArgs != null)
                {
                    foreach (var kvp in extraArgs)
                    {
                        args[kvp.Key] = kvp.Value;
                    }
                }
                var resultStr = SendPostRequest(url, args);
                var result = JToken.Parse(resultStr);
                return ParseCallResult(result, r => conversion(result));
            }
            catch (Exception e)
            {
                return new CallResult<T> { ErrorMessage = e.Message, Exception = e };
            }
        }

        /// <summary>
        /// Returns a dictionary containing the parameters required for Bitstamp authentication
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetAuthenticationArgs()
        {
            string nonce = DateTime.Now.Ticks.ToString();
            string message = nonce + _clientID + _apiKey;
            var hash = _hmac.ComputeHash(UTF8Encoding.UTF8.GetBytes(message));
            string signature = BitConverter.ToString(hash).Replace("-", string.Empty).ToUpper();

            var args = new Dictionary<string, string>()
            {
                { "key", _apiKey },
                { "nonce", nonce },
                {"signature", signature}
            };
            return args;
        }
        #endregion
    }
}
