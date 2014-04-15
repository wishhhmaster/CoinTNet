using System;
using System.Drawing;
using System.Windows.Forms;
using CoinTNet.DO;
using CoinTNet.Common.Constants;
using CoinTNet.UI.Common.EventAggregator;
using CoinTNet.UI.Common;

namespace CoinTNet.UI.Forms
{
    /// <summary>
    /// Shows a notification pop up, by default in the bottom right corner of the screen
    /// </summary>
    partial class NotificationPopUp<T> : Form
    {
        private T _item;

        /// <summary>
        /// Initialises a new instance of the NotificationPopUp class with the specified parameters
        /// </summary>
        /// <param name="notificationMessage">The message to display</param>
        /// <param name="messageType">The type of message</param>
        /// <param name="interval">How long the pop up should remain visible, in ms</param>
        public NotificationPopUp(string notificationMessage, string title, string link, MessageType messageType, int interval, T item)
        {
            InitializeComponent();
            _item = item;
            lblText.Text = notificationMessage;
            lblText.Tag = link;
            this.Visible = false;
            //tmShow.Interval = interval*100;
            lblApplicationName.Text = string.IsNullOrEmpty(title)?  ApplicationConstants.ApplicationName : title;

            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Width - 10,
                Screen.PrimaryScreen.WorkingArea.Bottom - this.Height - 20);

            if (messageType == MessageType.Error)
            {
                this.lblText.ForeColor = Color.Red;
                pcbIcon.Image = global::CoinTNet.Properties.Resources.cross;
            }
            else if (messageType == MessageType.Warning)
            {
                lblText.ForeColor = Color.Orange;
                pcbIcon.Image = global::CoinTNet.Properties.Resources.error;
            }
            else if (messageType == MessageType.Confirmation)
            {
                lblText.ForeColor = Color.Green;
                pcbIcon.Image = global::CoinTNet.Properties.Resources.information;
            }
            else if (messageType == MessageType.Information)
            {
                lblText.ForeColor = Color.Black;
                pcbIcon.Image = global::CoinTNet.Properties.Resources.information;
            }
            this.Shown += (s, e) => tmShow.Start();
        }

        /// <summary>
        /// Closes the pop up when the user clicks on it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUserClick(object sender, EventArgs e)
        {
            string link = (string)lblText.Tag;
            if(!string.IsNullOrEmpty(link))
            {
                System.Diagnostics.Process.Start(link);
            }
            this.Close();
            EventAggregator.Instance.Publish(new NotificationReadMessage<T> { Object = _item  });
            
        }

        /// <summary>
        /// WHen the timer ticks, closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmShow_Tick(object sender, EventArgs e)
        {
            tmShow.Stop();
            tmShow.Dispose();
            this.Close();
        }
    }
}
