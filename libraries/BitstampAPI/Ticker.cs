using Newtonsoft.Json.Linq;

namespace BitstampAPI
{
    public class Ticker
    {
        /// <summary>
        /// The highest buy order
        /// </summary>
        public decimal Bid { get; private set; }
        /// <summary>
        /// The lowest sell order
        /// </summary>
        public decimal Ask { get; private set; }
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


        public static Ticker CreateFromJObject(JObject o)
        {
            if (o == null)
            {
                return null;
            }

            var tick = new Ticker()
            {
                Bid = o.Value<decimal>("bid"),
                Ask = o.Value<decimal>("ask"),
                Volume = o.Value<decimal>("volume"),
                High = o.Value<decimal>("high"),
                Low = o.Value<decimal>("low"),
                Last = o.Value<decimal>("last")
            };

            return tick;
        }
    }
}
