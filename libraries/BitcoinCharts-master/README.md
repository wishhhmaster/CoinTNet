BitcoinCharts
=============

Uses Reactive Extensions to publish information to subscribers.

Here's a simple example to help get your started:
```c#
static void Main(string[] args) {
    var client = new BitcoinChartsClient();

    client.GetTradesAsync()
        .Result.ToObservable()
        .Subscribe(x => Console.WriteLine(x));

    client.Trades.Subscribe(x => Console.WriteLine(x));

    client.ConnectAsync();

    Console.ReadLine();
}
```
