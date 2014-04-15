using CoinTNet.DO;
using System.Collections.Generic;
using System.Linq;
using TicTacTec.TA.Library;

namespace CoinTNet.BLL.Analysis
{
    /// <summary>
    /// contains methods to calculate moving averages
    /// </summary>
    class MovingAverages
    {
        /// <summary>
        /// Calculates the moving averages for a set of candles
        /// </summary>
        /// <param name="candles">The candles</param>
        /// <param name="periodsAverage">The EMA period</param>
        /// <returns></returns>
        public static MovingAverage CalculateEMA(IList<OHLC> candles, int periodsAverage)
        {
            double[] closePrice = candles.Select(x => (double)x.Close).ToArray();
            double[] output = new double[closePrice.Length];
            int begin;
            int length;

            TicTacTec.TA.Library.Core.RetCode retCode = Core.Ema(0, closePrice.Length - 1, closePrice, periodsAverage, out begin, out length, output);

            if (retCode == TicTacTec.TA.Library.Core.RetCode.Success)
            {
                return new MovingAverage() { Begin = begin, Length = length, Output = output, Period = periodsAverage };
            }

            return null;
        }

    }
}
