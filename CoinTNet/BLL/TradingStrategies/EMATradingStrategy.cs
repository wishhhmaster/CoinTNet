using CoinTNet.BLL.TradingStrategies;
using CoinTNet.BLL.TradingIndicators;
using CoinTNet.DO;
using CoinTNet.BLL.Attributes;

namespace CoinTNet.BLL
{
    [Strategy("EMA Strategy", typeof(EMAStrategySettings))]
    class EMATradingStrategy : BaseTradingStrategy, ITradingStrategy
    {
        public EMATradingStrategy(EMAStrategySettings settings)
            : base(settings, new EMAIndicator(settings.FastEMAPeriod, settings.SlowEMAPeriod, settings.BuyThreshold, settings.SellThreshold))
        {
        }


    }
}
