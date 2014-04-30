
namespace CryptsyAPI
{
    /// <summary>
    /// Represents a currency pair
    /// </summary>
    public class CurrencyPair
    {
        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public CurrencyPair()
        {

        }

        /// <summary>
        /// Initialises a new instance of the class with specified parameters
        /// </summary>
        public CurrencyPair(string item1, string item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
        /// <summary>
        /// Gets or sets the pair's first item
        /// </summary>
        public string Item1 { get; set; }
        /// <summary>
        /// Gets or sets the pair's second item
        /// </summary>
        public string Item2 { get; set; }
        /// <summary>
        /// Gets or sets the pair's ID
        /// </summary>
        public string ID { get; set; }
    }
}
