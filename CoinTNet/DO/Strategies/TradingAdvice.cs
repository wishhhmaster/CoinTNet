using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTNet.DO
{
    enum TrendDirection
    {
        None,
        Up,
        Down

    }

    enum Advice
    {
        Buy,
        Sell,
        None
    }

    class StrategySettings
    {
        public decimal MinAmountItem2ToKeep { get; set; }
        public decimal MinAmountItem1ToKeep { get; set; }
        public decimal MaxAmountMoneyPerBuyOrder { get; set; }
        public decimal MaxNbBTCPerSellOrder { get; set; }

        public decimal InitialItem2Balance { get; set; }
        public decimal InitialItem1Balance { get; set; }
        public decimal Fee { get; set; }

        public CurrencyPair Pair { get; set; }
    }
    class SimpleStrategySettings : StrategySettings
    {
        public double BuyThreshold { get; set; }
        public double SellThreshold { get; set; }

    }

    class EMAStrategySettings : StrategySettings
    {
        public int SlowEMAPeriod { get; set; }
        public int FastEMAPeriod { get; set; }
        public double BuyThreshold { get; set; }
        public double SellThreshold { get; set; }
    }
    class MACDStrategySettings: EMAStrategySettings
    {
        public int SignalPeriod { get; set; }
    }

    class StrategyResult
    {
        public decimal BalanceItem2 { get; set; }
        public decimal BalanceItem1 { get; set; }
    }

   
}
