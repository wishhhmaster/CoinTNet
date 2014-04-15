using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTNet.DO;
using CoinTNet.UI.Common;
using CoinTNet.BLL;

namespace CoinTNet.UI.Controls.StrategyParameters
{
    [StrategySettingsProvider(typeof(EMATradingStrategy))]
    partial class EMAStrategyControl : UserControl, Interfaces.IStrategySettingsControl
    {
        public EMAStrategyControl()
        {
            InitializeComponent();
        }
        public void FillSettings(StrategySettings settings)
        {
            var emaSettings = (EMAStrategySettings)settings;

            emaSettings.BuyThreshold = (double)numBuyThreshold.Value;
            emaSettings.SellThreshold = -(double)numSellThreshold.Value;
            emaSettings.FastEMAPeriod = (int)numFastMAPeriod.Value;
            emaSettings.SlowEMAPeriod = (int)numSlowMAPeriod.Value;


        }
    }
}
