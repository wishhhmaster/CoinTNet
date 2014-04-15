using CoinTNet.BLL.Attributes;
using CoinTNet.BLL.TradingIndicators;
using CoinTNet.BLL.TradingStrategies;
using CoinTNet.DO;

namespace CoinTNet.BLL
{
    [Strategy("MACD Strategy", typeof(MACDStrategySettings))]
    class MACDTradingStrategy : BaseTradingStrategy, ITradingStrategy
    {
        public MACDTradingStrategy(MACDStrategySettings settings)
            : base(settings, new MACDIndicator(settings.FastEMAPeriod, settings.SlowEMAPeriod, settings.SignalPeriod, settings.BuyThreshold, settings.SellThreshold))
        {

        }

    }
}
