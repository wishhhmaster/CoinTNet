using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinCharts {
    public static partial class Extensions {
        internal static string FormatWith(this string input, params object[] formatting) {
            return string.Format(input, formatting);
        }

        internal static void Foreach<T>(this IEnumerable<T> self, Action<T> action) {
            var items = self.ToArray();
            for(int i = 0; i < items.Length; i++) {
                var item = items[i];
                action(item);
            }
        }

        internal static string ToQueryString(this Parameters self) {
            if(self == null) {
                return string.Empty;
            }

            return self.Select(x => "{0}={1}".FormatWith(x.Key.UrlEncode(), x.Value.UrlEncode())).Join("&");
        }

        /// <summary>
        /// Uses Uri.EscapeDataString() based on recommendations on MSDN
        /// http://blogs.msdn.com/b/yangxind/archive/2006/11/09/don-t-use-net-system-uri-unescapedatastring-in-url-decoding.aspx
        /// </summary>
        internal static string UrlEncode(this string self) {
            return Uri.EscapeDataString(self);
        }

        internal static string UrlEncode(this object self) {
            return UrlEncode(self.ToString());
        }

        internal static string Join(this IEnumerable<object> source, string seperator) {
            return string.Join(seperator, source);
        }

        internal static Uri Append(this Uri self, params object[] segments) {
            return new Uri(segments.Aggregate(self.AbsoluteUri, (current, segment) => string.Format("{0}/{1}", current.TrimEnd('/'), segment)));
        }

        internal static Task<T> ReadAsAsync<T>(this HttpContent content, MediaTypeFormatter formatter) {
            return content.ReadAsAsync<T>(new[] { formatter });
        }
    }
}
