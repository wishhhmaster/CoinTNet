using CoinTNet.DAL;
using CoinTNet.DAL.Interfaces;
using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls
{
    /// <summary>
    /// Control used to display the order book
    /// </summary>
    public partial class OrderBookControl : UserControl
    {
        #region Private members
        /// <summary>
        /// The update task
        /// </summary>
        private Task<List<SimpleOrderInfo>[]> _updateTask;
        /// <summary>
        /// The currently selected pair
        /// </summary>
        private CurrencyPair _selectedPair;
        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public OrderBookControl()
        {
            InitializeComponent();
            EventAggregator.Instance.Subscribe<ApplicationStartMessage>(m =>
            {
                UpdateOrderBook();
                tmrRefresh.Start();
            });
            btnRefresh.Click += (s, e) => UpdateOrderBook();
            tmrRefresh.Tick += (s, e) => UpdateOrderBook();
            EventAggregator.Instance.Subscribe<PairSelected>(m =>
            {
                if (m.SelectorType == SelectorType.Main)
                {
                    _selectedPair = m.Pair;
                }
            });
        }

        /// <summary>
        /// Updates the order book
        /// </summary>
        private void UpdateOrderBook()
        {
            if (_updateTask == null)
            {
                _updateTask = Task.Factory.StartNew(() => { return FetchOrders(); });
                _updateTask.ContinueWith(t =>
                {
                    _updateTask = null;
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        DisplayOrders(t.Result);
                    }
                }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        /// <summary>
        /// Fetch the orders and filters which orders must be displayed
        /// </summary>
        /// <returns>A list of asks and a list of bids</returns>
        private List<SimpleOrderInfo>[] FetchOrders()
        {
            var ordersRes =ExchangeProxyFactory.GetProxy(_selectedPair.Exchange.InternalCode).GetOrderBook(_selectedPair);
            if (!ordersRes.Success)
            {
                return new List<SimpleOrderInfo>[0];
            }
            var orders = ordersRes.Result;

            decimal depthRange = numPriceRange.Value;

            var minPriceAsk = orders.Asks.Min(o => o.Price);
            var maxPriceBid = orders.Bids.Max(o => o.Price);

            //We don't want to display all the orders, so we stick to a percentage
            var displayableAsks = orders.Asks.Where(a => Math.Abs(100 * (a.Price - minPriceAsk) / minPriceAsk) < depthRange).ToList();
            var displayableBids = orders.Bids.Where(b => Math.Abs(100 * (b.Price - maxPriceBid) / maxPriceBid) < depthRange).ToList();

            //Aggregates orders
            var aggregatedAsks = displayableAsks.Select(a => new SimpleOrderInfo { Amount = a.Amount + displayableAsks.TakeWhile(o => o != a).Sum(o => o.Amount), Price = a.Price });
            var aggregatedBids = displayableBids.Select(a => new SimpleOrderInfo { Amount = a.Amount + displayableBids.TakeWhile(o => o != a).Sum(o => o.Amount), Price = a.Price });

            return new[] { aggregatedAsks.ToList(), aggregatedBids.ToList() };
        }

        /// <summary>
        /// Displays orders
        /// </summary>
        /// <param name="orders">The list of asks and bids</param>
        private void DisplayOrders(List<SimpleOrderInfo>[] orders)
        {
            var displayableAsks = orders.Length > 1 ? orders[0] : new List<SimpleOrderInfo>();
            var displayableBids = orders.Length > 1 ? orders[1] : new List<SimpleOrderInfo>();

            chartOrderBook.Series["ask"].Points.DataBindXY(displayableAsks, "Price", displayableAsks, "Amount");
            chartOrderBook.Series["bid"].Points.DataBindXY(displayableBids, "Price", displayableBids, "Amount");

            foreach (var point in chartOrderBook.Series["ask"].Points)
            {
                point.ToolTip = string.Format(CultureInfo.InvariantCulture, "Amount : {0}\nPrice : {1:0.00#####}", point.YValues[0], point.XValue);
            }
        }

    }
}
