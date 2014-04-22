using System;

namespace BtcE
{
	public enum BtcePair
	{
		btc_usd,
		btc_rur,
		btc_eur,
        ltc_eur,
		ltc_btc,
		ltc_usd,
		ltc_rur,
		nmc_btc,
        nmc_usd,
		nvc_btc,
		usd_rur,
		eur_usd,
		trc_btc,
		ppc_btc,
        ppc_usd,
		ftc_btc,
        ltc_cnh,
        btc_cnh,
        usd_cnh,
        btc_gbp,
        ltc_gbp,
        gbp_usd,
        Unknown
	}

	public class BtcePairHelper
	{
		public static BtcePair FromString(string s) {
			BtcePair ret = BtcePair.Unknown;
            if (!Enum.TryParse<BtcePair>(s.ToLowerInvariant(), out ret))
            {
                ret = BtcePair.Unknown;
            }
			return ret;
		}
		public static string ToString(BtcePair v) {
			return Enum.GetName(typeof(BtcePair), v).ToLowerInvariant();
		}
	}
}
