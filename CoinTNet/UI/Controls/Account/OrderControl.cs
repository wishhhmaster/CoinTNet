using CoinTNet.Common;
using CoinTNet.DAL;
using CoinTNet.DAL.Interfaces;
using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
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
        private Fee _fee;
        /// <summary>
        /// Gets the currently selected currency pair
        /// </summary>
        private CurrencyPair _selectedPair;
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
            EventAggregator.Instance.Subscribe<SecuredDataChanged>(m => OnSecuredDataChanged(m));
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
                _selectedPair = m.Pair;
                lblItem1Balance.Text = lblItem2Balance.Text = string.Empty;
                txtBuyAmount.Text = txtBuyPrice.Text = txtSellAmount.Text = txtSellPrice.Text = string.Empty;
                var feeRes = _proxy.GetFee(m.Pair);
                lblCurrency1.Text = lblCurrency2.Text = m.Pair.Item2;
                lblSellAmountCurrency.Text = lblBuyAmountCurrency.Text = m.Pair.Item1;

                _fee = feeRes.Success ? feeRes.Result : new Fee { BuyFee = 0.5m, SellFee = 0.5m };
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
            var tickerRes = _proxy.GetTicker(_selectedPair);
            if (!tickerRes.Success)
            {
                ErrorHelper.DisplayErrorMessage(tickerRes.ErrorMessage);
                return;
            }
            var ticker = tickerRes.Result;
            string currency = _selectedPair.Item2;
            OnTickerUpdate(ticker);

            var balRes = _proxy.GetBalance(_selectedPair);
            if (balRes.Success)
            {
                var bal = balRes.Result;
                _fee = _proxy.GetFee(_selectedPair).Result;
                lblItem1Balance.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00######} {1}", bal.Balances[_selectedPair.Item1], _selectedPair.Item1);
                lblItem2Balance.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00######} {1}", bal.Balances[_selectedPair.Item2], currency);
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
            string currency = _selectedPair.Item2;

            lblLowestAsk.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00######} {1}", ticker.Ask, currency);
            lblHighestBid.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00######} {1}", ticker.Bid, currency);

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

        /// <summary>
        /// When the user clicks on the item1 balance, fill the # of items to sell with the balance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillSellAmount(object sender, EventArgs e)
        {
            txtSellAmount.Text = lblItem1Balance.Text.Substring(0, lblItem1Balance.Text.IndexOf(" "));
        }

        /// <summary>
        /// When the user clicks on the item2 balance, fills the # of items to buy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillBuyAmount(object sender, EventArgs e)
        {
            decimal balance = decimal.Parse(lblItem2Balance.Text.Substring(0, lblItem2Balance.Text.IndexOf(" ")), CultureInfo.InvariantCulture);
            decimal buyPrice = 0;
            if (decimal.TryParse(txtBuyPrice.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out buyPrice))
            {
                decimal amount = balance / buyPrice;

                if (!_selectedPair.Exchange.FeeDeductedFromTotal)
                {
                    amount = 100 * balance / ((100 + _fee.BuyFee) * buyPrice);
                }

                txtBuyAmount.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00######}", amount);
            }
        }

        /// <summary>
        /// Sets sell ptice to highest bid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHighestBidClick(object sender, EventArgs e)
        {
            txtSellPrice.Text = lblHighestBid.Text.Substring(0, lblHighestBid.Text.IndexOf(" "));
        }

        /// <summary>
        /// Sets buy price to lowest ask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLowestAskClick(object sender, EventArgs e)
        {
            txtBuyPrice.Text = lblLowestAsk.Text.Substring(0, lblLowestAsk.Text.IndexOf(" "));
        }

        /// <summary>
        /// Updates Buy total when the amount/price to buy has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBuyTotal(object sender, EventArgs e)
        {
            decimal amount, price;
            if (decimal.TryParse(txtBuyAmount.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out amount) && decimal.TryParse(txtBuyPrice.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out price))
            {
                decimal total = amount * price;
                decimal totalFee = total * _fee.BuyFee / 100m;
                bool feeDeductedFromTotal = _selectedPair.Exchange.FeeDeductedFromTotal;
                lblBuyFee.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.######} {1}", totalFee, _proxy.GetFeeUnit(_selectedPair, OrderType.Buy));
                lblBuyTotal.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.######} {1}", feeDeductedFromTotal ? total : total + totalFee, _selectedPair.Item2);
            }
        }

        /// <summary>
        /// Updates Sell total when the amount/price to sell has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSellTotal(object sender, EventArgs e)
        {
            decimal amount, price;

            if (decimal.TryParse(txtSellAmount.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out amount) && decimal.TryParse(txtSellPrice.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out price))
            {
                decimal total = decimal.Parse(txtSellAmount.Text, CultureInfo.InvariantCulture) * decimal.Parse(txtSellPrice.Text, CultureInfo.InvariantCulture);
                decimal totalFee = total * _fee.SellFee / 100m;
                bool feeDeductedFromTotal = _selectedPair.Exchange.FeeDeductedFromTotal;
                lblSellFee.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.######} {1}", totalFee, _proxy.GetFeeUnit(_selectedPair, OrderType.Sell));
                lblSellTotal.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.######} {1} ", feeDeductedFromTotal ? total : total - totalFee, _selectedPair.Item2);
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
                decimal amount, price;
                if (decimal.TryParse(txtBuyAmount.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out amount) && decimal.TryParse(txtBuyPrice.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out price))
                {
                    var orderDetailsRes = _proxy.PlaceBuyOrder(amount, price, _selectedPair);
                    if (orderDetailsRes.Success)
                    {
                        EventAggregator.Instance.Publish(new OrderSentMessage());
                    }
                    else
                    {
                        ErrorHelper.DisplayErrorMessage(orderDetailsRes.ErrorMessage);
                    }
                }
                else
                {
                    ErrorHelper.DisplayErrorMessage("Please enter amount and prices in a correct format");
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
                decimal amount, price;
                if (decimal.TryParse(txtSellAmount.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out amount) && decimal.TryParse(txtSellPrice.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out price))
                {
                    var orderDetailsRes = _proxy.PlaceSellOrder(amount, price, _selectedPair);
                    if (orderDetailsRes.Success)
                    {
                        EventAggregator.Instance.Publish(new OrderSentMessage());
                    }
                    else
                    {
                        ErrorHelper.DisplayErrorMessage(orderDetailsRes.ErrorMessage);
                    }
                }
                else
                {
                    ErrorHelper.DisplayErrorMessage("Please enter amount and prices in a correct format");
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.DisplayErrorMessage(ex.ToString());
            }
        }
    }
}
