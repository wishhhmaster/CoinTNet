using CoinTNet.BLL.TradingIndicator;
using CoinTNet.DAL.Exchanges;
using CoinTNet.DAL.Interfaces;
using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using CoinTNet.DO.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinTNet.BLL.TradingStrategies
{
    /// <summary>
    /// Represents a trading strategy
    /// </summary>
    abstract class BaseTradingStrategy
    {
        #region Privaate members
        /// <summary>
        /// The proxy to use for trading
        /// </summary>
        protected IExchange _proxy;
        /// <summary>
        /// The last order
        /// </summary>
        protected OrderDetails _lastOrder;
        /// <summary>
        /// The last advice sent by the indicator
        /// </summary>
        private Advice _lastAdvice;
        /// <summary>
        /// The current pair
        /// </summary>
        private CurrencyPair _pair;
        /// <summary>
        /// The strategy settings
        /// </summary>
        private StrategySettings _settings;
        /// <summary>
        /// The indicator used in the strategy
        /// </summary>
        private ITradingIndicator _tradingIndicator;

        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        /// <param name="settings">the strategy settings</param>
        /// <param name="tradingIndicator">The indicator to use</param>
        public BaseTradingStrategy(StrategySettings settings, ITradingIndicator tradingIndicator)
        {
            _settings = settings;
            _tradingIndicator = tradingIndicator;
            _pair = settings.Pair;
            Actions = new List<MarketAction>();

            // Only do back testing trading for now
            _proxy = new FakeExchange();
            if (_proxy is FakeExchange)
            {
                (_proxy as FakeExchange).Init(settings.InitialItem1Balance, settings.InitialItem2Balance, settings.Fee);
            }

        }

        /// <summary>
        /// Gets a list of the actions executed during a strategy session
        /// </summary>
        public List<MarketAction> Actions { get; private set; }

        /// <summary>
        /// Gets The result of the strategy
        /// </summary>
        /// <returns></returns>
        public StrategyResult GetResult()
        {
            var balRes = _proxy.GetBalance(_pair);
            return new StrategyResult
            {
                BalanceItem1 = balRes.Result.Balances[_pair.Item1],
                BalanceItem2 = balRes.Result.Balances[_pair.Item2]
            };
        }

        /// <summary>
        /// Method called on each new candle
        /// </summary>
        /// <param name="candles">The list of available candles</param>
        public void OnTick(IList<OHLC> candles)
        {
            if (candles.Count == 0)
            {
                return;
            }

            var candle = candles.Last();
            var balRes = _proxy.GetBalance(_settings.Pair);
            if (!balRes.Success)
            {
                return;
            }
            var bal = balRes.Result;
            var fee = bal.Fee;

            decimal totalAmountItem2 = bal.Balances[_pair.Item2];
            decimal totalAmountItem1 = bal.Balances[_pair.Item1];

            decimal availableAmountItem2ToBuy = totalAmountItem2 - _settings.MinAmountItem2ToKeep;
            decimal availableAmountItem1ForSell = totalAmountItem1 - _settings.MinAmountItem1ToKeep;

            var ordersRes = _proxy.GetOpenOrders(_settings.Pair);
            if (!ordersRes.Success)
            {
                return;
            }

            bool lastOrderFilled = _lastOrder == null || ordersRes.Result.List.FirstOrDefault(o => o.Id == _lastOrder.Id) == null;

            // return;

            var advice = _tradingIndicator.GetAdvice(candles);
            if (advice != Advice.None)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToShortTimeString() + " - Advice: " + advice.ToString());
                if (advice == Advice.Buy)
                {
                    if (_lastAdvice == Advice.Sell)
                    {
                        if (!lastOrderFilled)
                        {
                            var res = _proxy.CancelOrder(_lastOrder.Id);
                            if (res.Success)
                            {
                                availableAmountItem2ToBuy += _lastOrder.Amount * _lastOrder.Price;
                            }
                        }
                    }
                    else if (_lastAdvice == Advice.Buy)
                    {

                    }

                    availableAmountItem2ToBuy = availableAmountItem2ToBuy > _settings.MaxAmountMoneyPerBuyOrder ? _settings.MaxAmountMoneyPerBuyOrder : availableAmountItem2ToBuy;
                    decimal nbUnitsToBuy = Math.Round(availableAmountItem2ToBuy / candle.Close, 8);
                    //TODO: handle fees
                    PlaceBuyOrder(nbUnitsToBuy, candle.Close, candle.Date);
                }
                else if (advice == Advice.Sell)
                {
                    if (_lastAdvice == Advice.Buy)
                    {
                        if (!lastOrderFilled)
                        {
                            var res = _proxy.CancelOrder(_lastOrder.Id);
                            if (res.Success)
                            {
                                availableAmountItem1ForSell += _lastOrder.Amount;
                            }
                        }
                    }
                    else if (_lastAdvice == Advice.Sell)
                    {

                    }
                    decimal transactionFee = Math.Round(availableAmountItem1ForSell * candle.Close * fee / 100m, 8);

                    decimal nbUnitsToSell = availableAmountItem1ForSell > _settings.MaxNbBTCPerSellOrder ? _settings.MaxNbBTCPerSellOrder : availableAmountItem1ForSell;
                    //TODO: handle fees
                    //Sell here
                    PlaceSellOrder(nbUnitsToSell, candle.Close, candle.Date);
                }
            }
            _lastAdvice = advice;
        }

        /// <summary>
        /// Places a buy order
        /// </summary>
        /// <param name="nbUnitsToBuy"></param>
        /// <param name="price"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        protected CallResult<OrderDetails> PlaceBuyOrder(decimal nbUnitsToBuy, decimal price, DateTime date)
        {
            var orderRes = _proxy.PlaceBuyOrder(nbUnitsToBuy, price, _pair);
            if (orderRes.Success)
            {
                Actions.Add(new MarketAction
                {
                    ActionType = ActionType.Bid,
                    AmountCurrency = price,
                    AmountItem = nbUnitsToBuy,
                    Date = date
                });
                _lastOrder = orderRes.Result;
            }
            return orderRes;
        }

        /// <summary>
        /// Places a sell order
        /// </summary>
        /// <param name="nbUnitsToSell"></param>
        /// <param name="price"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        protected CallResult<OrderDetails> PlaceSellOrder(decimal nbUnitsToSell, decimal price, DateTime date)
        {
            var orderRes = _proxy.PlaceSellOrder(nbUnitsToSell, price, _pair);
            if (orderRes.Success)
            {
                Actions.Add(new MarketAction
                {
                    ActionType = ActionType.Ask,
                    AmountCurrency = price,
                    AmountItem = nbUnitsToSell,
                    Date = date
                });
                _lastOrder = orderRes.Result;
            }
            return orderRes;
        }
    }
}
