using CoinTNet.DAL;
using CoinTNet.DAL.Interfaces;
using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using System.Globalization;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls
{
    /// <summary>
    /// Controls displaying a list of open orders
    /// </summary>
    public partial class OpenOrdersControl : UserControl
    {
        #region Private members
        /// <summary>
        /// The proxy
        /// </summary>
        private IExchange _proxy;

        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public OpenOrdersControl()
        {
            InitializeComponent();
            btnRefresh.Click += (s, e) => RefreshOrders();
            EventAggregator.Instance.Subscribe<OrderSentMessage>(m => RefreshOrders());
            EventAggregator.Instance.Subscribe<ExchangeSelected>(m => OnExchangeSelected(m));
            EventAggregator.Instance.Subscribe<PairSelected>(m => OnPairSelected(m));
        }

        /// <summary>
        /// Gets the currently selected pair
        /// </summary>
        private CurrencyPair CurrentPair
        {
            get { return (this.ParentForm as CoinTNet.UI.Forms.MainForm).SelectedPair; }
        }

        /// <summary>
        /// Called when the main screen's exchange changes
        /// </summary>
        /// <param name="message"></param>
        private void OnExchangeSelected(ExchangeSelected message)
        {
            if (message.SelectorType == SelectorType.Main)
            {
                _proxy = ExchangeProxyFactory.GetProxy(message.InternalCode);
            }
        }
        /// <summary>
        /// Called when the main screen's pair changes
        /// </summary>
        /// <param name="message"></param>
        private void OnPairSelected(PairSelected message)
        {
            if (message.SelectorType == SelectorType.Main)
            {
                dgvOpenOrders.Rows.Clear();
                colAmount.HeaderText = message.Pair.Item1 + " Amount";
                colPrice.HeaderText = "Price in " + message.Pair.Item2;
                colTotalPrice.HeaderText = "Total Price in " + message.Pair.Item2;
            }
        }

        /// <summary>
        /// Updates the list of open orders
        /// </summary>
        private void RefreshOrders()
        {
            var openOrdersRes = _proxy.GetOpenOrders(CurrentPair);
            dgvOpenOrders.SuspendLayout();
            dgvOpenOrders.Rows.Clear();
            if (openOrdersRes.Success)
            {
                int i = 0;
                foreach (var o in openOrdersRes.Result.List)
                {
                    dgvOpenOrders.Rows.Add();
                    dgvOpenOrders.Rows[i].Tag = o;
                    var cells = dgvOpenOrders.Rows[i].Cells;
                    cells[colType.Index].Value = o.Type == OrderType.Sell ? "Sell" : "Buy";
                    cells[colDate.Index].Value = o.DateTime.ToString();
                    cells[colAmount.Index].Value = o.Amount.ToString("0.00###", CultureInfo.InvariantCulture);
                    cells[colPrice.Index].Value = o.Price.ToString("0.00###", CultureInfo.InvariantCulture);
                    cells[colTotalPrice.Index].Value = (o.Price * o.Amount).ToString("0.00###", CultureInfo.InvariantCulture);
                    cells[colIfExecuted.Index].Value = o.LimitPrice > 0 ? o.LimitPrice.ToString("0.00###", CultureInfo.InvariantCulture) : string.Empty;
                    (cells[colCancel.Index] as DataGridViewLinkCell).Value = "Cancel";
                    i++;
                }
            }
            else
            {
                ErrorHelper.DisplayErrorMessage(openOrdersRes.ErrorMessage);
            }
            dgvOpenOrders.ResumeLayout();
        }

        /// <summary>
        /// Cancels an order when the user clicks on the Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOpenOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colCancel.Index)
            {
                long orderId = (dgvOpenOrders.Rows[e.RowIndex].Tag as OrderDetails).Id;
                var callRes = _proxy.CancelOrder(orderId);
                if (callRes.Success)
                {
                    dgvOpenOrders.Rows.RemoveAt(e.RowIndex);
                    EventAggregator.Instance.Publish(new OrderCancelledMessage());
                }
                else
                {
                    ErrorHelper.DisplayErrorMessage(callRes.ErrorMessage);
                }
            }
        }
    }
}
