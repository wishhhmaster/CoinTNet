using CoinTNet.BLL;
using CoinTNet.BLL.Attributes;
using CoinTNet.BLL.TradingStrategies;
using CoinTNet.DO;
using CoinTNet.DO.Strategies;
using CoinTNet.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTNet.UI.Forms
{
    /// <summary>
    /// Delegate when for a strategy is created
    /// </summary>
    /// <param name="a"></param>
    delegate void StrategyCreatedHandler(ITradingStrategy a);

    /// <summary>
    /// Form enabling the user to run trading strategies
    /// </summary>
    partial class BackTestingForm : Form
    {
        /// <summary>
        /// Event triggered when a strategy is created
        /// </summary>
        public event StrategyCreatedHandler OnStrategyCreated;

        #region Private Members
        /// <summary>
        /// Current settings control
        /// </summary>
        private Control _settingsCtrl;
        /// <summary>
        /// Selected trading strategy
        /// </summary>
        private ITradingStrategy _tradingStrategy;
        /// <summary>
        /// Currently loaded candles
        /// </summary>
        private IList<OHLC> _candles;
        /// <summary>
        /// The indicators
        /// </summary>
        private IndicatorsForm _indicatorsForm;
        /// <summary>
        /// Whether we are in test mode (always true for now)
        /// </summary>
        private bool _isTestMode;
        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public BackTestingForm(bool isTestMode = true)
        {
            InitializeComponent();

            _isTestMode = isTestMode;
            pnlPlaceholder.Controls.Add(_settingsCtrl);
            PopulateStrategyDropdown();
            cbbStrategy.SelectedIndex = 0;
            chartSelector.LoadFromCSVClicked += chartSelector_LoadFromCSVClicked;
            chartSelector.RefreshClicked += chartSelector_RefreshClicked;

            _indicatorsForm = new IndicatorsForm();

            _indicatorsForm.IndicatorOptionsChanged += (s, options)
                =>
            {
                chartCtrl.NotifyIndicatorsChanged(options);
            };
            chartCtrl.NotifyIndicatorsChanged(_indicatorsForm.IndicatorOptions);
            chartSelector.OnPairChanged += (s, e) =>
                {
                    UpdateLabels(chartSelector.SelectedPair);
                };
            this.FormClosed += (s, e) => _indicatorsForm.Dispose();
        }

        public ITradingStrategy TradingStrategy
        {
            get { return _tradingStrategy; }
        }


        /// <summary>
        /// Gets a value indicating whether the form is on design mode
        /// </summary>
        public bool IsDesignMode
        {
            get { return DesignMode; }
        }

        /// <summary>
        /// Populates the strategy dropdown
        /// </summary>
        private void PopulateStrategyDropdown()
        {
            List<StrategyCbbItem> list = new List<StrategyCbbItem>();
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var d = new Dictionary<Type, Type>();
            foreach (var t in types)
            {
                if (typeof(Interfaces.IStrategySettingsControl).IsAssignableFrom(t))
                {
                    var attr = t.GetCustomAttribute<StrategySettingsProvider>();
                    if (attr != null)
                    {
                        d[attr.StrategyType] = t;
                    }
                }
            }

            foreach (var t in types)
            {
                if (typeof(ITradingStrategy).IsAssignableFrom(t))
                {
                    var attr = t.GetCustomAttribute<StrategyAttribute>();
                    if (attr != null)
                    {
                        var item = new StrategyCbbItem
                        {
                            Attribute = attr,
                            StrategyType = t,
                            SettingsProviderType = d.ContainsKey(t) ? d[t] : null,

                        };
                        list.Add(item);
                    }
                }
            }
            cbbStrategy.PopulateCbbFromList(list, i => i.Attribute.Name, list[0]);
        }
        /// <summary>
        /// Loads data when the user clicks on the refresh button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartSelector_RefreshClicked(object sender, EventArgs e)
        {
            LoadCandles(chartSelector.FromDate, chartSelector.SelectedPeriodInMin, chartSelector.ToDate);
        }

        /// <summary>
        /// Loads candles after clicking on Refresh button
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="isRealtime"></param>
        /// <param name="candlesDurationInMinutes"></param>
        /// <param name="maxDate"></param>
        private void LoadCandles(DateTime minDate, int candlesDurationInMinutes, DateTime? maxDate)
        {
            var currencyPair = chartSelector.SelectedPair;
            Task.Factory.StartNew(() => CandlesProvider.LoadCandles(currencyPair, minDate, candlesDurationInMinutes, maxDate))
                .ContinueWith(taskResult =>
                {
                    if (taskResult.Result != null)
                    {
                        _candles = taskResult.Result;
                        chartCtrl.DrawChart(taskResult.Result, Constants.PriceSerieName);
                    }
                }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Loads candles from a CSV file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartSelector_LoadFromCSVClicked(object sender, EventArgs e)
        {
            using (OpenFileDialog opfd = new OpenFileDialog())
            {
                opfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                opfd.FilterIndex = 1;
                opfd.RestoreDirectory = true;
                opfd.InitialDirectory = Application.ExecutablePath;

                if (opfd.ShowDialog() == DialogResult.OK)
                {
                    LoadChartFromCSVCandlesFile(opfd.FileName);
                }
            }
        }

        /// <summary>
        /// Loads chart data from a csv file
        /// </summary>
        /// <param name="fileName"></param>
        private void LoadChartFromCSVCandlesFile(string fileName)
        {
            _candles = CandlesProvider.ReadCandlesFromCSV(fileName, chartSelector.FromDate, chartSelector.ToDate);
            chartCtrl.DrawChart(_candles, Constants.PriceSerieName);
        }

        private void cbbStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlPlaceholder.Controls.Clear();
            var item = cbbStrategy.GetSelectedValue<StrategyCbbItem>();
            _settingsCtrl = (Control)Activator.CreateInstance(item.SettingsProviderType);
            pnlPlaceholder.Controls.Add(_settingsCtrl);
        }

        private StrategySettings GetSettings()
        {
            var item = cbbStrategy.GetSelectedValue<StrategyCbbItem>();
            return (StrategySettings)Activator.CreateInstance(item.Attribute.SettingsType);
        }

        private void BackTestingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                CreateTradingStrategy();
            }
            else
            {
                _tradingStrategy = null;
            }
        }

        private void CreateTradingStrategy()
        {
            var strategySettings = GetSettings();
            strategySettings.MinAmountItem1ToKeep = numMinAmountBTC.Value;
            strategySettings.MinAmountItem2ToKeep = numMinAmountMoney.Value;
            strategySettings.MaxAmountMoneyPerBuyOrder = numMaxAmountMoneyPerBuyOrder.Value;
            strategySettings.MaxNbBTCPerSellOrder = numMaxNbBTCPerSellOrder.Value;
            //Back testing
            strategySettings.InitialItem1Balance = numBalBTCTest.Value;
            strategySettings.InitialItem2Balance = numBalUSDTest.Value;
            strategySettings.Pair = chartSelector.SelectedPair;

            //strategy specific settings
            (_settingsCtrl as Interfaces.IStrategySettingsControl).FillSettings(strategySettings);

            var item = cbbStrategy.GetSelectedValue<StrategyCbbItem>();
            _tradingStrategy = (ITradingStrategy)Activator.CreateInstance(item.StrategyType, strategySettings);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            CreateTradingStrategy();
            if (this.OnStrategyCreated != null)
            {
                OnStrategyCreated(_tradingStrategy);
            }
            if (_isTestMode)
            {
                RunBackTests();
            }
        }

        /// <summary>
        /// Shows the indicators form
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

        /// <summary>
        /// Starts backtesting
        /// </summary>
        private void RunBackTests()
        {
            if (_candles == null || _candles.Count == 0)
            {
                ErrorHelper.DisplayErrorMessage("Not enough data to run the simulation");
                return;
            }
            int i = 0;
            var strategy = TradingStrategy;

            foreach (var candle in _candles)
            {
                var temp = _candles.Take(i++);
                strategy.OnTick(temp.ToList());
            }

            var pair = chartSelector.SelectedPair;
            var res = strategy.GetResult();
            var nbUnitsItem2 = res.BalanceItem1 * _candles.Last().Close;
            lblResult.Text = string.Format("{0:0.00} {3} - {1:0.00} {4} - Total: {2:0.00} {5}", res.BalanceItem2, res.BalanceItem1,
                res.BalanceItem2 + nbUnitsItem2, pair.Item2, pair.Item1, pair.Item2);

            var actions = strategy.Actions;
            var simRes = new SimulationResult
            {
                Actions = actions
            };
            chartCtrl.NotifySimulationResult(simRes);
        }

        /// <summary>
        /// Updates the labels depending on the selected currency pair
        /// </summary>
        /// <param name="pair"></param>
        private void UpdateLabels(CurrencyPair pair)
        {
            lblMinAmountItem1ToKeep.Text = string.Format("Min amount {0} to keep", pair.Item1);
            lblMinAmountItem2ToKeep.Text = string.Format("Min amount {0} to keep", pair.Item2);
            lblMaxAmountItem1PerSell.Text = string.Format("Max Nb {0}/ sell order", pair.Item1);
            lblMaxAmountItem2PerBuy.Text = string.Format("Max Nb {0}/ buy order", pair.Item2);
            lblInitialItem1Balance.Text = string.Format("Initial {0} balance (Back test)", pair.Item1);
            lblInitialItem2Balance.Text = string.Format("Initial {0} balance (Back test)", pair.Item2);
        }

        /// <summary>
        /// Item displayed in the strategy dropdown
        /// </summary>
        class StrategyCbbItem
        {
            public StrategyAttribute Attribute { get; set; }
            public Type SettingsProviderType { get; set; }
            public Type StrategyType { get; set; }
        }
    }

}
