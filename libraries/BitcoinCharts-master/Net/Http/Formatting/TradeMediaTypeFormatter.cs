using BitcoinCharts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BitcoinCharts.Net.Http.Formatting
{
    internal class TradeMediaTypeFormatter : MediaTypeFormatter
    {
        private readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private string _symbol;
        public TradeMediaTypeFormatter(string symbol)
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            _symbol = symbol;
        }

        public override bool CanReadType(Type type)
        {
            return (type == typeof(Trade)) ? true : typeof(IEnumerable<Trade>).IsAssignableFrom(type);
        }
        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger logger)
        {
            var trades = new Trades();
            var symbol = _symbol;

            using (var reader = new StreamReader(stream))
            {
                var line = reader.ReadLine();
                var seperator = ",".ToCharArray();

                while (null != (line = reader.ReadLine()))
                {
                    if (!line.Contains("502 Bad Gateway"))
                    {
                        var parts = line.Split(seperator);

                        var unixtime = parts[0].Parse();
                        var price = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
                        var quantity = decimal.Parse(parts[2], CultureInfo.InvariantCulture);

                        var datetime = Epoch.AddSeconds(unixtime);

                        trades.Add(symbol, datetime, price, quantity);
                    }
                    else throw new Exception("Bad Gateway");
                }
            }

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(trades);
            return tcs.Task;
        }


        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
        {
            return base.GetPerRequestFormatterInstance(type, request, mediaType);
        }
    }

    public static partial class Extensions
    {
        internal static Trades Add(this Trades source, string symbol, DateTimeOffset datetime, decimal price, decimal quantity)
        {
            source.Add(new Trade
            {
                Symbol = symbol,
                Datetime = datetime,
                Price = price,
                Quantity = quantity
            });
            return source;
        }

        internal static double Parse(this string source)
        {
            return double.Parse(source);
        }
    }
}