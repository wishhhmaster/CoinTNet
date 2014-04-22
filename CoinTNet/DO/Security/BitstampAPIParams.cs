
namespace CoinTNet.DO.Security
{
    /// <summary>
    /// Holds the parameters for connecting to Bitstamp's private API
    /// </summary>
    public class BitstampAPIParams
    {
        /// <summary>
        /// Gets or sets the API key
        /// </summary>
        public string APIKey { get; set; }
        /// <summary>
        /// Gets or sets the secret
        /// </summary>
        public string APISecret { get; set; }
        /// <summary>
        /// Gets or sets the client ID
        /// </summary>
        public string ClientID { get; set; }
    }
}
