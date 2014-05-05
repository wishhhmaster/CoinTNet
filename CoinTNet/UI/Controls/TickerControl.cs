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
                            EventAggregator.Instance.Publish(new TickerUpdateMessage { Ticker = ticker });
                            lblLast.Text = ticker.Last.ToString("0.00######", CultureInfo.InvariantCulture);
                            lblLow.Text = ticker.Low.ToString("0.00######", CultureInfo.InvariantCulture);
                            lblHigh.Text = ticker.High.ToString("0.00######", CultureInfo.InvariantCulture);
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
