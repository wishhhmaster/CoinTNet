using CoinTNet.Common.Constants;
using CoinTNet.DAL;
using CoinTNet.DO.Security;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls.Options
{
    /// <summary>
    /// Enables the user to enter the authentication info for Twitter's API
    /// </summary>
    public partial class TwitterKeysControl : UserControl, Interfaces.IOptionControl
    {
        /// <summary>
        /// The previous key values
        /// </summary>
        private TwitterAPIParams _apiParams;

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public TwitterKeysControl()
        {
            InitializeComponent();

            _apiParams = SecureStorage.GetEncryptedData<TwitterAPIParams>(SecuredDataKeys.TwitterAPI);
            txtKey.Text = _apiParams.ConsumerKey;
            txtSecret.Text = _apiParams.SecretKey;
        }

        /// <summary>
        /// Saves the new API parameters
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (txtSecret.Text != _apiParams.SecretKey || txtKey.Text != _apiParams.ConsumerKey)
            {
                var p = new TwitterAPIParams
                {
                    ConsumerKey = txtKey.Text,
                    SecretKey = txtSecret.Text,
                };
                SecureStorage.SaveEncryptedData(p, SecuredDataKeys.TwitterAPI);
                TwitterProxyFactory.NotifySettingsChanged();
            }
            return true;

        }
    }
}
