using System.Collections.Generic;

namespace CoinTNet.DO.Exchanges
{
    /// <summary>
    /// Contains a list of the user's open orders
    /// </summary>
    public class OpenOrders
    {
        /// <summary>
        /// Gets the list of open orders
        /// </summary>
        public List<OrderDetails> List { get; set; }

        public OpenOrders()
        {
            List = new List<OrderDetails>();
        }
    }
}
