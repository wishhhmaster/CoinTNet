using Newtonsoft.Json.Linq;
using System.Linq;

namespace CryptsyAPI
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

            decimal highestBid = 0m, lowestAsk = 0m;

            var sellOrders = o["sellorders"] as JArray;
            if (sellOrders != null)
            {
                lowestAsk = sellOrders.Min(s => s.Value<decimal>("price"));
            }
            var buyOrders = o["buyorders"] as JArray;
            if (buyOrders != null)
            {
                highestBid = buyOrders.Max(s => s.Value<decimal>("price"));
            }


            var tick = new Ticker()
            {
                Bid = highestBid,
                Ask = lowestAsk,
                Volume = o.Value<decimal>("volume"),
                High = o.Value<decimal>("24hhigh"),
                Low = o.Value<decimal>("24hlow"),
                Last = o.Value<decimal>("lasttradeprice")
            };

            return tick;
        }


    }
}
