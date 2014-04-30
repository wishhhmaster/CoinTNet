using System.Linq;

namespace CoinTNet.DO
{
    /// <summary>
    /// Represents an exchange
    /// </summary>
    class Exchange
    {
        /// <summary>
        /// initialises a new instance of the class
        /// </summary>
        /// <param name="name">The name of the exchange</param>
        /// <param name="pairs">The pairs traded on the exchange</param>
        /// <param name="internalCode">The internal code representing the exchange</param>
        /// <param name="btcChartsCode">The exchange's code on Bitcoincharts</param>
        /// <param name="btcWisdomCode">The exchange's code on BitcoinWisdom</param>
        public Exchange(string name, string internalCode, string btcChartsCode = null, string btcWisdomCode = null)
        {
            Name = name;
            InternalCode = internalCode;
            BitcoinChartsCode = string.IsNullOrEmpty(btcChartsCode) ? internalCode : btcChartsCode;
            BitcoinWisdomCode = string.IsNullOrEmpty(btcWisdomCode) ? internalCode : btcWisdomCode;
        }
        /// <summary>
        /// The code representing the exchange on Bitcoincharts
        /// </summary>
        public string BitcoinChartsCode { get; set; }
        /// <summary>
        /// The code representing the exchange on BitcoinWisdom
        /// </summary>
        public string BitcoinWisdomCode { get; set; }
        /// <summary>
        /// The code representing the exchange internally
        /// </summary>
        public string InternalCode { get; set; }
        /// <summary>
        /// The name of the exchange
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the currency pairs traded on the exchange
        /// </summary>
        public CurrencyPair[] CurrencyPairs { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating if the fee is deducted from in the total
        /// </summary>
        public bool FeeDeductedFromTotal { get; set; }

        public void AssignPairs(CurrencyPair[] pairs)
        {
            CurrencyPairs = pairs;
            CurrencyPairs.ToList().ForEach(cp => cp.Exchange = this);
        }
    }

    /// <summary>
    /// Represents a currency pair
    /// </summary>
    class CurrencyPair
    {
        /// <summary>
        /// Gets or sets a reference to the exchange for this instance of the currency pair
        /// </summary>
        public Exchange Exchange { get; set; }

        /// <summary>
        /// Initialises a new instance of the currency pair
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        public CurrencyPair(string item1, string item2, string ID = "")
        {
            Item1 = item1;
            Item2 = item2;
            this.ID = ID;
        }

        /// <summary>
        /// Gets the 1st item of the pair
        /// </summary>
        public string Item1 { get; private set; }
        /// <summary>
        /// Gets the 2nd item of the pair
        /// </summary>
        public string Item2 { get; private set; }
        /// <summary>
        /// Gets or sets an identifier for the currency pair
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Gets a description of the pair: <Item1>/<Item2>
        /// </summary>
        public string Description
        {
            get { return Item1 + "/" + Item2; }
        }
    }
}
