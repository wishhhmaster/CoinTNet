using CoinTNet.DAL;
using CoinTNet.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CoinTNet.UI.Forms
{
    /// <summary>
    /// Form allowing the user to login with a specific profile or create a new one
    /// This form is displayed when the application starts
    /// </summary>
    public partial class LoginForm : Form
    {

        #region Private members
        /// <summary>
        /// The  text of the new profile cbb item
        /// </summary>
        private const string NewProfileItem = "<New Profile>";
        /// <summary>
        /// Whether the new profile item is selected
        /// </summary>
        private bool _newProfileSelected;
        /// <summary>
        /// The list of existing profiles
        /// </summary>
        private IList<string> _existingProfiles;
        /// <summary>
        /// The authentication service
        /// </summary>
        private AuthenticationService _authService;

        #endregion

        /// <summary>
        /// Initialises a new instance of the form
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
            InitialiseProfiles();
            StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Initialises the list of profiles
        /// </summary>
        private void InitialiseProfiles()
        {
            _existingProfiles = _authService.GetProfiles();
            foreach (var i in _existingProfiles.Union(new[] { NewProfileItem }))
            {
                cbbProfiles.Items.Add(i);
            }
            cbbProfiles.SelectedIndex = 0;
        }

        /// <summary>
        /// Changes the ddl style depending on the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            _newProfileSelected = cbbProfiles.Text == NewProfileItem;
            cbbProfiles.DropDownStyle = _newProfileSelected ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbProfiles_TextChanged(object sender, EventArgs e)
        {
            string p = cbbProfiles.Text;
            _newProfileSelected = !_existingProfiles.Contains(p);
            btnLogin.Text = _newProfileSelected ? "Create" : "Login";
            btnLogin.Enabled = p != NewProfileItem && txtPassword.Text.Length > 0;
        }

        /// <summary>
        /// Creates a profile or logs the user with a profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string profileName = cbbProfiles.Text;
                if (_newProfileSelected)
                {
                    _authService.CreateProfile(profileName, txtPassword.Text);
                }
                else
                {
                    bool ok = _authService.Login(profileName, txtPassword.Text);
                    if (!ok)
                    {
                        ErrorHelper.DisplayWarningMessage("Invalid credentials");
                        e.Cancel = true;
                    }

                }
            }
        }

        /// <summary>
        /// Enables the login button or not depending on whether a password has been entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = cbbProfiles.Text != NewProfileItem && txtPassword.Text.Length > 0;
        }
    }
}
