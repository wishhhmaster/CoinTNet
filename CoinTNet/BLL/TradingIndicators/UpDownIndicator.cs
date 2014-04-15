using CoinTNet.BLL.TradingIndicator;
using CoinTNet.DO;
using System.Collections.Generic;
using System.Linq;

namespace CoinTNet.BLL.TradingIndicators
{
    class UpDownIndicator : ITradingIndicator
    {
        private double _upThreshold;
        private double _downThreshold;
        private TrendDirection _trend;

        public UpDownIndicator(double upThreshold, double downThreshold)
        {
            _upThreshold = upThreshold;
            _downThreshold = downThreshold;
        }

        public Advice GetAdvice(IList<OHLC> candles)
        {

            if (candles.Count < 2)
            {
                return Advice.None;
            }

            var lastCandle = candles[candles.Count - 1];
            var prevCandle = candles[candles.Count - 2];

            //var diff = 100 * (ShortMA.Output[i - ShortMA.Begin] - LongMA.Output[i - LongMA.Begin]) / LongMA.Output[i - LongMA.Begin];
            double diff = (double)((lastCandle.Close - prevCandle.Close)/prevCandle.Close)*100;


            if (diff > _upThreshold)
            {
                if (_trend != TrendDirection.Up)
                {
                    _trend = TrendDirection.Up;
                    return Advice.Buy;
                }
                //  return Advice.Buy;
                return Advice.None;
            }

            else if (diff < _downThreshold)
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
