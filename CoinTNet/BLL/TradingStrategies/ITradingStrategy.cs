using CoinTNet.DO;
using CoinTNet.DO.Strategies;
using System.Collections.Generic;

namespace CoinTNet.BLL.TradingStrategies
{
    interface ITradingStrategy
    {
        void OnTick(IList<OHLC> candles);
        StrategyResult GetResult();
        List<MarketAction> Actions { get; }
    }

}
