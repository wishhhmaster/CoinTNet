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
    /// Wrapper for the Bitstamp Proxy, to offer a unified interface
    /// </summary>
    class BitstampWrapper : Interfaces.IExchange
    {
        #region private members
        /// <summary>
        /// The Bitstamp proxy
        /// </summary>
        private BitstampAPI.BitstampProxy _proxy;
        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public BitstampWrapper()
        {
            var p = SecureStorage.GetEncryptedData<BitstampAPIParams>(SecuredDataKeys.BitstampAPI);
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("CoinTNet");
            string baseUrl = string.Empty;
            if (section != null && section.Count > 0)
            {
                baseUrl = section["bitstamp.APIBaseUrl"];
            }

            _proxy = new BitstampAPI.BitstampProxy(baseUrl, p.ClientID, p.APIKey, p.APISecret);
        }

        /// <summary>
        /// Gets a reference to the underlying proxy
        /// </summary>
        public BitstampAPI.BitstampProxy BitstampProxy
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
            return CallProxy(() => _proxy.GetTicker(), t => new Ticker
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
            return CallProxy(() => _proxy.GetBalance(), b =>
            {
                var bal = new Balance();
                bal.Balances[pair.Item1] = b.AvailableBTC;
                bal.Balances[pair.Item2] = b.AvailableUSD;
                bal.Fee = b.Fee;
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
            return CallProxy(() => _proxy.PlaceSellOrder(amount, price),
                o => new OrderDetails
                {
                    Amount = o.Amount,
                    DateTime = o.DateTime,
                    Id = o.Id,
                    Price = o.Price,
                    Type = o.Type == 0 ? OrderType.Buy : OrderType.Sell
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
            return CallProxy(() => _proxy.PlaceBuyOrder(amount, price),
                o => new OrderDetails
                {
                    Amount = o.Amount,
                    DateTime = o.DateTime,
                    Id = o.Id,
                    Price = o.Price,
                    Type = o.Type == 0 ? OrderType.Buy : OrderType.Sell
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
            return CallProxy(() => _proxy.GetTransactions(lastMin),
                tlist => new TransactionList
                {
                    Transactions = tlist.Transactions.Select((t => new DO.Exchanges.Transaction
                    {
                        Amount = t.Amount,
                        Price = t.Price,

                        ID = t.ID,
                        Timestamp = t.Timestamp,
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
            return CallProxy(() => _proxy.GetOrderBook(),
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
            return CallProxy(() => _proxy.GetOpenOrders(),
                openOrders => new OpenOrders
                {
                    List = openOrders.Orders.Select(o => new OrderDetails
                    {
                        Amount = o.Amount,
                        DateTime = o.DateTime,
                        Id = o.Id,
                        LimitPrice = o.LimitPrice,
                        Price = o.Price,
                        Type = o.Type == 0 ? OrderType.Buy : OrderType.Sell
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
        public CallResult<decimal> GetFee(CurrencyPair pair)
        {
            return CallProxy(() => _proxy.GetFee(), f => f);
        }

        /// <summary>
        /// Gets the unit for the fee for a given pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <param name="type">The type of order (buy/sell)</param>
        /// <returns></returns>
        public string GetFeeUnit(CurrencyPair pair, OrderType type)
        {
            return "USD";
        }
        /// <summary>
        /// Calls the proxy and converts the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="dataRetrievalFunc"></param>
        /// <param name="convFunc"></param>
        /// <returns></returns>
        private CallResult<T> CallProxy<T, T2>(Func<BitstampAPI.CallResult<T2>> dataRetrievalFunc, Func<T2, T> convFunc)
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
