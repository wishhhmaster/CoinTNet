using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinCharts.Net.Http {
    internal class BitcoinChartsDelegatingHandler : DelegatingHandler {
        public BitcoinChartsDelegatingHandler() {
            InnerHandler = new HttpClientHandler {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None,
            };
        }
    }
}