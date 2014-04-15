
namespace CoinTNet.DO.Exchanges
{
    /// <summary>
    /// Ticker for a currency pair
    /// </summary>
    public class Ticker
    {
        /// <summary>
        /// The highest buy order
        /// </summary>
        public decimal Bid { get; set; }
        /// <summary>
        /// The lowest sell order
        /// </summary>
        public decimal Ask { get; set; }
        /// <summary>
        /// The 24 Hour volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// The 24 h high
        /// </summary>
        public decimal High { get; set; }
        /// <summary>
        /// The 24 H low
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// The price for the last order
        /// </summary>
        public decimal Last { get; set; }
    }
}
