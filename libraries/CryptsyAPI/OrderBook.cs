using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CryptsyAPI
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

        public static OrderBook CreateFromJObject(JObject o, bool isPublic)
        {
            var r = new OrderBook();
            r.Asks = new List<SimpleOrderInfo>();
            r.Bids = new List<SimpleOrderInfo>();

            var sellOrders = o["sellorders"] as JArray;
            if (sellOrders != null)
            {
                foreach (var item in sellOrders as JArray)
                {
                    var order = SimpleOrderInfo.CreateFromJObject(item as JObject, isPublic ? "price" : "sellprice");
                    r.Asks.Add(order);
                }
            }
            var buyOrders = o["buyorders"] as JArray;
            if (buyOrders != null)
            {
                foreach (var item in buyOrders)
                {
                    var order = SimpleOrderInfo.CreateFromJObject(item as JObject, isPublic ? "price" : "buyprice");
                    r.Bids.Add(order);
                }
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

        /// <summary>
        /// The total (price * amount)
        /// </summary>
        public decimal Total { get; private set; }

        public static SimpleOrderInfo CreateFromJObject(JObject obj, string priceName)
        {
            if (obj == null)
            {
                return null;
            }

            var r = new SimpleOrderInfo()
            {
                Price = obj.Value<decimal>(priceName),
                Amount = obj.Value<decimal>("quantity"),
                Total = obj.Value<decimal>("total"),
            };

            return r;
        }

    }
}
