using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BitstampAPI
{
    /// <summary>
    /// Contains a list of open orders
    /// </summary>
    public class OpenOrdersContainer
    {
        /// <summary>
        /// The list of open orders
        /// </summary>
        public List<OrderDetails> Orders { get; private set; }

        public OpenOrdersContainer()
        {
            Orders = new List<OrderDetails>();
        }

        public static OpenOrdersContainer CreateFromJObject(JArray array)
        {
            if (array == null)
            {
                return null;
            }

            OpenOrdersContainer result = new OpenOrdersContainer();

            foreach (var item in array)
            {
                result.Orders.Add(OrderDetails.CreateFromJObject(item as JObject));
            }

            return result;
        }
    }
}
