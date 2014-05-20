using CoinTNet.Common;
using CoinTNet.Common.Constants;
using CoinTNet.DO.Exchanges;
using System;
using System.Windows.Forms;

namespace CoinTNet.UI.Common
{
    /// <summary>
    /// Helper for error display
    /// </summary>
    class ErrorHelper
    {
        /// <summary>
        /// Displays an error message
        /// </summary>
        /// <param name="errMsg"></param>
        public static void DisplayErrorMessage(string errMsg)
        {
            MessageBox.Show(errMsg, ApplicationConstants.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays an error message for a callresult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callRes"></param>
        public static void DisplayErrorMessage<T>(CallResult<T> callRes)
        {
            string errMsg = callRes.ErrorCode == ErrorCodes.InvalidAPIKeys ?
                "Please enter valid API Keys" : callRes.ErrorMessage;
            MessageBox.Show(errMsg, ApplicationConstants.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays a warning message
        /// </summary>
        /// <param name="msg">The message to display</param>
        public static void DisplayWarningMessage(string msg)
        {
            MessageBox.Show(msg, ApplicationConstants.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Logs an error and display a message informing the user that the application is going to crash
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleApplicationError(Exception ex)
        {
            Logger.Log(ex, "Application crash");
            DisplayErrorMessage("The application is about to crash. Error:\n" + ex.ToString());
            Environment.Exit(0);
        }
    }
}
