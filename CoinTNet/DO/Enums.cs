
namespace CoinTNet.DO
{
    public enum TradeSource
    {
        NotSet,
        BitcoinCharts,
        Database,
        BTCe,
        Bitstamp,
        CSV,
    }

    public enum MessageType
    {
        Information,
        Warning,
        Error,
        Confirmation
    }
}
