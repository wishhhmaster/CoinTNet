using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTNet.Common.Constants
{
    /// <summary>
    /// General constants about the application
    /// </summary>
    class ApplicationConstants
    {
        /// <summary>
        /// The application's name
        /// </summary>
        public const string ApplicationName = "CoinT.Net";
        /// <summary>
        /// The application's current version
        /// </summary>
        public const string CurrentVersion = "0.0.0.5";
    }

    /// <summary>
    /// Keys for the encrypted data
    /// </summary>
    class SecuredDataKeys
    {
        /// <summary>
        /// Key for Bitstamp's API keys
        /// </summary>
        public const string BitstampAPI = "bitstampAPI";
        /// <summary>
        /// Key for Btc-e's API keys
        /// </summary>
        public const string BtceAPI = "btceAPI";
        /// <summary>
        /// Key for Twitter's API keys
        /// </summary>
        public const string TwitterAPI = "twitterAPI";
        /// <summary>
        /// Key for Cryptsy's API keys
        /// </summary>
        public const string CryptsyAPI = "cryptsyAPI";
    }
}
