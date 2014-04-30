
namespace CoinTNet.DO.Security
{
    /// <summary>
    /// Holds the parameters for connecting to Cryptsy' private API
    /// </summary>
    public class CryptsyAPIParams
    {
        /// <summary>
        /// Gets or sets the public key
        /// </summary>
        public string PublicKey { get; set; }
        /// <summary>
        /// Gets or sets the secret secret
        /// </summary>
        public string SecretKey { get; set; }
    }
}
