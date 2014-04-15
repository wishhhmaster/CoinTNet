using CoinTNet.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTNet.BLL.TradingIndicator
{
    interface ITradingIndicator
    {
        Advice GetAdvice(IList<OHLC> candles);
    }
}
