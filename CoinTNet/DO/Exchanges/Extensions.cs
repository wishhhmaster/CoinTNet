using BtcE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTNet.DO.Exchanges
{
    /// <summary>
    /// Extensions for currency pairs
    /// </summary>
    static class PairExtensions
    {
        public static string Item1(this BtcePair p)
        {
            return BtcePairHelper.ToString(p).Split(new char[] { '_' })[0];
        }
        public static string Item2(this BtcePair p)
        {
            return BtcePairHelper.ToString(p).Split(new char[] { '_' })[1];
        }

        public static bool HasCurrency(this BtcePair p, string currency)
        {
            return BtcePairHelper.ToString(p).Contains(currency);
        }
    }
}
