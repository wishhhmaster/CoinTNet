using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinCharts {
    public partial class BitcoinChartsClient {
        public interface IGetTradesConfigurator {
            IGetTradesConfigurator Market(string value);
            IGetTradesConfigurator Currency(string value);

            IGetTradesConfigurator Start(DateTimeOffset value);
        }

        private class GetTradesSettings {
            public string Market { get; set; }
            public string Currency { get; set; }

            public DateTimeOffset Start { get; set; }
        }

        private class GetTradesConfigurator : IGetTradesConfigurator {
            private GetTradesSettings _settings = new GetTradesSettings();

            public IGetTradesConfigurator Market(string value) {
                _settings.Market = value;
                return this;
            }

            public IGetTradesConfigurator Currency(string value) {
                _settings.Currency = value;
                return this;
            }

            public IGetTradesConfigurator Start(DateTimeOffset value) {
                _settings.Start = value;
                return this;
            }

            //http://api.bitcoincharts.com/v1/trades.csv?symbol=mtgoxUSD
            public HttpRequestMessage Build() {
                var parameters = new Parameters();
                parameters.Add("symbol", "{0}{1}".FormatWith(_settings.Market, _settings.Currency));

                if(_settings.Start != default(DateTimeOffset)) {
                    parameters.Add("start", _settings.Start.ToUnixTime());
                }

                var query = parameters.ToQueryString();

                var uri = "v1/trades.csv?{0}".FormatWith(query);

                var request = new HttpRequestMessage {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(uri, UriKind.Relative)
                };

                return request;
            }
        }
    }

    public static partial class Extensions {
        private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        public static long ToUnixTime(this DateTimeOffset value) {
            return (long)(value - Epoch).TotalSeconds;
        }       
    }
}