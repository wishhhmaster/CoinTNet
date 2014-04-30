using BtcE;
using CoinTNet.Common.Constants;
using CoinTNet.DAL.Interfaces;
using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using CoinTNet.DO.Security;
using System;
using System.Linq;
using DE = CoinTNet.DO.Exchanges;

namespace CoinTNet.DAL.Exchanges
{
    /// <summary>
    /// Wrapper for the BTC-e proxy
    /// </summary>
    class BtceWrapper : IExchange
    {
        #region Private members
        /// <summary>
        /// The underlying Btc-e proxy
        /// </summary>
        private BtceApi _proxy;
        #endregion

        /// <summary>
        /// Initialises a new instance of the wrapper
        /// </summary>
        public BtceWrapper()
        {
            var p = SecureStorage.GetEncryptedData<BtceAPIParams>(SecuredDataKeys.BtceAPI);
            _proxy = new BtceApi(p.APIKey, string.IsNullOrEmpty(p.APISecret) ? string.Empty : p.APISecret);
        }

        /// <summary>
        /// Gets the underlying proxy
        /// </summary>
        public BtceApi BtceProxy
        {
            get { return _proxy; }
        }


        /// <summary>
        /// Gets the ticker for a given currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>The ticker</returns>
        public CallResult<DE.Ticker> GetTicker(CurrencyPair pair)
        {
            return CallProxy(() => BtceApi.GetTicker(pair.ToBtcePair()),
                t => new DE.Ticker
            {
                Ask = t.Buy,//Buy/Sell are opposite in btc-e
                Bid = t.Sell,
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
            return CallProxy(() => _proxy.GetInfo(),

            userInfo =>
            {
                var bal = new Balance();
                foreach (var kvp in userInfo.Funds.Balances)
                {
                    bal.Balances[kvp.Key] = kvp.Value;
                }
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
            return CallProxy(() => _proxy.Trade(pair.ToBtcePair(), TradeType.Sell, price, amount),
                o => new OrderDetails
            {
                Amount = amount,
                Price = price,
                Id = o.OrderId,
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
            return CallProxy(() => _proxy.Trade(pair.ToBtcePair(), TradeType.Buy, price, amount),
                o => new OrderDetails
                {
                    Amount = amount,
                    Price = price,
                    Id = o.OrderId,
                    Type = OrderType.Buy
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
            return CallProxy(
                () => BtceApi.GetTrades(pair.ToBtcePair()),
                trades =>
                {
                    var ret = trades.Select
                       (t => new DE.Transaction
                       {
                           Amount = t.Amount,
                           Price = t.Price,
                           ID = t.Tid,
                           Timestamp = BtcE.Utils.UnixTime.GetFromDateTime(t.Date),
                           Date = t.Date
                       });
                    var tlist = new TransactionList
                    {
                        Transactions = ret.ToList()
                    };

                    return tlist;
                }
                );
        }

        /// <summary>
        /// Gets the order book for a currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>The order book (bids/asks)</returns>
        public CallResult<OrderBook> GetOrderBook(CurrencyPair pair)
        {
            return CallProxy(() => BtceApi.GetDepth(pair.ToBtcePair()),
               orderBook => new OrderBook
            {
                Asks = orderBook.Asks.Select(a => new DE.SimpleOrderInfo
                {
                    Amount = a.Amount,
                    Price = a.Price
                }).ToList(),
                Bids = orderBook.Bids.Select(b => new DE.SimpleOrderInfo
                {
                    Amount = b.Amount,
                    Price = b.Price
                }).ToList()
            }); ;
        }

        /// <summary>
        /// Gets all open orders
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>A list of open orders</returns>
        public CallResult<OpenOrders> GetOpenOrders(CurrencyPair pair)
        {
            return CallProxy(() => _proxy.GetOpenOrders(pair.ToBtcePair()),
                orders =>

            new OpenOrders
            {
                List = orders.List.Select(o => new OrderDetails
                {
                    Amount = o.Value.Amount,
                    Id = o.Key,
                    Price = o.Value.Rate,
                    DateTime = BtcE.Utils.UnixTime.ConvertToDateTime(o.Value.TimestampCreated),
                    Type = o.Value.Type == TradeType.Sell ? OrderType.Sell : OrderType.Buy
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
            return CallProxy(() => _proxy.CancelOrder((int)orderId),
                o => true);
        }

        /// <summary>
        /// Gets the fee associated to a currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>The fee</returns>
        public CallResult<Fee> GetFee(CurrencyPair pair)
        {
            return CallProxy(() => BtceApi.GetFee(pair.ToBtcePair()), f =>
                new Fee
            {
                BuyFee = f,
                SellFee = f
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
            return type == OrderType.Buy ? pair.Item1 : pair.Item2;
        }

        /// <summary>
        /// Returns the list of currency pairs available on the exchange
        /// </summary>
        /// <returns>A list of currency pairs</returns>
        public CallResult<CurrencyPair[]> GetCurrencyPairs()
        {
            var pairs = new[]{
                            new CurrencyPair ("BTC", "USD" ),
                            new CurrencyPair ("BTC", "EUR" ),
                            new CurrencyPair ("LTC", "USD" ),
                            new CurrencyPair ("LTC", "BTC" ),
                            new CurrencyPair ("NMC", "USD" ),
                            new CurrencyPair ("NMC", "BTC" ),
                            new CurrencyPair ("PPC", "USD" ),
                            new CurrencyPair ("PPC", "BTC" )
             };
            return new CallResult<CurrencyPair[]>(pairs);
        }

        /// <summary>
        /// Makes a proxy call and converts the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="dataRetrievalFunc"></param>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        private CallResult<T> CallProxy<T, T2>(Func<T2> dataRetrievalFunc, Func<T2, T> conversionFunc)
        {
            try
            {
                var ret = dataRetrievalFunc();
                if (ret != null)
                {
                    return new CallResult<T>(conversionFunc(ret));
                }
                return new CallResult<T>("Unknown error");
            }
            catch (Exception e)
            {
                return new CallResult<T>(e.Message);
            }
        }

    }

    /// <summary>
    /// Extensions for CurrencyPair
    /// </summary>
    static class CurrencyPairExtensions
    {
        /// <summary>
        /// Converts a CurrenyPair instance to a BtcePair
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public static BtcePair ToBtcePair(this CurrencyPair pair)
        {
            return BtcePairHelper.FromString(string.Format("{0}_{1}", pair.Item1, pair.Item2));
        }

        /// <summary>
        /// Converts a CurrenyPair instance to a BtcePair
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public static CryptsyAPI.CurrencyPair ToCryptsyPair(this CurrencyPair pair)
        {
            return new CryptsyAPI.CurrencyPair
            {
                ID = pair.ID,
                Item1 = pair.Item1,
                Item2 = pair.Item2
            };

        }
    }
}
