using CoinTNet.Common;
using CoinTNet.Common.Constants;
using CoinTNet.DAL;
using CoinTNet.DO;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using CoinTNet.UI.Forms;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls
{
    /// <summary>
    /// Displays a ticker for the selected currency pair
    /// </summary>
    public partial class TickerControl : UserControl
    {
        #region Private members
        /// <summary>
        /// The last time the ticker was updated
        /// </summary>
        private DateTime _lastUpdateDttm = DateTime.Now;
        /// <summary>
        //Task A token associated with the ticker task
        /// </summary>
        private CancellationTokenSource _tickerToken;
        /// <summary>
        /// The selected pair
        /// </summary>
        private CurrencyPair _selectedPair;
        /// <summary>
        /// The last price
        /// </summary>
        private decimal _lastPrice;
        /// <summary>
        /// The lowest price if the last 24 h (hack for Cryptsy)
        /// </summary>
        private decimal? _24Low;
        /// <summary>
        /// The highest price if the last 24 h (hack for Cryptsy)
        /// </summary>
        private decimal? _24High;
        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public TickerControl()
        {
            InitializeComponent();
            EventAggregator.Instance.Subscribe<PairSelected>(m =>
            {
                if (m.SelectorType == SelectorType.Main)
                {
                    _selectedPair = m.Pair;
                    if (_tickerToken != null)
                    {
                        _tickerToken.Cancel();
                        _lastPrice = 0;
                    }
                    _24High = _24Low = null;
                    //If Cryptsy, we cache the last 24 hour high/low
                    if (m.Pair.Exchange.InternalCode == ExchangesInternalCodes.Cryptsy)
                    {
                        var mRes = (ExchangeProxyFactory.GetProxy(_selectedPair.Exchange.InternalCode) as DAL.Exchanges.CryptsyWrapper).CryptsyProxy.GetActiveMarkets();
                        if (mRes.Success)
                        {
                            var market = mRes.Result.Find(ma => ma.ID == _selectedPair.ID);
                            _24Low = market.Low;
                            _24High = market.High;
                        }
                    }


                }
            });
            this.Load += (s, e) =>
                {
                    if (!this.DesignMode && !(ParentForm as MainForm).IsDesignMode)
                    {
                        tmrTicker.Start();

                    }
                };
        }

        /// <summary>
        /// Updates the ticker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrTicker_Tick(object sender, EventArgs e)
        {
            var diff = (DateTime.Now - _lastUpdateDttm);
            lblLastUpdate.Text = string.Format("{0:0} seconds ago", diff.TotalSeconds);
            if (diff.TotalSeconds > 2)
            {
                if (_tickerToken == null)
                {
                    _tickerToken = new CancellationTokenSource();
                }
                var proxy = ExchangeProxyFactory.GetProxy(_selectedPair.Exchange.InternalCode);

                Task.Factory.StartNew(() => { return proxy.GetTicker(_selectedPair); }, _tickerToken.Token)
                    .ContinueWith(task =>
                    {
                        _tickerToken = null;
                        if (task.Status == TaskStatus.Canceled)
                        {
                            return;
                        }
                        var tickerRes = task.Result;
                        if (tickerRes.Success)
                        {
                            var ticker = tickerRes.Result;

                            //Hack for Cryptsy: to avoid querying all markets data every x seconds to get low/high,
                            //we do it only once and then keep track of these values
                            if (_24High.HasValue)
                            {
                                _24High = ticker.Last > _24High ? ticker.Last : _24High;
                                ticker.High = _24High.Value;
                            }

                            if (_24Low.HasValue)
                            {
                                _24Low = ticker.Last < _24Low ? ticker.Last : _24Low;
                                ticker.Low = _24Low.Value;
                            }


                            EventAggregator.Instance.Publish(new TickerUpdateMessage { Ticker = ticker });
                            lblLast.Text = ticker.Last.ToStandardFormat();
                            lblLow.Text = ticker.Low.ToStandardFormat();
                            lblHigh.Text = ticker.High.ToStandardFormat();
                            _lastUpdateDttm = DateTime.Now;
                            lblLastUpdate.ForeColor = Color.Black;
                            lblLast.ForeColor = ticker.Last >= _lastPrice ? Color.LimeGreen : Color.Red;
                            _lastPrice = ticker.Last;
                        }
                        else
                        {
                            lblLastUpdate.ForeColor = Color.Red;
                        }
                    }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        /*
         * //To use bitstamp's streaming API
                        var op = new PusherClient.PusherOptions
                            {
                                 Encrypted = true,
                                 // Authorizer = new PusherClient.HttpAuthorizer()
                            };
                        //op = null;
                        var c = new PusherClient.Pusher("de504dc5763aeef9ff52", op);
                        c.ConnectionStateChanged += (sender, state)
                            =>
                            {
                                int k = 0;
                            };
                        c.Connected += (sen) =>
                        {
                            var chan = c.Subscribe("live_trades");

                            chan.Bind("trade", (o) =>
                            {
                                int l = 1;
                            });
                        };
                        c.Connect();
                        int b = 0;*/
    }
}
