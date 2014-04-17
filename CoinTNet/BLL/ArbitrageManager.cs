using BtcE;
using CoinTNet.Common;
using CoinTNet.Common.Constants;
using CoinTNet.DAL;
using CoinTNet.DAL.Exchanges;
using CoinTNet.DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArbitrageAction = CoinTNet.DO.NTree<CoinTNet.BLL.MyAction>;

namespace CoinTNet.BLL
{
    /// <summary>
    /// Class in charge of doing arbitrage trading within BTC-e only
    /// This is more of a POC than anything else
    /// </summary>
    class ArbitrageManager
    {

        public delegate void ReportProgressHandler(string message);

        public event ReportProgressHandler ReportProgress;

        #region Private members

        private BtcePair[] _allowedPairs;
        private string[] _pairsAsString;
        private decimal _fee;

        private List<NTree<MyAction>> _currentChain;
        private BtceApi _proxy;
        /// <summary>
        /// Whether we must stop
        /// </summary>
        private bool _mustStop;

        #endregion


        public ArbitrageManager()
        {
            _fee = 0.2m;
            _proxy = (ExchangeProxyFactory.GetProxy(ExchangesInternalCodes.Btce) as BtceWrapper).BtceProxy;
        }
        /// <summary>
        /// Notifies the manager to stop
        /// </summary>
        public void NotifyStop()
        {
            _mustStop = true;
        }

        /// <summary>
        /// Starts the arbitrage
        /// </summary>
        /// <param name="originalAmount"></param>
        /// <param name="targetCurrency"></param>
        /// <param name="frequencyInSec"></param>
        /// <param name="profitThreshold"></param>
        /// <param name="realTrading"></param>
        /// <param name="allowedPairs"></param>
        public void Start(decimal originalAmount, string targetCurrency, int frequencyInSec, decimal profitThreshold, bool realTrading, BtcePair[] allowedPairs)
        {
            realTrading = false; //!!!DO not use real trading

            _allowedPairs = allowedPairs;
            _pairsAsString = _allowedPairs.Select(p => BtcePairHelper.ToString(p)).ToArray();

            _mustStop = false;
            OnReportProgress("Starting Arbitrage - Monitoring opportunities...");
            while (!_mustStop)
            {
                Dictionary<BtcePair, Ticker> tickers;
                try
                {
                    tickers = BtceApiV3.GetTicker(_allowedPairs);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    OnReportProgress("Error: " + ex.ToString());
                    System.Threading.Thread.Sleep(1000 * frequencyInSec);
                    continue;
                }


                var pairs = _allowedPairs.Where(p => p.HasCurrency(targetCurrency));

                var ac = new MyAction
                {
                    UnitsCurrency1 = 0,
                    UnitsCurrency2 = originalAmount,
                    Pair = BtcePair.Unknown

                };
                NTree<MyAction> tree = new NTree<MyAction>(ac);


                foreach (var p in pairs)
                {
                    BuildArbitrageTree(tickers, p, tree, originalAmount, p.Item1() == targetCurrency, targetCurrency);
                }

                var leaves = new List<NTree<MyAction>>();
                tree.Traverse(n =>
                {
                    if (n.Data.IsFinalAction)
                    {
                        leaves.Add(n);
                    }
                });

                decimal maxProfit = 0;
                List<NTree<MyAction>> bestChain = null;
                int bestIndex = 0;

                for (var lIndex = 0; lIndex < leaves.Count; lIndex++)
                {
                    //  System.Diagnostics.Debug.WriteLine("Option " + (lIndex + 1));
                    var l = leaves[lIndex];
                    var t = l.GetTree();
                    for (var nIndex = 1; nIndex < t.Count; nIndex++)
                    {
                        var c = t[nIndex].Data;
                        //System.Diagnostics.Debug.WriteLine(string.Format("Converting {0:0.00###} {1:0.00###} to {2:0.00###} {3:0.00###}", c.UnitsCurrency1, c.Currency1, c.UnitsCurrency2, c.Currency2));
                    }
                    decimal profit = l.Data.UnitsCurrency2 - originalAmount;
                    // System.Diagnostics.Debug.WriteLine("Profit " + profit.ToString("0.00###"));
                    if (profit > maxProfit)
                    {
                        maxProfit = l.Data.UnitsCurrency2 - originalAmount;
                        bestChain = t;
                        bestIndex = lIndex;
                    }
                }

                if (bestChain != null)
                {
                    //System.Diagnostics.Debug.WriteLine("Best Option: " + (bestIndex + 1));
                    OnReportProgress("Max profit: " + maxProfit.ToString("0.00###"));

                    for (var nIndex = 1; nIndex < bestChain.Count; nIndex++)
                    {
                        var c = bestChain[nIndex].Data;
                        OnReportProgress(c.Description);
                    }

                    _currentChain = bestChain;
                    var percentage = maxProfit / originalAmount * 100;
                    OnReportProgress(string.Format("Percentage {0:0.00}", percentage));
                    if (percentage > profitThreshold)
                    {

                        FollowChain(bestChain, realTrading);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No profit possible");
                }

                System.Diagnostics.Debug.WriteLine("=======================================================================");
                System.Threading.Thread.Sleep(1000 * frequencyInSec);


            }
        }

        /// <summary>
        /// Recursive method to build an arbitrage tree
        /// </summary>
        /// <param name="tickerDic"></param>
        /// <param name="pair"></param>
        /// <param name="parentNode"></param>
        /// <param name="amount"></param>
        /// <param name="isSell"></param>
        /// <param name="targetCurrency"></param>
        private void BuildArbitrageTree(Dictionary<BtcePair, Ticker> tickerDic, BtcePair pair, ArbitrageAction parentNode, decimal amount, bool isSell, string targetCurrency)
        {
            var ticker = tickerDic[pair];
            decimal rate = isSell ? ticker.Sell : ticker.Buy;
            //rate = ticker.Last;

            var ac = new MyAction
            {
                Currency1 = isSell ? pair.Item1() : pair.Item2(),
                Currency2 = isSell ? pair.Item2() : pair.Item1(),
                UnitsCurrency1 = amount,
                UnitsCurrency2 = isSell ? amount * rate - (amount * rate * _fee / 100) : amount / rate - (amount / rate) * _fee / 100,
                Pair = pair,
                Rate = rate,
                IsBuyOrder = !isSell
            };
            var newNode = parentNode.AddChild(ac);

            var currencyNow = isSell ? pair.Item2() : pair.Item1();

            if (currencyNow == targetCurrency)
            {
                newNode.Data.IsFinalAction = true;
                return; //Done for that branch of the tree
            }
            //Check next possible conversions
            foreach (var p in _allowedPairs)
            {

                if (p == pair || p.Item1() != currencyNow && p.Item2() != currencyNow)
                {
                    continue;//Current, or unrelated pair
                }
                var parent = parentNode;
                bool found = false;
                if (p.Item1() != targetCurrency && p.Item2() != targetCurrency)
                {
                    //If related pair, but not toing to target currency, check if we've already been through one of the currencies

                    while (parent != null)
                    {
                        if (p == parent.Data.Pair || p.Item1() == parent.Data.Currency1 || p.Item1() == parent.Data.Currency2 || p.Item2() == parent.Data.Currency1 || p.Item2() == parent.Data.Currency2)
                        {
                            found = true;
                            break;
                        }
                        parent = parent.Parent;
                    }
                    if (found)
                    {
                        continue;//We've already used one of the currencies in that pair
                    }
                }

                BuildArbitrageTree(tickerDic, p, newNode, ac.UnitsCurrency2, p.Item1() == currencyNow, targetCurrency);
            }
        }

        /// <summary>
        /// Places orders to follow the conversions
        /// </summary>
        /// <param name="chain"></param>
        /// <param name="realTrading">Whether to place the orders for real or not</param>
        private void FollowChain(List<ArbitrageAction> chain, bool realTrading)
        {
            OnReportProgress("==============================");
            OnReportProgress(DateTime.Now.ToString("HH:mm:ss"));
            for (int i = 1; i < chain.Count; i++)
            {
                if (_mustStop)
                {
                    return;
                }
                var ac = chain[i].Data;
                var stepMsg = string.Format("Step {1}/{2}  - {0}",
                    ac.Description, i, chain.Count - 1);
                OnReportProgress(stepMsg);

                int orderId = 0;
                if (realTrading)
                {
                    var orderRes = ac.IsBuyOrder ? _proxy.Trade(ac.Pair, TradeType.Buy, ac.Rate, ac.UnitsCurrency2) : _proxy.Trade(ac.Pair, TradeType.Sell, ac.Rate, ac.UnitsCurrency1);
                    orderId = orderRes.OrderId;
                }
                else
                {
                    orderId = new Random().Next(300);
                }


                OnReportProgress(string.Format("Order #{0} placed. Waiting for its execution", orderId));

                while (!_mustStop)
                {
                    System.Threading.Thread.Sleep(500);
                    var openOrders = _proxy.GetOpenOrders(ac.Pair);
                    if (!openOrders.List.Any(o => o.Key == orderId))
                    {
                        OnReportProgress(string.Format("Order #{0} executed", orderId));
                        break;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Waiting for order #{0} to be executed", orderId));
                    }

                }

            }

            OnReportProgress(string.Format("Finished"));

        }

        /// <summary>
        /// Triggers the ReportProgress event to send messages
        /// </summary>
        /// <param name="msg"></param>
        private void OnReportProgress(string msg)
        {
            if (this.ReportProgress != null)
            {
                this.ReportProgress(msg);
            }
            System.Diagnostics.Debug.WriteLine(msg);
        }


    }
    [DebuggerDisplay("From {Currency1} to {Currency2}")]
    class MyAction
    {
        public decimal UnitsCurrency1 { get; set; }
        public decimal UnitsCurrency2 { get; set; }
        public BtcePair Pair { get; set; }
        public string Currency1 { get; set; }
        public string Currency2 { get; set; }
        public decimal Rate { get; set; }
        public bool IsBuyOrder { get; set; }
        public bool IsFinalAction { get; set; }

        public string Description
        {
            get
            {
                return IsBuyOrder ? string.Format("Buying {0:0.00###} {1:0.00###} @ {2:0.00###} ==> {0:0.00###} {1:0.00###}",
                    UnitsCurrency2, Currency2, Rate) :

                    string.Format("Selling {0:0.00###} {1:0.00###} @ {4:0.00###} ==> {2:0.00###} {3:0.00###} ",
                            UnitsCurrency1, Currency1, UnitsCurrency2, Currency2, Rate);

            }
        }
    }

    /// <summary>
    /// Extensions for currency pairs
    /// </summary>
    static class PairExtensions
    {
        public static string Item1(this BtcePair p)
        {
            return BtcePairHelper.ToString(p).Split(new char[] { '_' })[0];
        }
        public static string Item2(this BtcePair p)
        {
            return BtcePairHelper.ToString(p).Split(new char[] { '_' })[1];
        }

        public static bool HasCurrency(this BtcePair p, string currency)
        {
            return BtcePairHelper.ToString(p).Contains(currency);
        }
    }
}
