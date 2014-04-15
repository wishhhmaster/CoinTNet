using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BitstampAPI
{
    /// <summary>
    /// Represents the order book
    /// </summary>
    public class OrderBook
    {
        /// <summary>
        /// The list of sell orders
        /// </summary>
        public List<SimpleOrderInfo> Asks { get; private set; }
        /// <summary>
        /// The list of buy orders
        /// </summary>
        public List<SimpleOrderInfo> Bids { get; private set; }

        public static OrderBook CreateFromJObject(JObject o)
        {
            var r = new OrderBook();
            r.Asks = new List<SimpleOrderInfo>();
            r.Bids = new List<SimpleOrderInfo>();

            foreach (var item in o["asks"] as JArray)
            {
                var order = SimpleOrderInfo.CreateFromJObject(item as JArray);
                r.Asks.Add(order);
            }
            foreach (var item in o["bids"] as JArray)
            {
                var order = SimpleOrderInfo.CreateFromJObject(item as JArray);
                r.Bids.Add(order);
            }

            return r;
        }

    }

    /// <summary>
    /// Represents an order from the order book
    /// </summary>
    public class SimpleOrderInfo
    {
        /// <summary>
        /// The price
        /// </summary>
        public decimal Price { get; private set; }
        /// <summary>
        /// The amount
        /// </summary>
        public decimal Amount { get; private set; }

        public static SimpleOrderInfo CreateFromJObject(JArray obj)
        {
            if (obj == null)
            {
                return null;
            }

            var r = new SimpleOrderInfo()
            {
                Price = obj.Value<decimal>(0),
                Amount = obj.Value<decimal>(1),
            };

            return r;
        }

    }
}
