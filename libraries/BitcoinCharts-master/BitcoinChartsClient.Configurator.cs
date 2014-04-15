using BitcoinCharts.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinCharts {
    public partial class BitcoinChartsClient {
        private class BitcoinChartsClientSettings {
            public static readonly BitcoinChartsClientSettings Default = new BitcoinChartsClientSettings {
                Uri = "http://api.bitcoincharts.com/v1"
            };

            public string Uri { get; set; }

        }
        public interface IBitcoinChartsClientConfigurator {
            IBitcoinChartsClientConfigurator Uri(string value);
        }

        private class BitcoinChartsClientConfigurator : IBitcoinChartsClientConfigurator {
            private BitcoinChartsClientSettings _settings;

            public BitcoinChartsClientConfigurator()
                : this(BitcoinChartsClientSettings.Default) {
            }

            private BitcoinChartsClientConfigurator(BitcoinChartsClientSettings defaults) {
				_settings=defaults;
			}

            public IBitcoinChartsClientConfigurator Uri(string value) {
                _settings.Uri = value;
                return this;
            }

            public HttpClient Build() {
                return new HttpClient(new BitcoinChartsDelegatingHandler()) {
                    BaseAddress = new Uri(_settings.Uri)
                };
            }
        }
    }
}