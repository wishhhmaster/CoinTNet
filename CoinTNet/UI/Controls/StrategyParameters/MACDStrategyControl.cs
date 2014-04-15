using CoinTNet.BLL;
using CoinTNet.DO;
using CoinTNet.UI.Common;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls.StrategyParameters
{
    /// <summary>
    /// Provides settings for the MACD Strategy
    /// </summary>
    [StrategySettingsProvider(typeof(MACDTradingStrategy))]
    partial class MACDStrategyControl : UserControl, Interfaces.IStrategySettingsControl
    {
        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public MACDStrategyControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Saves settings into the object
        /// </summary>
        /// <param name="settings"></param>
        public void FillSettings(StrategySettings settings)
        {
            var macdSettings = (MACDStrategySettings)settings;

            macdSettings.BuyThreshold = (double)numBuyThreshold.Value;
            macdSettings.SellThreshold = -(double)numSellThreshold.Value;
            macdSettings.FastEMAPeriod = (int)numFastMAPeriod.Value;
            macdSettings.SlowEMAPeriod = (int)numSlowMAPeriod.Value;
            macdSettings.SignalPeriod = (int)numSignal.Value;
        }
    }
}
