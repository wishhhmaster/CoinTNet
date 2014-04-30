using System;

namespace BtcE
{
    public enum BtcePair
    {
        btc_usd,
        btc_rur,
        btc_eur,
        btc_cnh,
        btc_gbp,
        ltc_btc,
        ltc_usd,
        ltc_rur,
        ltc_eur,
        ltc_cnh,
        ltc_gbp,
        nmc_btc,
        nmc_usd,
        nvc_btc,
        nvc_usd,
        usd_rur,
        eur_usd,
        usd_cnh,
        gbp_usd,
        trc_btc,
        ppc_btc,
        ppc_usd,
        ftc_btc,
        xpm_btc,
        Unknown
    }

    public class BtcePairHelper
    {
        public static BtcePair FromString(string s)
        {
            BtcePair ret = BtcePair.Unknown;
            if (!Enum.TryParse<BtcePair>(s.ToLowerInvariant(), out ret))
            {
                ret = BtcePair.Unknown;
            }
            return ret;
        }
        public static string ToString(BtcePair v)
        {
            return Enum.GetName(typeof(BtcePair), v).ToLowerInvariant();
        }
    }
}
