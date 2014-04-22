using CoinTNet.Common.Constants;
using CoinTNet.DAL.Exchanges;
using CoinTNet.DAL.Interfaces;
using CoinTNet.DO.Security;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace CoinTNet.DAL
{
    /// <summary>
    /// Factory for twitter proxy
    /// </summary>
    class TwitterProxyFactory
    {
        /// <summary>
        /// The proxy singleton
        /// </summary>
        private static TwitterProxy _proxy;

        /// <summary>
        /// Gets the proxy
        /// </summary>
        /// <param name="exchangeCode"></param>
        /// <returns></returns>
        public static TwitterProxy GetProxy()
        {
            if (_proxy == null)
            {
                var p = SecureStorage.GetEncryptedData<TwitterAPIParams>(SecuredDataKeys.TwitterAPI);
                NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("CoinTNet");
                string baseUrl = string.Empty;
                if (section != null && section.Count > 0)
                {
                    baseUrl = section["twitter.APIBaseUrl"];
                }

                _proxy = new TwitterProxy(p.ConsumerKey, p.SecretKey, baseUrl);

            }
            return _proxy;
        }

        /// <summary>
        /// Notifies the factory that api key/secret have changed so that the proxy instance is reset
        /// </summary>
        public static void NotifySettingsChanged()
        {
            _proxy = null;
        }

    }
}
