using System;
using System.Collections.Generic;

namespace CoinTNet.DO.Exchanges
{
    /// <summary>
    /// Represents a list of transactions
    /// </summary>
    public class TransactionList
    {
        public List<Transaction> Transactions { get; set; }
    }

    /// <summary>
    /// Represents a transaction
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets price
        /// </summary>
        public decimal Price { get;  set; }
        /// <summary>
        /// Gets or sets the amount
        /// </summary>
        public decimal Amount { get;  set; }
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public long ID { get;  set; }
        /// <summary>
        /// Gets or sets the date and time of the transaction
        /// </summary>
        public DateTime Date { get;  set; }
        /// <summary>
        /// Gets or sets the timestamp of the transaction
        /// </summary>
        public long Timestamp { get;  set; }
    }
}
