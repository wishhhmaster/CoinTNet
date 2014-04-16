using CoinTNet.Common;
using CoinTNet.DAL;
using CoinTNet.DAL.Interfaces;
using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using CoinTNet.UI.Forms;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace CoinTNet.UI.Controls
{
    /// <summary>
    /// Control that lets the user add new orders
    /// </summary>
    public partial class OrderControl : UserControl
    {
        #region private Members
        /// <summary>
        /// The proxy
        /// </summary>
        private IExchange _proxy;
        /// <summary>
        /// The fee applying to transactions
        /// </summary>
        private decimal _fee;

        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public OrderControl()
        {
            InitializeComponent();
            EventAggregator.Instance.Subscribe<OrderCancelledMessage>(m => UpdateBalance());
            EventAggregator.Instance.Subscribe<TickerUpdateMessage>(m => OnTickerUpdate(m.Ticker));
            EventAggregator.Instance.Subscribe<ExchangeSelected>(m => OnExchangeSelected(m));
            EventAggregator.Instance.Subscribe<PairSelected>(m => OnPairChanged(m));
        }

        /// <summary>
        /// Gets the currently selected currency pair
        /// </summary>
        private CurrencyPair SelectedPair
        {
            get { return (this.ParentForm as MainForm).SelectedPair; }
        }

        /// <summary>
        /// Called when the selected exchange changes
        /// </summary>
        /// <param name="market"></param>
        private void OnExchangeSelected(ExchangeSelected message)
        {
            if (message.SelectorType == SelectorType.Main)
            {
                _proxy = ExchangeProxyFactory.GetProxy(message.InternalCode);
            }
        }

        /// <summary>
        /// Clear fields and gets the fee for the selected pair
        /// </summary>
        private void OnPairChanged(PairSelected m)
        {
            if (m.SelectorType != SelectorType.Main)
            {
                return;
            }
            try
            {
                lblItemBalance.Text = lblCurrencyBalance.Text = string.Empty;
                txtBuyAmount.Text = txtBuyPrice.Text = txtSellAmount.Text = txtSellPrice.Text = string.Empty;
                var feeRes = _proxy.GetFee(m.Pair);
                lblCurrency1.Text = lblCurrency2.Text = m.Pair.Item2;
                lblSellAmountCurrency.Text = lblBuyAmountCurrency.Text = m.Pair.Item1;

                _fee = feeRes.Success ? feeRes.Result : 0.03m;
                lblBuyFee.Text = lblSellFee.Text = "0";
                lblBuyTotal.Text = lblSellTotal.Text = "0";
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                ErrorHelper.DisplayErrorMessage(ex.ToString());
            }
        }
        /// <summary>
        /// Updates the user's balance
        /// </summary>
        private void UpdateBalance()
        {
            var tickerRes = _proxy.GetTicker(SelectedPair);
            if (!tickerRes.Success)
            {
                ErrorHelper.DisplayErrorMessage(tickerRes.ErrorMessage);
                return;
            }
            var ticker = tickerRes.Result;
            string currency = SelectedPair.Item2;
            OnTickerUpdate(ticker);

            var balRes = _proxy.GetBalance(SelectedPair);
            if (balRes.Success)
            {
                var bal = balRes.Result;
                _fee = bal.Fee;
                lblItemBalance.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00###} {1}", bal.Balances[SelectedPair.Item1], SelectedPair.Item1);
                lblCurrencyBalance.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00###} {1}", bal.Balances[SelectedPair.Item2], currency);
            }
            else
            {
                ErrorHelper.DisplayErrorMessage(balRes.ErrorMessage);
            }

        }

        /// <summary>
        /// Updates the ask and bid prices
        /// </summary>
        /// <param name="ticker"></param>
        private void OnTickerUpdate(Ticker ticker)
        {
            string currency = SelectedPair.Item2;

            lblBuy.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00###} {1}", ticker.Bid, currency);
            lblSell.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00###} {1}", ticker.Ask, currency);

            if (string.IsNullOrEmpty(txtBuyPrice.Text))
            {
                txtBuyPrice.Text = ticker.Ask.ToString(CultureInfo.InvariantCulture);
            }

            if (string.IsNullOrEmpty(txtSellPrice.Text))
            {
                txtSellPrice.Text = ticker.Bid.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Refreshes data when the user clicks on the refresh button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateBalance();
        }

        private void OnFillItemAmount(object sender, EventArgs e)
        {
            decimal balance = decimal.Parse(lblItemBalance.Text.Substring(0, lblItemBalance.Text.IndexOf(" ")), CultureInfo.InvariantCulture);
            decimal buyPrice = decimal.Parse(txtBuyPrice.Text, CultureInfo.InvariantCulture);

            decimal result = balance / buyPrice;
            txtBuyAmount.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00######}", result);
        }

        private void OnFillCurrencyAmount(object sender, EventArgs e)
        {
            txtSellAmount.Text = lblCurrencyBalance.Text.Substring(0, lblCurrencyBalance.Text.IndexOf(" "));
        }

        private void OnFillPriceCurrency(object sender, EventArgs e)
        {
            txtSellPrice.Text = lblSell.Text.Substring(0, lblSell.Text.IndexOf(" "));
        }

        private void OnFillPriceItem(object sender, EventArgs e)
        {
            txtBuyPrice.Text = lblBuy.Text.Substring(0, lblBuy.Text.IndexOf(" "));
        }

        /// <summary>
        /// Updates Buy total when the amount/price to buy has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBuyTotal(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuyAmount.Text) && !string.IsNullOrEmpty(txtBuyPrice.Text))
            {
                decimal total = decimal.Parse(txtBuyAmount.Text, CultureInfo.InvariantCulture) * decimal.Parse(txtBuyPrice.Text, CultureInfo.InvariantCulture);
                decimal totalFee = total * _fee / 100m;
                bool feeDeductedFromTotal = SelectedPair.Exchange.FeeDeductedFromTotal;
                lblBuyFee.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.####} {1}", totalFee, _proxy.GetFeeUnit(SelectedPair, OrderType.Buy));
                lblBuyTotal.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.####} {1}", feeDeductedFromTotal ? total : total + totalFee, SelectedPair.Item2);
            }
        }

        /// <summary>
        /// Updates Sell total when the amount/price to sell has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSellTotal(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSellAmount.Text) && !string.IsNullOrEmpty(txtSellPrice.Text))
            {
                decimal total = decimal.Parse(txtSellAmount.Text, CultureInfo.InvariantCulture) * decimal.Parse(txtSellPrice.Text, CultureInfo.InvariantCulture);
                decimal totalFee = total * _fee / 100m;
                bool feeDeductedFromTotal = SelectedPair.Exchange.FeeDeductedFromTotal;
                lblSellFee.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.####} {1}", totalFee, _proxy.GetFeeUnit(SelectedPair, OrderType.Sell));
                lblSellTotal.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.####} {1} ", feeDeductedFromTotal ? total : total - totalFee, SelectedPair.Item2);
            }
        }

        /// <summary>
        /// Places a buy order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuy_Click(object sender, EventArgs e)
        {
            try
            {
                var orderDetailsRes = _proxy.PlaceBuyOrder(Convert.ToDecimal(txtBuyAmount.Text, CultureInfo.InvariantCulture), Convert.ToDecimal(txtBuyPrice.Text, CultureInfo.InvariantCulture), SelectedPair);
                if (orderDetailsRes.Success)
                {
                    EventAggregator.Instance.Publish(new OrderSentMessage());
                }
                else
                {
                    ErrorHelper.DisplayErrorMessage(orderDetailsRes.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                ErrorHelper.DisplayErrorMessage(ex.ToString());
            }
        }

        /// <summary>
        /// Places a sell order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSell_Click(object sender, EventArgs e)
        {
            try
            {
                var orderDetailsRes = _proxy.PlaceSellOrder(Convert.ToDecimal(txtSellAmount.Text, CultureInfo.InvariantCulture), Convert.ToDecimal(txtSellPrice.Text, CultureInfo.InvariantCulture), SelectedPair);
                if (orderDetailsRes.Success)
                {
                    EventAggregator.Instance.Publish(new OrderSentMessage());
                }
                else
                {
                    ErrorHelper.DisplayErrorMessage(orderDetailsRes.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.DisplayErrorMessage(ex.ToString());
            }
        }
    }
}
