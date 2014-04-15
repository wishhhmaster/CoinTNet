using BitcoinCharts.Models;
using BitcoinCharts.Net.Http;
using BitcoinCharts.Net.Http.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BitcoinCharts {
    public partial class BitcoinChartsClient : IDisposable {
        private HttpMessageHandler _handler = new BitcoinChartsDelegatingHandler();
        private HttpClient _client;
        private volatile bool _disposed;

        public BitcoinChartsClient()
            : this(x => { }) {
        }

        public BitcoinChartsClient(Action<IBitcoinChartsClientConfigurator> configure){            
            var c = new BitcoinChartsClientConfigurator();        
            configure(c);

            _client = c.Build();
        }

        ~BitcoinChartsClient() {
            Dispose(false);
        }

        public Task<Trades> GetTradesAsync(Action<IGetTradesConfigurator> configure) {
            var c = new GetTradesConfigurator();
            configure(c);

            var request = c.Build();            
            return _client.SendAsync(request)
                .GetAwaiter()
                .GetResult()
                .GetTradesAsync();
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(disposing) {
            }
            _disposed = true;
        }
    }

    public static partial class Extensions {
        public static Task<Trades> GetTradesAsync(this BitcoinChartsClient client, string market, string currency) {
            return client.GetTradesAsync(x => x
                .Market(market)
                .Currency(currency)
            );
        }

        public static Task<Trades> GetTradesAsync(this BitcoinChartsClient client, DateTimeOffset start, string market, string currency) {
            return client.GetTradesAsync(x => x
                .Market(market)
                .Currency(currency)
                .Start(start)
            );
        }

        internal static Task<Trades> GetTradesAsync(this HttpResponseMessage source) {
            var query = HttpUtility.ParseQueryString(source.RequestMessage.RequestUri.Query);
            var symbol = query["symbol"];
            return source.Content.ReadAsAsync<Trades>(new[] { new TradeMediaTypeFormatter(symbol) });
        }
    }
}