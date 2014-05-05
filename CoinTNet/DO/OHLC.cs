using System;

namespace CoinTNet.DO
{
    /// <summary>
    /// Represents a candle
    /// </summary>
    public class OHLC
    {
        /// <summary>
        /// Gets or sets the start date of the candle
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the start date of the candle in Unix time
        /// </summary>
        public long TimeStamp { get; set; }
        /// <summary>
        /// Gets or sets the highest price during the candle's period
        /// </summary>
        public decimal High { get; set; }
        /// <summary>
        /// Gets or sets the lowest price during the candle's period
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// Gets or sets the opening price for the candle
        /// </summary>
        public decimal Open { get; set; }
        /// <summary>
        /// Gets or sets the closing price for the candle
        /// </summary>
        public decimal Close { get; set; }
        /// <summary>
        /// Gets or sets the number of trades during the candle's period
        /// </summary>
        public int TradesCount { get; set; }
        /// <summary>
        /// Gets or sets the highest price during the candle's period
        /// </summary>
        public TradeSource TradeSource { get; set; }

        /// <summary>
        /// Returns a visual representation of the candle (debugging)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} O:{1:0.00######} H:{2:0.00######} L:{3:0.00######} C:{4:0.00######}", Date, Open, High, Low, Close);
        }
    }


    /// <summary>
    /// Represents a caculated moving average
    /// </summary>
    public class MovingAverage
    {
        /// <summary>
        /// Gets or sets the period for the average
        /// </summary>
        public int Period { get; set; }
        public int Begin { get; set; }
        public int Length { get; set; }
        public double[] Output { get; set; }
    }
}
