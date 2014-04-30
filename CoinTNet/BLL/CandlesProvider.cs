using BitcoinCharts;
using CoinTNet.Common;
using CoinTNet.Common.Constants;
using CoinTNet.DAL;
using CoinTNet.DO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTNet.BLL
{
    /// <summary>
    /// Class in charge of loading candles from various sources
    /// </summary>
    class CandlesProvider
    {
        /// <summary>
        /// Used for lock
        /// </summary>
        private static object _obj = new object();

        private static CSVCandleRepository _csvRepository = new CSVCandleRepository();

        private static BitcoinWisdomAPI.BitcoinWisdomProxy _bitcoinWisdomProxy;

        /// <summary>
        /// Retrieves candles. We first look in a local transactions file, then we get the missing candles from bitcoincharts or bitcoinwisdom
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="fromDateTime"></param>
        /// <param name="candleDurationsInMinutes"></param>
        /// <param name="toDateTime"></param>
        /// <returns></returns>
        public static IList<OHLC> LoadCandles(CurrencyPair pair, DateTime fromDateTime, int candleDurationsInMinutes, DateTime? toDateTime = null)
        {
            //Try to get candles from local transactions
            var candles = _csvRepository.GetCandlesFromCSVTransactions(fromDateTime, toDateTime, candleDurationsInMinutes, pair);
            var lastCandle = candles.LastOrDefault();

            if (toDateTime == null)
            {
                toDateTime = DateTime.UtcNow;
            }

            if (lastCandle != null)
            {
                candles.Remove(lastCandle);
            }
            var newLast = candles.LastOrDefault();
            DateTime minTime = newLast != null ? newLast.Date.AddSeconds(1) : fromDateTime;
            if (minTime.AddHours(1) < toDateTime)//If older than 1 H, we need to get data from bitcoincharts/bitcoinwisdom
            {
                if (pair.Item1 == CurrencyCodes.BTC)
                {
                    var newestCandles = GetCandlesFromBitcoincharts(minTime, candleDurationsInMinutes, pair.Exchange.BitcoinChartsCode, pair.Item2);
                    candles = candles.Union(newestCandles).ToList();
                    if (candles.Count == 0)
                    {
                        newestCandles = GetCandlesFromBitcoinWisdom(pair, fromDateTime, candleDurationsInMinutes);
                        candles = candles.Union(newestCandles).ToList();
                    }
                }
                else
                {
                    var newestCandles = GetCandlesFromBitcoinWisdom(pair, fromDateTime, candleDurationsInMinutes);
                    candles = candles.Union(newestCandles).ToList();
                }
            }

            var recentCandles = GetRecentCandlesFromTransactions(fromDateTime, candleDurationsInMinutes, pair);

            foreach (OHLC newCandle in recentCandles)
            {
                var existingCandle = candles.FirstOrDefault(e => e.Date == newCandle.Date);
                if (existingCandle != null)
                {
                    existingCandle.High = newCandle.High > existingCandle.High ? newCandle.High : existingCandle.High;
                    existingCandle.Low = newCandle.Low < existingCandle.Low ? newCandle.Low : existingCandle.Low;
                    existingCandle.Close = newCandle.Close;
                }
                else
                {
                    candles.Add(newCandle);
                }
            }


            return candles;

        }

        /// <summary>
        /// Saves a list of candles to a CSV file
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="candles">The candles to save</param>
        public static void SaveCandlesToCSV(string fileName, IList<OHLC> candles)
        {
            var rep = new CSVCandleRepository();
            rep.SaveCandles(fileName, candles);
        }
        /// <summary>
        /// Calculates candles from a file containing transactions
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="candlesDurationInMin"></param>
        /// <param name="fromDateTime"></param>
        /// <param name="toDateTime"></param>
        /// <returns></returns>
        public static IList<OHLC> ReadCandlesFromCSVTransactions(CurrencyPair pair, int candlesDurationInMin, DateTime fromDateTime, DateTime? toDateTime = null)
        {
            var rep = new CSVCandleRepository();
            string market = pair != null ? pair.Exchange.BitcoinChartsCode : string.Empty;
            string currency = pair != null ? pair.Item2 : string.Empty;

            return rep.GetCandlesFromCSVTransactions(fromDateTime, toDateTime, candlesDurationInMin, pair);
        }


        /// <summary>
        /// Loads candles from a CSV file
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <param name="fromDateTime">The from date</param>
        /// <param name="toDateTime">The to date</param>
        /// <returns>A list of candles</returns>
        public static IList<OHLC> ReadCandlesFromCSV(string fileName, DateTime? fromDateTime = null, DateTime? toDateTime = null)
        {
            var rep = new CSVCandleRepository();
            return rep.GetCandlesFromCSVCandles(fileName, fromDateTime, toDateTime);
        }

        /// <summary>
        /// Updates a list of candles with the most recent data from a given exchange (Called when ticker refreshes)
        /// </summary>
        /// <param name="candlesDurationInMin">The period' duration in minutes</param>
        /// <param name="existingCandles">A list of existing candles</param>
        /// <returns>True if the update was successful</returns>
        public static bool UpdateCandlesWithLiveData(int candlesDurationInMin, IList<OHLC> existingCandles, CurrencyPair pair)
        {
            try
            {
                var proxy = ExchangeProxyFactory.GetProxy(pair.Exchange.InternalCode);

                //We first get the transactions for the last minute
                var transactionsRes = proxy.GetTransactions(true, pair);//
                if (!transactionsRes.Success)
                {
                    return false;
                }

                var transactions = transactionsRes.Result.Transactions;
                transactions.Reverse();//Make sure they are in ASC order

                // Create trade list (required to calculate OHLC)
                IList<BitcoinCharts.Models.Trade> list = (from trade in transactions
                                                          select new BitcoinCharts.Models.Trade()
                                                          {
                                                              Datetime = trade.Date,
                                                              Price = trade.Price,
                                                              Quantity = trade.Amount,
                                                              Symbol = pair.Item2
                                                          }).ToList();

                //Check if we need to create another candle
                var lastCandle = existingCandles.LastOrDefault();
                if (list.Count == 0 && lastCandle != null)
                {
                    if (lastCandle.Date.Subtract(DateTime.MinValue).TotalMinutes / candlesDurationInMin
                        != DateTime.Now.Subtract(DateTime.MinValue).TotalMinutes / candlesDurationInMin)
                    {
                        list.Add(new BitcoinCharts.Models.Trade
                                                              {
                                                                  Datetime = lastCandle.Date.AddMilliseconds(candlesDurationInMin),
                                                                  Price = lastCandle.Close,
                                                                  Quantity = 0,
                                                                  Symbol = pair.Item2
                                                              });
                    }
                }
                // Calculate trades
                IList<OHLC> recentCandles = CandlesProvider.CalculateOHLCFromTrades(list, candlesDurationInMin, TradeSource.Bitstamp);


                foreach (OHLC newCandle in recentCandles)
                {
                    var existingCandle = existingCandles.FirstOrDefault(e => e.Date == newCandle.Date);
                    if (existingCandle != null)
                    {
                        existingCandle.High = newCandle.High > existingCandle.High ? newCandle.High : existingCandle.High;
                        existingCandle.Low = newCandle.Low < existingCandle.Low ? newCandle.Low : existingCandle.Low;
                        existingCandle.Close = newCandle.Close;
                        existingCandle.TradeSource = TradeSource.Bitstamp;
                        //recentCandles = recentCandles.Skip(1).ToList();
                    }
                    else
                    {
                        existingCandles.Add(newCandle);
                    }
                }

                return recentCandles.Count() > 0;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(ex.ToString());
            }
            return false;
        }


        /// <summary>
        /// Returns candles from Bitcoincharts (for Bitcoins only)
        /// </summary>
        /// <param name="minValue">The from date</param>
        /// <param name="candlesDurationInMinutes">The candles' duration in miutes</param>
        /// <param name="market">The exchange</param>
        /// <param name="currency">The currency</param>
        /// <returns></returns>
        private static IList<OHLC> GetCandlesFromBitcoincharts(DateTime minValue, int candlesDurationInMinutes, string market, string currency)
        {
            lock (_obj)
            {
                var client = new BitcoinChartsClient();
                IList<OHLC> aggregate = new List<OHLC>();
                DateTime retrieveFrom = minValue;
                while (retrieveFrom.AddMinutes(candlesDurationInMinutes) < new DateTimeOffset(DateTime.Now))
                {
                    Task<BitcoinCharts.Models.Trades> tradeFetcher;
                    if (retrieveFrom != DateTime.MinValue)
                    {
                        tradeFetcher = client.GetTradesAsync(new DateTimeOffset(retrieveFrom), market, currency);
                    }
                    else
                    {
                        tradeFetcher = client.GetTradesAsync(market, currency);
                    }
                    if (tradeFetcher.Status == TaskStatus.Faulted)
                    {
                        break;
                    }

                    try
                    {
                        tradeFetcher.Wait();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                        MessageBox.Show(ex.ToString());
                    }

                    if (tradeFetcher.Status == TaskStatus.Faulted)
                    {
                        throw new Exception("Failed to get values from Bitcoincharts");
                    }

                    if (tradeFetcher.Result != null && tradeFetcher.Result.Count > 0)
                    {
                        IList<OHLC> candles = CandlesProvider.CalculateOHLCFromTrades(tradeFetcher.Result, candlesDurationInMinutes, TradeSource.BitcoinCharts).Where(x => x.Date >= minValue).ToList(); ;
                        if (candles.Count() == 0 || candles.Last().Date == retrieveFrom)
                        {
                            break;
                        }
                        else
                        {
                            if (aggregate.Count > 0)
                            {
                                aggregate.RemoveAt(aggregate.Count - 1);
                            }
                            aggregate = aggregate.Union(candles).ToList();
                            retrieveFrom = candles.Last().Date;
                        }
                    }
                    else
                    {
                        break;
                    }


                }//while
                return aggregate.OrderBy(i => i.Date).ToList();
            }//lock
        }


        /// <summary>
        /// Retrieves candles from BitcoinWisdom
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="from"></param>
        /// <param name="candlesDurationInMin"></param>
        /// <returns></returns>
        private static IList<OHLC> GetCandlesFromBitcoinWisdom(CurrencyPair pair, DateTime from, int candlesDurationInMin)
        {
            try
            {
                var p = GetBitcoinWisdomProxy();
                var trades = p.GetCandles(from, candlesDurationInMin, pair.Exchange.BitcoinWisdomCode, pair.Item1, pair.Item2);
                return trades.Select(t =>
                    new OHLC
                    {
                        Close = t.Close,
                        Open = t.Open,
                        High = t.High,
                        Date = t.DateTime.DateTime,
                        Low = t.Low,
                        TimeStamp = t.TimeStamp,


                    }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(ex.ToString());
            }
            return new List<OHLC>();
        }
        /// <summary>
        /// Retrieves recent transactions from the exchange to calculate recent candles
        /// </summary>
        /// <param name="candlesDurationInMin"></param>
        /// <returns></returns>
        private static IList<OHLC> GetRecentCandlesFromTransactions(DateTime from, int candlesDurationInMin, CurrencyPair pair)
        {
            try
            {
                var proxy = ExchangeProxyFactory.GetProxy(pair.Exchange.InternalCode);
                var tradesRes = proxy.GetTransactions(false, pair);
                if (!tradesRes.Success)
                {
                    return new List<OHLC>();
                }

                var trades = tradesRes.Result.Transactions;
                trades.Reverse();

                // Create trade list (required to calculate OHLC)
                IList<BitcoinCharts.Models.Trade> list = (from trade in trades
                                                          select new BitcoinCharts.Models.Trade()
                                                          {
                                                              Datetime = trade.Date,
                                                              Price = trade.Price,
                                                              Quantity = trade.Amount,
                                                              Symbol = pair.Item2
                                                          }).ToList();
                // Calculate trades
                IList<OHLC> candles = CandlesProvider.CalculateOHLCFromTrades(list, candlesDurationInMin, TradeSource.Bitstamp).Where(c => c.Date >= from).ToList();
                return candles;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(ex.ToString());
            }
            return new List<OHLC>();

        }

        /// <summary>
        /// Converts a list of Trades into candles
        /// </summary>
        /// <param name="trades"></param>
        /// <param name="candlesDurationInMin"></param>
        /// <param name="tradeSource"></param>
        /// <returns></returns>
        private static IList<OHLC> CalculateOHLCFromTrades(IList<BitcoinCharts.Models.Trade> trades, int candlesDurationInMin, TradeSource tradeSource)
        {
            var result = from trade in trades
                         group trade by (long)(trade.Datetime.DateTime.Subtract(DateTime.MinValue).TotalMinutes / candlesDurationInMin) into candleTrades
                         let key = candleTrades.Key
                         select new OHLC()
                         {
                             Date = DateTime.MinValue.AddMinutes(candleTrades.Key * candlesDurationInMin).ChangeTypeToUtc(),
                             Open = candleTrades.First().Price,
                             Close = candleTrades.Last().Price,
                             Low = candleTrades.Min(x => x.Price),
                             High = candleTrades.Max(x => x.Price),
                             TradesCount = candleTrades.Count(),
                             TradeSource = tradeSource
                         };

            return result.ToList();
        }

        /// <summary>
        /// Gets a reference to the BitcoinWisdom proxy
        /// </summary>
        /// <returns></returns>
        private static BitcoinWisdomAPI.BitcoinWisdomProxy GetBitcoinWisdomProxy()
        {
            if (_bitcoinWisdomProxy == null)
            {
                NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("CoinTNet");
                string url = string.Empty;
                if (section != null && section.Count > 0)
                {
                    url = section["bitcoinwisdom.baseUrl"];
                }
                _bitcoinWisdomProxy = new BitcoinWisdomAPI.BitcoinWisdomProxy(url);
            }
            return _bitcoinWisdomProxy;
        }
    }
}
