using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTNet.DO.Strategies
{
    public enum ActionType
    {
        Bid,
        Ask
    }

    class MarketAction
    {
        public ActionType ActionType { get; set; }
        public DateTime Date { get; set; }
        public decimal AmountCurrency { get; set; }
        public decimal AmountItem { get; set; }
        public bool WaitingConfirmation { get; set; }
    }

    class SimulationResult
    {
        public MovingAverage ShortMA { get; set; }
        public MovingAverage LongMA { get; set; }
        public double BuyThreshold { get; set; }
        public double SellThreshold { get; set; }
        public decimal Performance { get; set; }
        public decimal BuyAndHold { get; set; }
        public IList<MarketAction> Actions { get; set; }
    }
}
