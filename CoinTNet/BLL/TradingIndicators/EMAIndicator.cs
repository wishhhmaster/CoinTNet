using CoinTNet.BLL.TradingIndicator;
using CoinTNet.DO;
using System.Collections.Generic;
using System.Linq;

namespace CoinTNet.BLL.TradingIndicators
{
    class EMAIndicator : ITradingIndicator
    {
        private int _shortEMAPeriod;
        private int _longEMAPeriod;
        private double _buyThreshold;
        private double _sellThreshold;
        private TrendDirection _trend;

        public EMAIndicator(int shortEMAPeriod, int longEMAPeriod, double buyThreshold, double sellThreshold)
        {
            _shortEMAPeriod = shortEMAPeriod;
            _longEMAPeriod = longEMAPeriod;
            _buyThreshold = buyThreshold;
            _sellThreshold = sellThreshold;
        }

        public Advice GetAdvice(IList<OHLC> candles)
        {
            var shortMA = Analysis.MovingAverages.CalculateEMA(candles, _shortEMAPeriod);
            var longMA = Analysis.MovingAverages.CalculateEMA(candles, _longEMAPeriod);
            if (shortMA == null || longMA == null)
            {
                return Advice.None;
            }
            int i = candles.Count - 1;
            var diff = 100 * (shortMA.Output[i - shortMA.Begin] - longMA.Output[i - longMA.Begin]) / ((longMA.Output[i - longMA.Begin] + shortMA.Output[i - shortMA.Begin]) / 2);

            var candle = candles.Last();

            if (diff > _buyThreshold)
            {
                if (_trend != TrendDirection.Up)
                {
                    _trend = TrendDirection.Up;
                    return Advice.Buy;
                }
                //  return Advice.Buy;
                return Advice.None;
            }

            else if (diff < _sellThreshold)
            {
                if (_trend != TrendDirection.Down)
                {
                    _trend = TrendDirection.Down;
                    return Advice.Sell;
                }
                // return Advice.Sell;
                return Advice.None;
            }
            return Advice.None;
        }
    }
}
