using CoinTNet.BLL;
using CoinTNet.UI.Common;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTNet.UI.Forms
{
    /// <summary>
    /// Lets the user define settings for BTC-e arbitrage
    /// </summary>
    public partial class ArbitrageForm : Form
    {
        #region Private members
        /// <summary>
        /// Whether the arbitrage manager is started
        /// </summary>
        private bool _started = false;
        /// <summary>
        /// The arbitrage manager
        /// </summary>
        private ArbitrageManager _arbitrageManager;
        /// <summary>
        /// Whether the form is closing (to make sure we don't try to access disposed components when the arbitrage manager sends its messages)
        /// </summary>
        private bool _closing;

        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public ArbitrageForm()
        {
            InitializeComponent();
            _arbitrageManager = new ArbitrageManager();
            _arbitrageManager.ReportProgress += OnProgressReport;
            InitControls();
        }

        /// <summary>
        /// Callback for when the ArbitrageManager sends messages
        /// </summary>
        /// <param name="message"></param>
        private void OnProgressReport(string message)
        {
            if (_closing)
            {
                return;
            }
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(OnProgressReport), message);
                return;
            }
            txtLog.AppendText(message + Environment.NewLine);
        }

        /// <summary>
        /// Starts/stops the arbitrage manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!_started)
            {
                decimal amount = numAmount.Value;
                string currency = cbbCurrency.GetSelectedValue<Tuple<string, string>>().Item2;
                int freq = (int)numFrequency.Value;
                decimal profitThreshold = numProfit.Value;
                bool realTrading = chkRealTrading.Checked;

                var allowedPairs = chkLbAllowedPairs.CheckedItems.ToListExt<object>()
                    .Cast<ListControlItem<Tuple<string, BtcE.BtcePair>>>()
                    .Select(i => i.Item.Item2).ToArray();


                Task.Factory.StartNew(() => _arbitrageManager.Start(amount, currency, freq, profitThreshold, realTrading, allowedPairs))
                    .ContinueWith(t =>
                    {
                        if(t.IsFaulted)
                        {
                            ErrorHelper.DisplayErrorMessage("Une erreur s'est produite :\n" + t.Exception.ToString());
                        }
                        _started = false;
                        btnStart.Text = "Start";
                        btnStart.Enabled = true;
                    }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
                btnStart.Text = "Stop";
                _started = true;
            }
            else
            {
                btnStart.Enabled = false;
                btnStart.Text = "Stopping";
                _arbitrageManager.NotifyStop();
            }
        }

        /// <summary>
        /// Initialises the controls' values
        /// </summary>
        private void InitControls()
        {
            Tuple<string, string>[] values = { Tuple.Create("BTC", "btc"), Tuple.Create("LTC", "ltc"), Tuple.Create("USD", "usd"),
                                          Tuple.Create ("CNH", "cnh")};
            cbbCurrency.PopulateCbbFromList(values, t => t.Item1, values[1]);

            Tuple<string, BtcE.BtcePair>[] values2 = { Tuple.Create("BTC/USD", BtcE.BtcePair.btc_usd), Tuple.Create("LTC/USD", BtcE.BtcePair.ltc_usd), Tuple.Create("LTC/BTC", BtcE.BtcePair.ltc_btc ),
                                          Tuple.Create ("BTC/CNH", BtcE.BtcePair.btc_cnh), Tuple.Create ("LTC/CNH", BtcE.BtcePair.ltc_cnh), Tuple.Create ("USD/CNH", BtcE.BtcePair.usd_cnh),
                                          Tuple.Create ("BTC/EUR", BtcE.BtcePair.btc_eur), Tuple.Create ("LTC/EUR", BtcE.BtcePair.ltc_eur), Tuple.Create ("EUR/USD", BtcE.BtcePair.eur_usd),
                                                     Tuple.Create ("BTC/GBP", BtcE.BtcePair.btc_gbp), Tuple.Create ("LTC/GBP", BtcE.BtcePair.ltc_gbp), Tuple.Create ("GBP/USD", BtcE.BtcePair.gbp_usd)};

            for (int i = 0; i < values2.Length; i++)
            {
                var v = values2[i];
                chkLbAllowedPairs.Items.Add(new ListControlItem<Tuple<string, BtcE.BtcePair>>(v, v.Item1), i < 6);
            }
        }

        /// <summary>
        /// Notifies the arbitrage manager to stop when the form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArbitrageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _arbitrageManager.NotifyStop();
            _closing = true;
        }


    }
}
