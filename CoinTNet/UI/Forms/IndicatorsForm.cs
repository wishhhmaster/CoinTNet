using CoinTNet.UI.Common;
using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CoinTNet.UI.Forms
{
    /// <summary>
    /// Form enabling the user to choose Indicators
    /// </summary>
    partial class IndicatorsForm : Form
    {
        /// <summary>
        /// Triggered when the indicators change
        /// </summary>
        public event EventHandler<IndicatorOptions> IndicatorOptionsChanged;

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public IndicatorsForm()
        {
            InitializeComponent();
            InitControls();
        }

        /// <summary>
        /// Initialises controls' values
        /// </summary>
        private void InitControls()
        {
            MSChartAverageItem[] msSlowValues = { new MSChartAverageItem("None", string.Empty,0), new MSChartAverageItem("Simple", "SimpleH",FinancialFormula.MovingAverage), new MSChartAverageItem("Exponential", "ExponentialH", FinancialFormula.ExponentialMovingAverage)
                                                   , new MSChartAverageItem("Triangular", "TriangularH",FinancialFormula.TriangularMovingAverage), new MSChartAverageItem("Weighted", "WeightedH",FinancialFormula.WeightedMovingAverage)};

            MSChartAverageItem[] msFastValues = { new MSChartAverageItem("None", string.Empty,0), new MSChartAverageItem("Simple", "SimpleFastH",FinancialFormula.MovingAverage), new MSChartAverageItem("Exponential", "ExponentialFastH", FinancialFormula.ExponentialMovingAverage)
                                                   , new MSChartAverageItem("Triangular", "TriangularFastH",FinancialFormula.TriangularMovingAverage), new MSChartAverageItem("Weighted", "WeightedFastH",FinancialFormula.WeightedMovingAverage)};

            cbbMSSlowAvgType.PopulateCbbFromList(msSlowValues, t => t.Display, msSlowValues[2]);
            cbbMSFastAvgType.PopulateCbbFromList(msFastValues, t => t.Display, msFastValues[2]);


            numSlowMAPeriod.Value = 21m;
            numFastMAPeriod.Value = 10m;
        }

        public IndicatorOptions IndicatorOptions
        {
            get
            {
                return new IndicatorOptions
                {
                    FastMSAverage = cbbMSFastAvgType.GetSelectedValue<MSChartAverageItem>(),
                    SlowMSAverage = cbbMSSlowAvgType.GetSelectedValue<MSChartAverageItem>(),
                    SlowMSPeriod = (int)numSlowMAPeriod.Value,
                    FastMSPeriod = (int)numFastMAPeriod.Value,
                };
            }
        }

        private void OnMATypeChanged(object sender, EventArgs e)
        {
            OnIndicatorOptionsChanged();
        }

        private void OnMAPeriodChanged(object sender, EventArgs e)
        {
            OnIndicatorOptionsChanged();
        }

        /// <summary>
        /// Triggers the IndicatorOptionsChanged event
        /// </summary>
        private void OnIndicatorOptionsChanged()
        {
            if (IndicatorOptionsChanged != null)
            {
                IndicatorOptionsChanged(this, IndicatorOptions);
            }
        }

        private void IndicatorsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }


    }
}
