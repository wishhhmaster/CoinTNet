using CoinTNet.Common.Constants;
using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using CoinTNet.DO.Security;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace CoinTNet.DAL.Exchanges
{
    /// <summary>
    /// Wrapper for the Cryptsy Proxy, to offer a unified interface
    /// </summary>
    class CryptsyWrapper : Interfaces.IExchange
    {
        #region private members
        /// <summary>
        /// The Cryptsy proxy
        /// </summary>
        private CryptsyAPI.CryptsyProxy _proxy;
        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public CryptsyWrapper()
        {
            var p = SecureStorage.GetEncryptedData<CryptsyAPIParams>(SecuredDataKeys.CryptsyAPI);
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("CoinTNet");
            string publicAPIBaseUrl = string.Empty, privateAPIBaseUrl = string.Empty, serverTimeZone = string.Empty;
            if (section != null && section.Count > 0)
            {
                publicAPIBaseUrl = section["cryptsy.publicAPIBaseUrl"];
                privateAPIBaseUrl = section["cryptsy.privateAPIBaseUrl"];
                serverTimeZone = section["cryptsy.serverTimeZone"];
            }

            var provider = TimeZoneInfo.FindSystemTimeZoneById(serverTimeZone);
            TimeSpan providerOffset = provider.GetUtcOffset(DateTimeOffset.UtcNow);
            int hoursDiffToUtc = -(int)providerOffset.TotalHours;

            _proxy = new CryptsyAPI.CryptsyProxy(p.PublicKey, p.SecretKey, publicAPIBaseUrl, privateAPIBaseUrl, hoursDiffToUtc);
        }

        /// <summary>
        /// Gets a reference to the underlying proxy
        /// </summary>
        public CryptsyAPI.CryptsyProxy CryptsyProxy
        {
            get { return _proxy; }
        }

        /// <summary>
        /// Gets the ticker for a given currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>The ticker</returns>
        public CallResult<Ticker> GetTicker(CurrencyPair pair)
        {
            return CallProxy(() => _proxy.GetTicker(pair.ToCryptsyPair()), t => new Ticker
            {
                Ask = t.Ask,
                Bid = t.Bid,
                High = t.High,
                Low = t.Low,
                Last = t.Last
            });
        }

        /// <summary>
        /// Gets the balance for a given currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns></returns>
        public CallResult<Balance> GetBalance(CurrencyPair pair)
        {
            return CallProxy(() => _proxy.GetInfo(), b =>
            {
                var bal = new Balance();
                bal.Balances[pair.Item1] = b.BalancesAvailable[pair.Item1];
                bal.Balances[pair.Item2] = b.BalancesAvailable[pair.Item2];
                return bal;
            });
        }
        /// <summary>
        /// Places a sell order
        /// </summary>
        /// <param name="amount">The amount of units to buy</param>
        /// <param name="price">The price per unit</param>
        /// <param name="pair">The currency pair</param>
        /// <returns>Detaisl about the new order</returns>
        public CallResult<OrderDetails> PlaceSellOrder(decimal amount, decimal price, CurrencyPair pair)
        {
            return CallProxy(() => _proxy.PlaceOrder(price, amount, CryptsyAPI.OrderTypes.Sell, pair.ToCryptsyPair()),
                o => new OrderDetails
                {
                    Amount = amount,
                    DateTime = DateTime.UtcNow,
                    Id = o,
                    Price = price,
                    Type = OrderType.Sell
                });
        }

        /// <summary>
        /// Places a buy order
        /// </summary>
        /// <param name="amount">The amount of units to buy</param>
        /// <param name="price">The price per unit</param>
        /// <param name="pair">The currency pair</param>
        /// <returns>Detaisl about the new order</returns>
        public CallResult<OrderDetails> PlaceBuyOrder(decimal amount, decimal price, CurrencyPair pair)
        {
            return CallProxy(() => _proxy.PlaceOrder(price, amount, CryptsyAPI.OrderTypes.Buy, pair.ToCryptsyPair()),
               o => new OrderDetails
               {
                   Amount = amount,
                   DateTime = DateTime.UtcNow,
                   Id = o,
                   Price = price,
                   Type = OrderType.Sell
               });
        }

        /// <summary>
        /// Gets recent transactions
        /// </summary>
        /// <param name="lastMin"></param>
        /// <param name="pair">The currency pair</param>
        /// <returns>The most recent transactions</returns>
        public CallResult<TransactionList> GetTransactions(bool lastMin, CurrencyPair pair)
        {
            return CallProxy(() => _proxy.GetRecentTrades(pair.ToCryptsyPair()),
                tlist => new TransactionList
                {
                    Transactions = tlist.Select((t => new DO.Exchanges.Transaction
                    {
                        Amount = t.Amount,
                        Price = t.Price,
                        ID = t.ID,
                        Timestamp = BitstampAPI.UnixTimeHelper.GetFromDateTime(t.Date),
                        Date = t.Date
                    })).ToList()
                });
        }

        /// <summary>
        /// Gets the order book for a currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>The order book (bids/asks)</returns>
        public CallResult<OrderBook> GetOrderBook(CurrencyPair pair)
        {
            return CallProxy(() => _proxy.GetOrderBook(pair.ToCryptsyPair()),
                o => new OrderBook
                {
                    Asks = o.Asks.Select(a => new SimpleOrderInfo
                    {
                        Amount = a.Amount,
                        Price = a.Price
                    }).ToList(),

                    Bids = o.Bids.Select(b => new SimpleOrderInfo
                    {
                        Amount = b.Amount,
                        Price = b.Price
                    }).ToList(),
                }
                );
        }

        /// <summary>
        /// Gets all open orders
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>A list of open orders</returns>
        public CallResult<OpenOrders> GetOpenOrders(CurrencyPair pair)
        {
            return CallProxy(() => _proxy.GetOpenOrders(pair.ToCryptsyPair()),
                openOrders => new OpenOrders
                {
                    List = openOrders.Select(o => new OrderDetails
                    {
                        Amount = o.Amount,
                        DateTime = o.DateTime,
                        Id = o.Id,
                        Price = o.Price,
                        Type = o.Type == CryptsyAPI.OrderTypes.Buy ? OrderType.Buy : OrderType.Sell
                    }).ToList()
                });
        }

        /// <summary>
        /// Cancels an order
        /// </summary>
        /// <param name="orderId">The Id of the order to cancel</param>
        /// <returns>true if the order was cancelled</returns>
        public CallResult<bool> CancelOrder(long orderId)
        {
            return CallProxy(() => _proxy.CancelOrder(orderId), b => b);
        }
        /// <summary>
        /// Gets the fee associated to a currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>The fee</returns>
        public CallResult<Fee> GetFee(CurrencyPair pair)
        {
            return new CallResult<Fee>(new Fee
            {
                BuyFee = 0.2m,
                SellFee = 0.3m
            });
        }

        /// <summary>
        /// Gets the unit for the fee for a given pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <param name="type">The type of order (buy/sell)</param>
        /// <returns></returns>
        public string GetFeeUnit(CurrencyPair pair, OrderType type)
        {
            return pair.Item2;
        }

        /// <summary>
        /// Returns the list of currency pairs available on the exchange
        /// </summary>
        /// <returns>A list of currency pairs</returns>
        public CallResult<CurrencyPair[]> GetCurrencyPairs()
        {
            return CallProxy(() => _proxy.GetAllCurrencyPairs(), list =>
                {
                    return list.Select(p => new CurrencyPair
                        (p.Item1, p.Item2, p.ID)).ToArray();
                });

        }

        /// <summary>
        /// Calls the proxy and converts the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="dataRetrievalFunc"></param>
        /// <param name="convFunc"></param>
        /// <returns></returns>
        private CallResult<T> CallProxy<T, T2>(Func<CryptsyAPI.CallResult<T2>> dataRetrievalFunc, Func<T2, T> convFunc)
        {
            var callRes = dataRetrievalFunc();
            if (callRes.Success)
            {
                return new CallResult<T>(convFunc(callRes.Result));
            }
            return new CallResult<T>(callRes.ErrorMessage) { Exception = callRes.Exception };
        }
    }

}
