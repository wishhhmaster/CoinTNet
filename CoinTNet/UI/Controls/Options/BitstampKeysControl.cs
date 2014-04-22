using CoinTNet.Common.Constants;
using CoinTNet.DAL;
using CoinTNet.DO.Security;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls.Options
{
    /// <summary>
    /// Enables the user to enter the authentication info for Bitstamp's API
    /// </summary>
    public partial class BitstampKeysControl : UserControl, Interfaces.IOptionControl
    {
        /// <summary>
        /// The previous key values
        /// </summary>
        private BitstampAPIParams _apiParams;
        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public BitstampKeysControl()
        {
            InitializeComponent();
            _apiParams = SecureStorage.GetEncryptedData<BitstampAPIParams>(SecuredDataKeys.BitstampAPI);
            txtClientID.Text = _apiParams.ClientID;
            txtKey.Text = _apiParams.APIKey;
            txtSecret.Text = _apiParams.APISecret;
        }

        /// <summary>
        /// Saves the new keys
        /// </summary>
        /// <returns>True if the data was saved correctly</returns>
        public bool Save()
        {
            if (txtSecret.Text != _apiParams.APISecret || txtKey.Text != _apiParams.APIKey || txtClientID.Text != _apiParams.ClientID)
            {
                BitstampAPIParams p = new BitstampAPIParams
                {
                    APIKey = txtKey.Text,
                    APISecret = txtSecret.Text,
                    ClientID = txtClientID.Text
                };
                SecureStorage.SaveEncryptedData(p, SecuredDataKeys.BitstampAPI);
                ExchangeProxyFactory.NotifySettingsChanged(ExchangesInternalCodes.Bitstamp);
                EventAggregator.Instance.Publish(new SecuredDataChanged { DataKey = ExchangesInternalCodes.Bitstamp });
            }
            return true;

        }
    }
}
