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
using CoinTNet.BLL;
using CoinTNet.UI.Common;

namespace CoinTNet.UI.Controls.StrategyParameters
{
    [StrategySettingsProvider(typeof(SimpleTradingStrategy))]
    partial class SimpleStrategyControl : UserControl, Interfaces.IStrategySettingsControl
    {
        public SimpleStrategyControl()
        {
            InitializeComponent();
        }
        public void FillSettings(StrategySettings settings)
        {
            var udSettings = (SimpleStrategySettings)settings;


            udSettings.BuyThreshold = (double)numBuyThreshold.Value;
            udSettings.SellThreshold = -(double)numSellThreshold.Value;


        }
    }
}
