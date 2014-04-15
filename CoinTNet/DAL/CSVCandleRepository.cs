using CoinTNet.DO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CoinTNet.DAL
{
    class CSVCandleRepository
    {

        /// <summary>
        /// Gets the path of the file where transactions for a given currency pair are stored
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        private string GetTransactionsStoreFileName(CurrencyPair pair)
        {
            return Path.Combine(Environment.CurrentDirectory, string.Format("{0}{1}{2}.csv", pair.Exchange.BitcoinChartsCode, pair.Item1, pair.Item2));
        }

        /// <summary>
        /// Saves candles to a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="candles"></param>
        public void SaveCandles(string fileName, IList<OHLC> candles)
        {
            using (var writer = new StreamWriter(File.OpenWrite(fileName)))
            {
                foreach (var c in candles)
                {
                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:0.000000},{2:0.000000},{3:0.000000},{4:0.000000}", BitstampAPI.UnixTimeHelper.GetFromDateTime(c.Date), c.Open, c.High, c.Low, c.Close));
                }
            }
        }

        /// <summary>
        /// Retrieve candles from a file containing transactions
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="candleDurationInMinutes"></param>
        /// <param name="pair"></param>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        public List<OHLC> GetCandlesFromCSVTransactions(DateTime from, DateTime? to, int candleDurationInMinutes, CurrencyPair pair, string inputFile = null)
        {
            if (string.IsNullOrEmpty(inputFile))
            {
                inputFile = GetTransactionsStoreFileName(pair);
            }
            if (!File.Exists(inputFile))
            {
                return new List<OHLC>();
            }
            var fromTimeStamp = BitstampAPI.UnixTimeHelper.GetFromDateTime(from);
            var toTimeStamp = to != null ? BitstampAPI.UnixTimeHelper.GetFromDateTime(to.Value) : (uint?)null;

            long? nextStamp = null;
            int candleDurationInSeconds = candleDurationInMinutes * 60;
            List<decimal> pricesForCurrentCandle = new List<decimal>();
            List<OHLC> candles = new List<OHLC>();
            using (var sr = new StreamReader(inputFile))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var buf = line.Split(new[] { ',' });
                    long transTimeStamp = Convert.ToInt64(buf[0]);
                    if (transTimeStamp < fromTimeStamp)
                    {
                        continue;
                    }
                    decimal price = Convert.ToDecimal(buf[1], CultureInfo.InvariantCulture);
                    if (nextStamp == null)
                    {
                        nextStamp = transTimeStamp + candleDurationInSeconds;
                    }
                    if (transTimeStamp < nextStamp)
                    {
                        pricesForCurrentCandle.Add(price);
                    }
                    else//New candle
                    {
                        OHLC ohlc = new OHLC();
                        if (pricesForCurrentCandle.Count > 0)
                        {
                            ohlc = new OHLC
                            {
                                Open = pricesForCurrentCandle.First(),
                                High = pricesForCurrentCandle.Max(),
                                Low = pricesForCurrentCandle.Min(),
                                Close = pricesForCurrentCandle.Last(),
                                Date = BitstampAPI.UnixTimeHelper.ConvertToDateTime((uint)nextStamp)
                            };

                        }
                        else//No prices for this candle... use previous candle's close value
                        {
                            var prev = candles.Last();
                            ohlc = new OHLC
                            {
                                Open = prev.Close,
                                High = prev.Close,
                                Low = prev.Close,
                                Close = prev.Close,
                                Date = BitstampAPI.UnixTimeHelper.ConvertToDateTime((uint)nextStamp)
                            };
                        }
                        candles.Add(ohlc);

                        nextStamp = nextStamp + candleDurationInSeconds;
                        pricesForCurrentCandle.Clear();
                        if (toTimeStamp != null && nextStamp > toTimeStamp.Value)
                        {
                            break;
                        }
                    }

                }
            }//using

            return candles;


        }

        /// <summary>
        /// Gets a list of candles from a file containing CSV Candles
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fromDateTime"></param>
        /// <param name="toDateTime"></param>
        /// <returns></returns>
        public IList<OHLC> GetCandlesFromCSVCandles(string fileName, DateTime? fromDateTime = null, DateTime? toDateTime = null)
        {

            if (!File.Exists(fileName))
            {
                return new List<OHLC>();
            }


            using (var reader = new StreamReader(File.OpenRead(fileName)))
            {
                List<OHLC> k = new List<OHLC>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine(); //date,open,high,low,close
                    var values = line.Split(',');
                    k.Add(new OHLC()
                    {
                        Date = BitstampAPI.UnixTimeHelper.ConvertToDateTime(Convert.ToUInt32(values[0])),
                        Open = decimal.Parse(values[1], CultureInfo.InvariantCulture),
                        High = decimal.Parse(values[2], CultureInfo.InvariantCulture),
                        Low = decimal.Parse(values[3], CultureInfo.InvariantCulture),
                        Close = decimal.Parse(values[4], CultureInfo.InvariantCulture),
                        TimeStamp = Convert.ToInt64(values[0]),
                        TradeSource = TradeSource.CSV
                    });
                }

                if (fromDateTime.HasValue)
                {
                    k = k.Where(x => x.Date > fromDateTime).ToList();
                }

                return k;
            }
        }
    }
}
