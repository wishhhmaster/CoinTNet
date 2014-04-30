using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CryptsyAPI
{

    /// <summary>
    /// Represents a trade
    /// </summary>
    public class Trade
    {
        /// <summary>
        /// Gets price
        /// </summary>
        public decimal Price { get; private set; }
        /// <summary>
        /// Gets the amount
        /// </summary>
        public decimal Amount { get; private set; }
        /// <summary>
        /// Gets the total price of the trade
        /// </summary>
        public decimal Total { get; private set; }
        /// <summary>
        /// Gets the ID
        /// </summary>
        public long ID { get; private set; }
        /// <summary>
        /// Gets the date and time of the transaction
        /// </summary>
        public DateTime Date { get; private set; }
        /// <summary>
        /// Gets the type of order which initiated the trade
        /// </summary>
        public string InitiatingOrderType { get; private set; }

        public static Trade CreateFromJObject(JObject obj, int hoursDiff)
        {
            if (obj == null)
            {
                return null;
            }

            var t = new Trade()
            {
                ID = obj.Value<long>("tradeid"),
                Price = obj.Value<decimal>("tradeprice"),
                Amount = obj.Value<decimal>("quantity"),
                Total = obj.Value<decimal>("total"),
                Date = DateTime.SpecifyKind(DateTime.Parse(obj.Value<string>("datetime")).AddHours(hoursDiff), DateTimeKind.Utc),
                InitiatingOrderType = obj.Value<string>("initiate_ordertype"),
            };

            return t;
        }
    }


}
