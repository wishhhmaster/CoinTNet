using CoinTNet.BLL.Attributes;
using CoinTNet.BLL.TradingIndicators;
using CoinTNet.BLL.TradingStrategies;
using CoinTNet.DO;

namespace CoinTNet.BLL
{
    [Strategy("Simple Strategy", typeof(SimpleStrategySettings))]
    class SimpleTradingStrategy : BaseTradingStrategy, ITradingStrategy
    {
        public SimpleTradingStrategy(SimpleStrategySettings settings)
            : base(settings, new UpDownIndicator(settings.BuyThreshold, settings.SellThreshold))
        {
        }

    }
}
