using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BitstampAPI
{
    /// <summary>
    /// Container for transactions
    /// </summary>
    public class TransactionList
    {
        /// <summary>
        /// The list of transactions
        /// </summary>
        public List<Transaction> Transactions { get; private set; }

        public static TransactionList ReadFromJObject(JArray o)
        {
            var list = new TransactionList();
            list.Transactions = new List<Transaction>();

            foreach (var item in o)
            {
                list.Transactions.Add(Transaction.CreateFromJObject(item as JObject));
            }


            return list;
        }

    }

    /// <summary>
    /// Represents a transaction
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets price
        /// </summary>
        public decimal Price { get; private set; }
        /// <summary>
        /// Gets or sets the amount
        /// </summary>
        public decimal Amount { get; private set; }
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public long ID { get; private set; }
        /// <summary>
        /// Gets or sets the date and time of the transaction
        /// </summary>
        public DateTime Date { get; private set; }
        /// <summary>
        /// Gets or sets the timestamp of the transaction
        /// </summary>
        public long Timestamp { get; private set; }

        public static Transaction CreateFromJObject(JObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            var t = new Transaction()
            {
                ID = obj.Value<long>("tid"),
                Price = obj.Value<decimal>("price"),
                Timestamp = obj.Value<long>("date")
            };
            //converts the amount as follows because it can sometimes be a float value (e.g. 1E-7)
            string amount = obj.Value<string>("amount");
            t.Amount = decimal.Parse(amount, NumberStyles.AllowExponent | NumberStyles.Float, CultureInfo.InvariantCulture);
            t.Date = UnixTimeHelper.ConvertToDateTime((uint)t.Timestamp);

            return t;
        }
    }


}
