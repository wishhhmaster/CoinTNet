using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace CoinTNet.UI.Common
{
    class Support
    {
        //class 
    }

    class IndicatorOptions
    {
        public MSChartAverageItem SlowMSAverage { get; set; }
        public MSChartAverageItem FastMSAverage { get; set; }
        public int SlowMSPeriod { get; set; }
        public int FastMSPeriod { get; set; }
    }

    class TAAverageItem
    {
        public string SerieName { get; set; }
        public string Display { get; set; }
        public TAAverageItem(string display, string serieName)
        {
            Display = display;
            SerieName = serieName;
        }
    }

    class MSChartAverageItem
    {
        public FinancialFormula Formula { get; set; }
        public string SerieName { get; set; }
        public string Display { get; set; }
        public MSChartAverageItem(string display, string serieName, FinancialFormula formula)
        {
            Formula = formula;
            Display = display;
            SerieName = serieName;
        }
    }

    enum SelectorType
    {
        Main,
        BackTest
    }

    [AttributeUsage(AttributeTargets.Class)]
    class StrategySettingsProvider : Attribute
    {
        public StrategySettingsProvider(Type t)
        {
            StrategyType = t;
        }
        public Type StrategyType { get; set; }
    }
}
