using CoinTNet.UI.Common;
using System;
using System.Windows.Forms;

namespace CoinTNet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Form1_UIThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CoinTNet.UI.Forms.MainForm());
            }
            catch (Exception e)
            {
                ErrorHelper.HandleApplicationError(e);
            }
        }

        /// <summary>
        /// Handles UI errors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="t"></param>
        private static void Form1_UIThreadException(object sender, System.Threading.ThreadExceptionEventArgs t)
        {
            ErrorHelper.HandleApplicationError(t.Exception);
        }


        /// <summary>
        /// Handle the UI exceptions by showing a dialog box
        /// NOTE: This exception cannot be kept from terminating the application - it can only 
        /// log the event, and inform the user about it. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorHelper.HandleApplicationError((e.ExceptionObject as Exception));
        }
    }
}
