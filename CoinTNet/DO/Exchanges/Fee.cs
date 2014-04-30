
namespace CoinTNet.DO.Exchanges
{
    /// <summary>
    /// Represents a fee
    /// </summary>
    class Fee
    {
        /// <summary>
        /// Gets or sets the fee for a trade when buying
        /// </summary>
        public decimal BuyFee { get; set; }
        /// <summary>
        /// Gets or sets the fee for a trade when selling
        /// </summary>
        public decimal SellFee { get; set; }
    }
}
