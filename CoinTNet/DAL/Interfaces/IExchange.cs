using CoinTNet.DO;
using CoinTNet.DO.Exchanges;

namespace CoinTNet.DAL.Interfaces
{
    /// <summary>
    /// Generic interface for exchange APIs
    /// </summary>
    interface IExchange
    {
        /// <summary>
        /// Gets the ticker for a given currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>The ticker</returns>
        CallResult<Ticker> GetTicker(CurrencyPair pair);
        /// <summary>
        /// Gets the balance for a given currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns></returns>
        CallResult<Balance> GetBalance(CurrencyPair pair);
        /// <summary>
        /// Places a sell order
        /// </summary>
        /// <param name="amount">The amount of units to buy</param>
        /// <param name="price">The price per unit</param>
        /// <param name="pair">The currency pair</param>
        /// <returns>Detaisl about the new order</returns>
        CallResult<OrderDetails> PlaceSellOrder(decimal amount, decimal price, CurrencyPair pair);
        /// <summary>
        /// Places a buy order
        /// </summary>
        /// <param name="amount">The amount of units to buy</param>
        /// <param name="price">The price per unit</param>
        /// <param name="pair">The currency pair</param>
        /// <returns>Detaisl about the new order</returns>
        CallResult<OrderDetails> PlaceBuyOrder(decimal amount, decimal price, CurrencyPair pair);
        /// <summary>
        /// Gets recent transactions
        /// </summary>
        /// <param name="lastMin"></param>
        /// <param name="pair">The currency pair</param>
        /// <returns>The most recent transactions</returns>
        CallResult<TransactionList> GetTransactions(bool lastMin, CurrencyPair pair);
        /// <summary>
        /// Gets the order book for a currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>The order book (bids/asks)</returns>
        CallResult<OrderBook> GetOrderBook(CurrencyPair pair);
        /// <summary>
        /// Gets all open orders
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>A list of open orders</returns>
        CallResult<OpenOrders> GetOpenOrders(CurrencyPair pair);
        /// <summary>
        /// Cancels an order
        /// </summary>
        /// <param name="orderId">The Id of the order to cancel</param>
        /// <returns>true if the order was cancelled</returns>
        CallResult<bool> CancelOrder(long orderId);
        /// <summary>
        /// Gets the fee associated to a currency pair
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <returns>The fee</returns>
        CallResult<Fee> GetFee(CurrencyPair pair);
        /// <summary>
        /// Gets the unit for the fee for a given pair and order type
        /// </summary>
        /// <param name="pair">The currency pair</param>
        /// <param name="type">The type of order (buy/sell)</param>
        /// <returns></returns>
        string GetFeeUnit(CurrencyPair pair, OrderType type);
        /// <summary>
        /// Returns the list of currency pairs available on the exchange
        /// </summary>
        /// <returns>A list of currency pairs</returns>
        CallResult<CurrencyPair[]> GetCurrencyPairs();

    }
}
