using CoinTNet.DAL.Interfaces;
using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using System;

namespace CoinTNet.DAL.Exchanges
{
    /// <summary>
    /// Fake exchange, used in backtesting
    /// </summary>
    class FakeExchange : IExchange
    {
        #region Private members

        private decimal _availableItem2;
        private decimal _availableItem1;
        private decimal _fee;

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public FakeExchange()
        {
            _availableItem2 = 4000;
            _availableItem1 = 2;
            _fee = 0.3m;
        }

        public void Init(decimal balanceItem1, decimal balanceItem2, decimal fee)
        {
            _availableItem2 = balanceItem2;
            _availableItem1 = balanceItem1;
            _fee = fee;
        }
        public CallResult<Ticker> GetTicker(CurrencyPair pair)
        {
            throw new NotImplementedException();
        }
        public CallResult<OrderBook> GetOrderBook(CurrencyPair pair)
        {
            throw new NotImplementedException();
        }

        public CallResult<TransactionList> GetTransactions(bool lastMin, CurrencyPair pair)
        {
            throw new NotImplementedException();
        }


        public CallResult<Balance> GetBalance(CurrencyPair pair)
        {
            var bal = new Balance
            {
                Fee = 0.3m,
            };
            bal.Balances[pair.Item1] = _availableItem1;
            bal.Balances[pair.Item2] = _availableItem2;
            return new CallResult<Balance>(bal);

        }
        public CallResult<OrderDetails> PlaceBuyOrder(decimal amount, decimal price, CurrencyPair pair)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Buying {0:0.00} {2} @ {1:0.00} {3}", amount, price, pair.Item1, pair.Item2));
            _availableItem2 -= (amount * price + amount * price * _fee / 100);
            _availableItem1 += amount;
            return new CallResult<OrderDetails>(new OrderDetails
            {
                Amount = amount,
                Price = price,
                Type = OrderType.Buy
            });
        }

        public CallResult<OrderDetails> PlaceSellOrder(decimal amount, decimal price, CurrencyPair pair)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Selling {0:0.00} {2} @ {1:0.00} {3}", amount, price, pair.Item1, pair.Item2));
            _availableItem2 += (amount * price - amount * price * _fee / 100);
            _availableItem1 -= amount;
            return new CallResult<OrderDetails>(new OrderDetails
                {
                    Amount = amount,
                    Price = price,
                    Type = OrderType.Sell
                });
        }
        public CallResult<bool> CancelOrder(long orderId)
        {
            return new CallResult<bool>(true);
        }

        public CallResult<OpenOrders> GetOpenOrders(CurrencyPair pair)
        {
            return new CallResult<OpenOrders>(new OpenOrders());
        }
        public CallResult<decimal> GetFee(CurrencyPair pair)
        {
            return new CallResult<decimal>(_fee);
        }

        /// <summary>
        /// Gets the unit for the fee for a given pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <param name="type">The type of order (buy/sell)</param>
        /// <returns></returns>
        public string GetFeeUnit(CurrencyPair pair, OrderType type)
        {
            return type == OrderType.Buy ? pair.Item1 : pair.Item2;
        }
    }
}
