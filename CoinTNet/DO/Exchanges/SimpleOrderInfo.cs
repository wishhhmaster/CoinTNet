
namespace CoinTNet.DO.Exchanges
{
    /// <summary>
    /// Represents an order in the order book
    /// </summary>
    public class SimpleOrderInfo
    {
        /// <summary>
        /// The price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        public decimal Amount { get; set; }
    }
}
