using CoinTNet.Common.Constants;
using CoinTNet.DAL.Exchanges;
using CoinTNet.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace CoinTNet.DAL
{
    /// <summary>
    /// Factory for proxies
    /// </summary>
    class ExchangeProxyFactory
    {
        /// <summary>
        /// Dictionary containing proxy singletons
        /// </summary>
        private static Dictionary<string, IExchange> _singletons = new Dictionary<string, IExchange>();

        /// <summary>
        /// Gets the proxy
        /// </summary>
        /// <param name="exchangeCode"></param>
        /// <returns></returns>
        public static IExchange GetProxy(string exchangeCode)
        {
            IExchange ret;
            if (!_singletons.TryGetValue(exchangeCode, out ret))
            {
                if (exchangeCode == ExchangesInternalCodes.Bitstamp)
                {
                    ret = new BitstampWrapper();
                }
                else if (exchangeCode == ExchangesInternalCodes.Btce)
                {
                    ret = new BtceWrapper();
                }
                else if (exchangeCode == ExchangesInternalCodes.Cryptsy)
                {
                    ret = new CryptsyWrapper();
                }
                else
                {
                    throw new ArgumentException("Unknown exchange code " + exchangeCode);
                }
                _singletons[exchangeCode] = ret;
            }
            return ret;
        }

        /// <summary>
        /// Notifies the factory that api key/secret have changed
        /// </summary>
        /// <param name="exchangeCode"></param>
        public static void NotifySettingsChanged(string exchangeCode)
        {
            if (_singletons.ContainsKey(exchangeCode))
            {
                _singletons.Remove(exchangeCode);
            }
        }

    }
}
