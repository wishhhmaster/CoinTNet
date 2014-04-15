using CoinTNet.BLL;
using CoinTNet.BLL.TradingStrategies;
using CoinTNet.DO;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTNet.UI.Forms
{
    /// <summary>
    /// Application's main form
    /// </summary>
    partial class MainForm : Form
    {
        #region Private variables
        /// <summary>
        /// Task for updating candles with latest data from API
        /// </summary>
        private Task _updateCandlesTask;
        /// <summary>
        /// The current list of candles
        /// </summary>
        private IList<OHLC> _candles = new List<OHLC>();
        /// <summary>
        /// Used in timer's tick event
        /// </summary>
        private DateTime _lastUpdate = DateTime.MinValue;
        /// <summary>
        /// The selected strategy
        /// </summary>
        private ITradingStrategy _strategy;
        /// <summary>
        /// The currently selected currency pair
        /// </summary>
        private CurrencyPair _selectedPair;
        /// <summary>
        /// The form used for indicators
        /// </summary>
        private IndicatorsForm _indicatorsForm;

        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.Text = CoinTNet.Common.Constants.ApplicationConstants.ApplicationName;
            EventAggregator.Instance.Subscribe<StatusUpdateMessage>(m => UpdateStatus(m.Message));
            //   this.Shown += (s, e) => OnBtnRefreshClick();
            dataSelector.RefreshClicked += (s, e) => OnBtnRefreshClick();
            dataSelector.OnPairChanged += (s, e) => OnPairChanged();
            dataSelector.UseLiveData = true;
            _selectedPair = dataSelector.SelectedPair;
            _indicatorsForm = new IndicatorsForm();

            _indicatorsForm.IndicatorOptionsChanged += (s, options)
                =>
                {
                    myChartControl.NotifyIndicatorsChanged(options);
                };
            myChartControl.NotifyIndicatorsChanged(_indicatorsForm.IndicatorOptions);

            this.Load += (s, e) => EventAggregator.Instance.Publish(new ApplicationStartMessage());
        }

        #region Public Properties

        /// <summary>
        /// Gets the selecged pais
        /// </summary>
        public CurrencyPair SelectedPair
        {
            get { return _selectedPair; }
        }

        /// <summary>
        /// Gets the current candles
        /// </summary>
        public IList<OHLC> Candles
        {
            get { return _candles; }
        }

        /// <summary>
        /// Gets a value indicating whether the form is on design mode
        /// </summary>
        public bool IsDesignMode
        {
            get { return DesignMode; }
        }
        #endregion

        #region Events

        /// <summary>
        /// Lets the user pick a file to which save currently displayed candles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCandles_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.RestoreDirectory = true;
                sfd.InitialDirectory = Application.ExecutablePath;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CandlesProvider.SaveCandlesToCSV(sfd.FileName, _candles);
                }
            }
        }

        /// <summary>
        /// Click on the refresh button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBtnRefreshClick()
        {
            LoadCandles(dataSelector.FromDate, dataSelector.SelectedPeriodInMin, dataSelector.ToDate);
        }

        /// <summary>
        /// Triggered when the slow timer ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSlowTimerTick(object sender, EventArgs e)
        {

            if (_updateCandlesTask == null || _updateCandlesTask.IsCompleted || _updateCandlesTask.IsFaulted || _updateCandlesTask.IsCanceled)
            {
                if (DateTime.UtcNow.Subtract(_lastUpdate).TotalSeconds >= 5)
                {
                    int period = dataSelector.SelectedPeriodInMin;
                    var pair = SelectedPair;
                    var updateTask = Task.Factory.StartNew(() => { UpdateCandles(period, pair); });
                    _updateCandlesTask = updateTask.ContinueWith(_ =>
                    {
                        if (_candles.Count > 0)
                        {
                            myChartControl.DrawChart(_candles, Constants.PriceSerieName);
                        }
                    }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext())
                    .ContinueWith(t =>
                    {
                        if (_strategy != null)
                        {
                            _strategy.OnTick(_candles);
                        }
                    });
                }
            }
            else if (_updateCandlesTask != null)
            {
                lblStatus.Text = lblStatus.Text + ".";
            }
        }

        /// <summary>
        /// Called when slow timer ticks, to update displayed candles with latest data
        /// </summary>
        /// <param name="fetchLiveData"></param>
        /// <param name="candlesDurationInMin"></param>
        private void UpdateCandles(int candlesDurationInMin, CurrencyPair pair)
        {
            lock (this)
            {

                // First load history
                if (_candles.Count > 0)
                {
                    UpdateStatus("Fetching new data");
                    CandlesProvider.UpdateCandlesWithLiveData(candlesDurationInMin, _candles, pair);
                }


                try
                {
                    UpdateStatus("Ready...");
                }
                catch (Exception ex)
                {
                    UpdateStatus(string.Format("Error during Update: {0}", ex.Message));
                }
            }
        }

        /// <summary>
        /// Updates the status message
        /// </summary>
        /// <param name="message"></param>
        private void UpdateStatus(string message)
        {
            if (!this.InvokeRequired)
            {
                lblStatus.Text = message;
            }
            else
            {
                this.BeginInvoke(new Action<string>(UpdateStatus), new object[] { message });
            }
        }


        #endregion

        #region Init methods

        /// <summary>
        /// Loads candles after clicking on Refresh button
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="isRealtime"></param>
        /// <param name="candlesDurationInMinutes"></param>
        /// <param name="maxDate"></param>
        private void LoadCandles(DateTime minDate, int candlesDurationInMinutes, DateTime? maxDate)
        {
            var currencyPair = dataSelector.SelectedPair;
            Task.Factory.StartNew(() => CandlesProvider.LoadCandles(currencyPair, minDate, candlesDurationInMinutes, maxDate))
                .ContinueWith(taskResult =>
                {
                    if (taskResult.Result != null)
                    {
                        _candles = taskResult.Result;
                        myChartControl.DrawChart(taskResult.Result, Constants.PriceSerieName);
                    }
                }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        #endregion

        #region Control Events


        private void OnShowForecastChanged(object sender, EventArgs e)
        {
            myChartControl.Forecast();
        }

        private void OnPairChanged()
        {
            _candles.Clear();
            myChartControl.Clear();
            _selectedPair = dataSelector.SelectedPair;
            OnBtnRefreshClick();
        }

        #endregion

        /// <summary>
        /// Shows the strategy form for live trading.
        /// Not entirely finished
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStrategy_Click(object sender, EventArgs e)
        {
            using (var f = new BackTestingForm())
            {
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _strategy = f.TradingStrategy;
                }
            }
        }

        /// <summary>
        /// Shows the arbitrage form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArbitrage_Click(object sender, EventArgs e)
        {
            var f = new ArbitrageForm();
            f.Show(this);

        }

        /// <summary>
        /// Shows the indicator form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIndicators_Click(object sender, EventArgs e)
        {
            if (!_indicatorsForm.Visible)
            {
                _indicatorsForm.Show();
            }

        }

        private void btnBackTesting_Click(object sender, EventArgs e)
        {
            var asf = new BackTestingForm(true);
            asf.Show();
        }

    }

}
