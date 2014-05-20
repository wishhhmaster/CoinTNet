using Newtonsoft.Json.Linq;
using System.Linq;

namespace CryptsyAPI
{
    /// <summary>
    /// Represents an active market (private method getmarkets from the api)
    /// </summary>
    public class ActiveMarket
    {
        /// <summary>
        /// Gets the market ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Gets the market's label
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// The 24 Hour volume
        /// </summary>
        public decimal Volume { get; private set; }
        /// <summary>
        /// The 24 h high
        /// </summary>
        public decimal High { get; private set; }
        /// <summary>
        /// The 24 H low
        /// </summary>
        public decimal Low { get; private set; }
        /// <summary>
        /// The price for the last order
        /// </summary>
        public decimal Last { get; private set; }


        public static ActiveMarket CreateFromJObject(JObject o)
        {
            if (o == null)
            {
                return null;
            }



            var m = new ActiveMarket()
            {
                ID = o.Value<string>("marketid"),
                Label = o.Value<string>("label"),
                Volume = o.Value<decimal>("current_volume"),
                High = o.Value<decimal>("high_trade"),
                Low = o.Value<decimal>("low_trade"),
                Last = o.Value<decimal>("last_trade")
            };

            return m;
        }


    }
}
