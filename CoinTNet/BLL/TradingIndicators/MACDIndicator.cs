using CoinTNet.BLL.TradingIndicator;
using CoinTNet.DO;
using System.Collections.Generic;
using System.Linq;

namespace CoinTNet.BLL.TradingIndicators
{
    class MACDIndicator : ITradingIndicator
    {
        private int _shortEMAPeriod;
        private int _longEMAPeriod;
        private double _buyThreshold;
        private double _sellThreshold;
        private int _signalPeriod;
        private Trend _trend;
        class Trend
        {
            public TrendDirection Direction { get; set; }
            public int Duration { get; set; }
            public bool Persisted { get; set; }
            public bool Adviced { get; set; }

        }
        public MACDIndicator(int shortEMAPeriod, int longEMAPeriod, int signalPeriod, double buyThreshold, double sellThreshold)
        {
            _shortEMAPeriod = shortEMAPeriod;
            _longEMAPeriod = longEMAPeriod;
            _buyThreshold = buyThreshold;
            _sellThreshold = sellThreshold;
            _signalPeriod = signalPeriod;
            _trend = new Trend { Direction = TrendDirection.None };
        }

        public Advice GetAdvice(IList<OHLC> candles)
        {
            var shortMA = Analysis.MovingAverages.CalculateEMA(candles, _shortEMAPeriod);
            var longMA = Analysis.MovingAverages.CalculateEMA(candles, _longEMAPeriod);


            if (shortMA == null || longMA == null)
            {
                return Advice.None;
            }

            int firstIndex = _longEMAPeriod - 1;
            if (candles.Count < _longEMAPeriod + _signalPeriod - 1)
            {
                return Advice.None;
            }

            int i = candles.Count - 1;

            var emaDiff = new OHLC[i - firstIndex + 1];
            for (int k = firstIndex; k <= i; k++)
            {
                emaDiff[k - firstIndex] = new OHLC
                    {
                        Close = (decimal)shortMA.Output[k - shortMA.Begin] - (decimal)longMA.Output[k - longMA.Begin]
                    };
            }

            var signal = Analysis.MovingAverages.CalculateEMA(emaDiff, _signalPeriod);
            if (signal == null)
            {
                return Advice.None;
            }

            var macdDiff = shortMA.Output[i - shortMA.Begin] - longMA.Output[i - longMA.Begin] - signal.Output[i - firstIndex - signal.Begin];


            if (macdDiff > 0.025/* settings.thresholds.up*/)
            {

                // new trend detected
                if (this._trend.Direction != TrendDirection.Up)
                    // reset the state for the new trend
                    this._trend = new Trend
                    {
                        Duration = 0,
                        Persisted = false,
                        Direction = TrendDirection.Up,
                        Adviced = false
                    };

                this._trend.Duration++;

                //log.debug('In uptrend since', this.trend.duration, 'candle(s)');

                if (this._trend.Duration >= 1/*settings.thresholds.persistence*/)
                    this._trend.Persisted = true;

                if (this._trend.Persisted && !this._trend.Adviced)
                {
                    this._trend.Adviced = true;
                    return Advice.Buy;
                }
                else return Advice.None;

            }
            else if (macdDiff < -0.025/* settings.thresholds.down*/)
            {

                // new trend detected
                if (this._trend.Direction != TrendDirection.Down)
                    // reset the state for the new trend
                    this._trend = new Trend
                    {
                        Duration = 0,
                        Persisted = false,
                        Direction = TrendDirection.Down,
                        Adviced = false
                    };

                this._trend.Duration++;

                //log.debug('In downtrend since', this.trend.duration, 'candle(s)');

                if (this._trend.Duration >= 1/*settings.thresholds.persistence*/)
                {
                    this._trend.Persisted = true;
                }

                if (this._trend.Persisted && !this._trend.Adviced)
                {
                    this._trend.Adviced = true;
                    return Advice.Sell;
                }
                else
                    return Advice.None;

            }
            else
            {
                return Advice.None;
            }

        }
    }
}
