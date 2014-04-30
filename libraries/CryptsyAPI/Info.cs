using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace CryptsyAPI
{
    /// <summary>
    /// Represents the info retrieves from the getinfo method
    /// </summary>
    public class Info
    {
        /// <summary>
        /// Gets available balances for all currencies
        /// </summary>
        public Dictionary<string, decimal> BalancesAvailable { get; private set; }
        /// <summary>
        /// Gets balances on hold (reserved in orders) for all currencies
        /// </summary>
        public Dictionary<string, decimal> BalancesOnHold { get; private set; }
        /// <summary>
        /// Gets the server's unix timestamp
        /// </summary>
        public long ServerTimeStamp { get; private set; }
        /// <summary>
        /// Gets the server's timezone
        /// </summary>
        public string ServerTimeZone { get; private set; }
        /// <summary>
        /// Get's the server's datetime (local timezone)
        /// </summary>
        public string ServerdDateTime { get; private set; }
        /// <summary>
        /// Gets the number of open orders for the user
        /// </summary>
        public int OpenOrdersCount { get; private set; }

        /// <summary>
        /// Returns a new instance of the class based on a json object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Info CreateFromJObject(JObject obj)
        {
            var info = new Info
                {
                    ServerTimeStamp = obj.Value<long>("servertimestamp"),
                    OpenOrdersCount = obj.Value<int>("openordercount"),
                    BalancesAvailable = new Dictionary<string, decimal>(),
                    BalancesOnHold = new Dictionary<string, decimal>(),
                    ServerTimeZone = obj.Value<string>("servertimezone"),
                    ServerdDateTime = obj.Value<string>("serverdatetime"),
                };

            foreach (var val in (obj["balances_available"] as JObject))
            {
                info.BalancesAvailable[val.Key.ToUpper()] = decimal.Parse(val.Value.Value<string>(), NumberStyles.AllowExponent | NumberStyles.Float, CultureInfo.InvariantCulture);
            }

            if (obj["balances_hold"] != null)//will be null if there is no open order
            {
                foreach (var val in (obj["balances_hold"] as JObject))
                {
                    info.BalancesAvailable[val.Key.ToUpper()] = decimal.Parse(val.Value.Value<string>(), NumberStyles.AllowExponent | NumberStyles.Float, CultureInfo.InvariantCulture);
                }
            }
            return info;
        }
    }


}
