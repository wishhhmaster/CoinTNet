using CoinTNet.Common;
using CoinTNet.DAL;
using CoinTNet.DAL.Interfaces;
using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;


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
        /// <summary>
        /// Gets the currently selected pair
        /// </summary>
        private CurrencyPair _selectedPair;
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
            EventAggregator.Instance.Subscribe<SecuredDataChanged>(m => OnSecuredDataChanged(m));
            lblTotalBuyOrders.Text = lblTotalSellOrders.Text = string.Empty;
        }

        /// <summary>
        /// Reinitialises the proxy if the user changes the API authentication settings for this proxy
        /// </summary>
        /// <param name="m"></param>
        private void OnSecuredDataChanged(SecuredDataChanged m)
        {
            if (m.DataKey == _selectedPair.Exchange.InternalCode)
            {
                _proxy = ExchangeProxyFactory.GetProxy(m.DataKey);
            }
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
                _selectedPair = message.Pair;
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
            var openOrdersRes = _proxy.GetOpenOrders(_selectedPair);
            dgvOpenOrders.SuspendLayout();
            dgvOpenOrders.Rows.Clear();
            lblTotalBuyOrders.Text = lblTotalSellOrders.Text = string.Empty;

            if (openOrdersRes.Success)
            {
                int i = 0;
                dgvOpenOrders.Columns[colDate.Index].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                var orders = openOrdersRes.Result.List;
                decimal totalBuyPrice = 0m, totalBuyAmount = 0m, totalSellPrice = 0m, totalSellAmount = 0m;
                foreach (var order in orders)
                {

                    dgvOpenOrders.Rows.Add();
                    dgvOpenOrders.Rows[i].Tag = order;
                    var cells = dgvOpenOrders.Rows[i].Cells;
                    cells[colType.Index].Value = order.Type == OrderType.Sell ? "Sell" : "Buy";
                    cells[colDate.Index].Value = order.DateTime;
                    cells[colAmount.Index].Value = order.Amount.ToStandardFormat();
                    cells[colPrice.Index].Value = order.Price.ToStandardFormat();
                    cells[colTotalPrice.Index].Value = (order.Price * order.Amount).ToStandardFormat();
                    cells[colIfExecuted.Index].Value = order.LimitPrice > 0 ? order.LimitPrice.ToStandardFormat() : string.Empty;
                    (cells[colCancel.Index] as DataGridViewLinkCell).Value = "Cancel";
                    i++;
                    if (order.Type == OrderType.Sell)
                    {
                        totalSellAmount += order.Amount;
                        totalSellPrice += order.Price;
                    }
                    else
                    {
                        totalBuyAmount += order.Amount;
                        totalBuyPrice += order.Price;
                    }

                }
                dgvOpenOrders.Sort(colDate, System.ComponentModel.ListSortDirection.Descending);
                lblTotalBuyOrders.Text = string.Format("{0} {1} / {2} {3}", totalBuyAmount.ToStandardFormat(), _selectedPair.Item1, totalBuyPrice.ToStandardFormat(), _selectedPair.Item2);
                lblTotalSellOrders.Text = string.Format("{0} {1} / {2} {3}", totalSellAmount.ToStandardFormat(), _selectedPair.Item1, totalSellPrice.ToStandardFormat(), _selectedPair.Item2);
            }
            else
            {
                ErrorHelper.DisplayErrorMessage(openOrdersRes);
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
                    ErrorHelper.DisplayErrorMessage(callRes);
                }
            }
        }
    }
}
