using CoinTNet.Common.Constants;
using CoinTNet.DAL;
using CoinTNet.DO.Security;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls.Options
{
    /// <summary>
    /// Enables the user to enter the authentication info for Cryptsy's API
    /// </summary>
    public partial class CryptsyKeysControl : UserControl, Interfaces.IOptionControl
    {
        /// <summary>
        /// The previous key values
        /// </summary>
        private CryptsyAPIParams _apiParams;

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public CryptsyKeysControl()
        {
            InitializeComponent();

            _apiParams = SecureStorage.GetEncryptedData<CryptsyAPIParams>(SecuredDataKeys.CryptsyAPI);
            txtPublicKey.Text = _apiParams.PublicKey;
            txtSecretKey.Text = _apiParams.SecretKey;
        }

        /// <summary>
        /// Saves the new keys
        /// </summary>
        /// <returns>True if the data was saved correctly</returns>
        public bool Save()
        {
            if (txtSecretKey.Text != _apiParams.SecretKey || txtPublicKey.Text != _apiParams.PublicKey)
            {
                var p = new CryptsyAPIParams
                {
                    PublicKey = txtPublicKey.Text,
                    SecretKey = txtSecretKey.Text,
                };
                SecureStorage.SaveEncryptedData(p, SecuredDataKeys.CryptsyAPI);
                ExchangeProxyFactory.NotifySettingsChanged(ExchangesInternalCodes.Cryptsy);
                EventAggregator.Instance.Publish(new SecuredDataChanged { DataKey = ExchangesInternalCodes.Cryptsy });

            }
            return true;

        }
    }
}
