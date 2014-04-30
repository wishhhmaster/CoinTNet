using Newtonsoft.Json.Linq;
using System;

namespace CryptsyAPI
{
    public class OrderDetails
    {
        /// <summary>
        /// Gets the order ID
        /// </summary>
        public long Id { get; private set; }
        /// <summary>
        /// Gets the date the order was created
        /// </summary>
        public DateTime DateTime { get; private set; }
        /// <summary>
        /// Gets the type of order
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Gets the order price
        /// </summary>
        public decimal Price { get; private set; }
        /// <summary>
        /// Gets the order's current amount
        /// </summary>
        public decimal Amount { get; private set; }
        /// <summary>
        /// Gets the order's original amount
        /// </summary>
        public decimal OriginalAmount { get; private set; }
        /// <summary>
        /// Gets the order's total price
        /// </summary>
        public decimal Total { get; private set; }

        public static OrderDetails CreateFromJObject(JObject o, int hoursDiffToUtc)
        {
            if (o == null)
            {
                return null;
            }

            var od = new OrderDetails()
            {
                Id = o.Value<long>("orderid"),
                Type = o.Value<string>("ordertype"),
                Price = o.Value<decimal>("price"),
                Amount = o.Value<decimal>("quantity"),
                OriginalAmount = o.Value<decimal>("orig_quantity"),
                DateTime = DateTime.SpecifyKind(DateTime.Parse(o.Value<string>("created")).AddHours(hoursDiffToUtc), DateTimeKind.Utc),
                Total = o.Value<decimal>("total"),
            };

            return od;
        }
    }
}
