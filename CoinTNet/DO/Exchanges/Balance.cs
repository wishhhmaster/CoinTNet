using System.Collections.Generic;

namespace CoinTNet.DO.Exchanges
{
    /// <summary>
    /// Holds the user's balance in multiple currencies
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public Balance()
        {
            Balances = new Dictionary<string, decimal>();
        }
        /// <summary>
        /// Gets the fee
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Holds balances per currency
        /// </summary>
        public Dictionary<string, decimal> Balances { get; private set; }

        /// <summary>
        /// Gets the balance for the specified currency
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public decimal this[string currency]
        {
            get { return Balances[currency]; }
        }
    }
}
