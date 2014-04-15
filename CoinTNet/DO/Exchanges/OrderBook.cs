using System.Collections.Generic;

namespace CoinTNet.DO.Exchanges
{
    /// <summary>
    /// Represents an exchange's order book
    /// </summary>
    public class OrderBook
    {
        /// <summary>
        /// The list of sell orders
        /// </summary>
        public List<SimpleOrderInfo> Asks { get; set; }
        /// <summary>
        /// The list of buy orders
        /// </summary>
        public List<SimpleOrderInfo> Bids { get; set; }
    }
}
