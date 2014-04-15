using System;

namespace CoinTNet.UI.Common.EventAggregator
{
    //http://www.minddriven.de/index.php/technology/development/design-patterns/event-aggregator-implementation

    public interface IMessage { }
    public interface ISubscription<TMessage> : IDisposable
    where TMessage : IMessage
    {
        Action<TMessage> Action { get; }
        IEventAggregator EventAggregator { get; }
    }

    public class Subscription<TMessage> : ISubscription<TMessage>
        where TMessage : IMessage
    {
        public Action<TMessage> Action { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }

        public Subscription(IEventAggregator eventAggregator, Action<TMessage> action)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            if (action == null) throw new ArgumentNullException("action");

            EventAggregator = eventAggregator;
            Action = action;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                EventAggregator.UnSubscribe(this);
        }
    }
}
