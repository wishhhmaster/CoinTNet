using CoinTNet.DO;
using CoinTNet.DO.Exchanges;
using CoinTNet.UI.Common.EventAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTNet.UI.Common
{

    class ApplicationStartMessage : IMessage
    {

    }
    class StatusUpdateMessage : IMessage
    {
        public string Message { get; set; }
    }
    class OrderSentMessage : IMessage
    {

    }
    class OrderCancelledMessage : IMessage
    {

    }
    class TickerUpdateMessage : IMessage
    {
        public Ticker Ticker { get; set; }
    }
    class ExchangeSelected : IMessage
    {
        /// <summary>
        /// The exchange's internal code
        /// </summary>
        public string InternalCode { get; set; }
        public SelectorType SelectorType { get; set; }

    }
    class PairSelected : IMessage
    {
        public CurrencyPair Pair { get; set; }
        public SelectorType SelectorType { get; set; }
    }
    class NotificationReadMessage<T> : IMessage
    {
        public T Object { get; set; }
    }
}
