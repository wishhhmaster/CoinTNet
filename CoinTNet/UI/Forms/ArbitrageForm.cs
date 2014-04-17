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
                string currency = cbbCurrency.GetSelectedValue<string>().ToLower();
                int freq = (int)numFrequency.Value;
                decimal profitThreshold = numProfit.Value;
                bool realTrading = chkRealTrading.Checked;

                var allowedPairs = chkLbAllowedPairs.CheckedItems.ToListExt<object>()
                    .Cast<ListControlItem<BtcE.BtcePair>>()
                    .Select(l => l.Item)
                    .ToArray();


                Task.Factory.StartNew(() => _arbitrageManager.Start(amount, currency, freq, profitThreshold, realTrading, allowedPairs))
                    .ContinueWith(t =>
                    {
                        if (t.IsFaulted)
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
            string[] values = { "BTC", "LTC", "USD", "EUR", "PPC", "NMC", "CNH" };


            cbbCurrency.PopulateCbbFromList(values, t => t, values[1]);

            BtcE.BtcePair[] availablePairs = { BtcE.BtcePair.btc_usd,BtcE.BtcePair.ltc_usd, BtcE.BtcePair.ltc_btc,
                                                         BtcE.BtcePair.nmc_usd, BtcE.BtcePair.nmc_btc, BtcE.BtcePair.ppc_usd, BtcE.BtcePair.ppc_btc,
                                          BtcE.BtcePair.btc_cnh,BtcE.BtcePair.ltc_cnh, BtcE.BtcePair.usd_cnh,
                                          BtcE.BtcePair.btc_eur,  BtcE.BtcePair.ltc_eur, BtcE.BtcePair.eur_usd,
                                                     BtcE.BtcePair.btc_gbp, BtcE.BtcePair.ltc_gbp,  BtcE.BtcePair.gbp_usd};

            for (int i = 0; i < availablePairs.Length; i++)
            {
                var p = availablePairs[i];
                chkLbAllowedPairs.Items.Add(new ListControlItem<BtcE.BtcePair>(p, p.Item1().ToUpper() + "/" + p.Item2().ToUpper()), i < 7);
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
