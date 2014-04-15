using System;
using System.IO;

namespace CoinTNet.Common
{
    /// <summary>
    /// Logger for the application
    /// </summary>
    class Logger
    {
        /// <summary>
        /// Logs the error message in the console output and in a log file
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        public static void Log(Exception ex, string msg = "")
        {
            try
            {
                string text = string.Format("[{0}] The following exception occurred\n{1}{2}",
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), msg, ex.ToString());
                System.Diagnostics.Debug.WriteLine(text);
                File.AppendAllText("error.log", text + Environment.NewLine);
            }
            catch (Exception ex2)
            {
                System.Diagnostics.Debug.WriteLine("Cannot write error: " + ex2.ToString());
            }

        }
    }
}
