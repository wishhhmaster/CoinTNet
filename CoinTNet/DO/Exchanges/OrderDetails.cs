using System;

namespace CoinTNet.DO.Exchanges
{
    public enum OrderType
    {
        Buy,
        Sell
    }
    /// <summary>
    /// Represents an order
    /// </summary>
    public class OrderDetails
    {
        /// <summary>
        /// Gets or sets the order ID
        /// </summary>
        public long Id { get;  set; }
        /// <summary>
        /// Gets or sets the date the order was created
        /// </summary>
        public DateTime DateTime { get;  set; }
        /// <summary>
        /// Gets or sets the type of order
        /// </summary>
        public OrderType Type { get;  set; }
        /// <summary>
        /// Gets or sets the order price
        /// </summary>
        public decimal Price { get;  set; }
        /// <summary>
        /// Gets or sets the order's amount
        /// </summary>
        public decimal Amount { get;  set; }

        public decimal LimitPrice { get; set; }

    }
}
