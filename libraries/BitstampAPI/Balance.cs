using Newtonsoft.Json.Linq;

namespace BitstampAPI
{
    /// <summary>
    /// Represents the user's balance
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// The amount of available USD for orders
        /// </summary>
        public decimal AvailableUSD { get; private set; }
        /// <summary>
        /// The amount of available BTC for orders
        /// </summary>
        public decimal AvailableBTC { get; private set; }
        /// <summary>
        /// The total amount of USD
        /// </summary>
        public decimal BalanceUSD { get; private set; }
        /// <summary>
        /// The total amount of BTC
        /// </summary>
        public decimal BalanceBTC { get; private set; }
        /// <summary>
        /// Amount of USD reserved in open orders
        /// </summary>
        public decimal ReservedUSD { get; private set; }
        /// <summary>
        /// Amount of BTC reserved in open orders
        /// </summary>
        public decimal ReservedBTC { get; private set; }
        /// <summary>
        /// The fee
        /// </summary>
        public decimal Fee { get; private set; }

        public static Balance CreateFromJObject(JObject o)
        {
            if (o == null)
            {
                return null;
            }

            var r = new Balance()
            {
                BalanceUSD = o.Value<decimal>("usd_balance"),
                BalanceBTC = o.Value<decimal>("btc_balance"),
                AvailableUSD = o.Value<decimal>("usd_available"),
                AvailableBTC = o.Value<decimal>("btc_available"),
                ReservedUSD = o.Value<decimal>("usd_reserved"),
                ReservedBTC = o.Value<decimal>("btc_reserved"),
                Fee = o.Value<decimal>("fee")
            };

            return r;
        }

    }
}
