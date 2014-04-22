using CoinTNet.Common.Constants;
using CoinTNet.DAL;
using CoinTNet.DO.Security;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls.Options
{
    /// <summary>
    /// Enables the user to enter the authentication info for Btc-e's API
    /// </summary>
    public partial class BtceKeysControl : UserControl, Interfaces.IOptionControl
    {
        /// <summary>
        /// The previous key values
        /// </summary>
        private BtceAPIParams _apiParams;

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public BtceKeysControl()
        {
            InitializeComponent();

            _apiParams = SecureStorage.GetEncryptedData<BtceAPIParams>(SecuredDataKeys.BtceAPI);
            txtKey.Text = _apiParams.APIKey;
            txtSecret.Text = _apiParams.APISecret;
        }

        /// <summary>
        /// Saves the new keys
        /// </summary>
        /// <returns>True if the data was saved correctly</returns>
        public bool Save()
        {
            if (txtSecret.Text != _apiParams.APISecret || txtKey.Text != _apiParams.APIKey)
            {
                var p = new BtceAPIParams
                {
                    APIKey = txtKey.Text,
                    APISecret = txtSecret.Text,
                };
                SecureStorage.SaveEncryptedData(p, SecuredDataKeys.BtceAPI);
                ExchangeProxyFactory.NotifySettingsChanged(ExchangesInternalCodes.Btce);
                EventAggregator.Instance.Publish(new SecuredDataChanged { DataKey = ExchangesInternalCodes.Btce });

            }
            return true;

        }
    }
}
