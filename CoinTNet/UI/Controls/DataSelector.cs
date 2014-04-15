using CoinTNet.Common.Constants;
using CoinTNet.DO;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using CoinTNet.UI.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls
{
    /// <summary>
    /// Lets the user selected which data to display on the chart
    /// </summary>
    partial class DataSelector : UserControl
    {
        #region Events
        /// <summary>
        /// Triggered when the user clicks on the refresh button
        /// </summary>
        public event EventHandler RefreshClicked;
        /// <summary>
        /// Triggered when the user clicks on the button to load a CSV file
        /// </summary>
        public event EventHandler LoadFromCSVClicked;
        /// <summary>
        /// Triggered when the selected pair changes
        /// </summary>
        public event EventHandler OnPairChanged;

        #endregion

        #region Private members
        /// <summary>
        /// Whether we are using live data
        /// </summary>
        private bool _useLiveData;
        /// <summary>
        /// Whether we are loading currency pairs (to prevent event propagation when not wanted)
        /// </summary>
        private bool _loadingPairs;
        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public DataSelector()
        {
            InitializeComponent();
            dtpFrom.Value = DateTime.Now.Date;
            Tuple<string, int>[] values = { Tuple.Create("1 min", 1), Tuple.Create("5 min", 5), Tuple.Create("15 min", 15),
                                          Tuple.Create ("1 hour", 60), Tuple.Create ("2 hours", 2*60),
                                          Tuple.Create ("4 hours", 4*60), Tuple.Create ("8 hours", 8 * 60)};


            cbbPeriodInMin.PopulateCbbFromList(values, t => t.Item1, values[2]);

            btnLoadFromCSV.Enabled = false;
            this.Load += (s, e) =>
            {
                bool parentFormInDesignMode = (ParentForm is MainForm && (ParentForm as MainForm).IsDesignMode) || (ParentForm is BackTestingForm && (ParentForm as BackTestingForm).IsDesignMode);
                if (!this.DesignMode && !parentFormInDesignMode)
                {
                    InitExchanges();
                }
            };

            rdbLoadFile.Enabled = rdbUseAPI.Enabled = false;//For now, can only use API.. To enable loading file, we need to be able to specify currency pair
        }

        #region Public Properties

        /// <summary>
        /// Gets or sets the selector type
        /// </summary>
        public SelectorType SelectorType { get; set; }

        /// <summary>
        /// Gets the selected currency pair
        /// </summary>
        public CurrencyPair SelectedPair
        {
            get { return cbbPairs.GetSelectedValue<CurrencyPair>(); }
        }

        /// <summary>
        /// Gets the selected period, in minutes
        /// </summary>
        public int SelectedPeriodInMin
        {
            get { return cbbPeriodInMin.GetSelectedValue<Tuple<string, int>>().Item2; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are using live data
        /// </summary>
        public bool UseLiveData
        {
            get { return _useLiveData; }
            set
            {
                _useLiveData = value;
                chkNow.Enabled = !value;
                if (value)
                {
                    chkNow.Checked = true;
                }

                rdbUseAPI.Visible = panel1.Visible = !value;
                this.tableLayoutPanel2.ColumnStyles[0] = !value ? new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F) :
                    new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F);
            }
        }

        /// <summary>
        /// Gets the from date
        /// </summary>
        public DateTime FromDate
        {
            get { return dtpFrom.Value; }
        }
        /// <summary>
        /// Gets the To Date (or null if now is Checked)
        /// </summary>
        public DateTime? ToDate
        {
            get { return chkNow.Checked ? (DateTime?)null : dtpTo.Value; }
        }

        #endregion

        /// <summary>
        /// Initialises the exchanges/pairs ddls
        /// </summary>
        private void InitExchanges()
        {

            CurrencyPair[] bitstampCP = new[] { new CurrencyPair("BTC", "USD") };
            var bitstampEx = new Exchange("Bitstamp", bitstampCP, ExchangesInternalCodes.Bitstamp);
            bitstampEx.FeeDeductedFromTotal = false;

            var btceEx = new Exchange("BTC-e", new[]{
                            new CurrencyPair ("BTC", "USD" ),
                            new CurrencyPair ("BTC", "EUR" ),
                            new CurrencyPair ("LTC", "USD" ),
                            new CurrencyPair ("LTC", "BTC" ),
                            new CurrencyPair ("NMC", "USD" )
            }, ExchangesInternalCodes.Btce);
            btceEx.FeeDeductedFromTotal = true;
            var allExchanges = new[] { bitstampEx, btceEx };
            cbbExchange.PopulateCbbFromList(allExchanges, e => e.Name, bitstampEx);
        }

        /// <summary>
        /// Triggers the RefreshClicked event whern the refresh button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshCandles_Click(object sender, EventArgs e)
        {
            if (RefreshClicked != null)
            {
                RefreshClicked(this, e);
            }
        }

        /// <summary>
        /// Publishes a global event when the selectd exchange changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbExchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ex = cbbExchange.GetSelectedValue<Exchange>();
            _loadingPairs = true;
            cbbPairs.PopulateCbbFromList(ex.CurrencyPairs, cp => cp.Description, ex.CurrencyPairs.FirstOrDefault());
            _loadingPairs = false;
            EventAggregator.Instance.Publish(new ExchangeSelected { InternalCode = ex.InternalCode, SelectorType = this.SelectorType });
            cbbPairs_SelectedIndexChanged(cbbPairs, EventArgs.Empty);
        }

        /// <summary>
        /// Triggers the OnPairChanged event when the selected pair changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbPairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingPairs)
            {
                return;
            }
            if (this.OnPairChanged != null)
            {
                this.OnPairChanged(this, e);
            }
            EventAggregator.Instance.Publish(new PairSelected { Pair = cbbPairs.GetSelectedValue<CurrencyPair>(), SelectorType = this.SelectorType });
        }

        /// <summary>
        /// Triggers the LoadFromCSVClicked event when the user clicks on LoadEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadFromCSV_Click(object sender, EventArgs e)
        {
            if (LoadFromCSVClicked != null)
            {
                LoadFromCSVClicked(this, e);
            }
        }

        /// <summary>
        /// Enables/disables control when the source changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbUseAPI_CheckedChanged(object sender, EventArgs e)
        {

            //cbbExchange.Enabled = cbbPairs.Enabled = rdbUseAPI.Checked;
            //btnLoadFromCSV.Enabled = !rdbUseAPI.Checked;
        }

        /// <summary>
        /// Enables/disables the To Date dtp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkNow_CheckedChanged(object sender, EventArgs e)
        {
            dtpTo.Enabled = !UseLiveData && !chkNow.Checked;
            if (chkNow.Checked)
            {
                dtpTo.Value = DateTime.Now;
            }
        }
    }
}
