using TradeNet.Models;

namespace TradeNet.Application.Abstractions
{
    public interface IExchangeClient
    {
        Task<Ticker> GetTickerAsync(string tradingPair = "tBTCUSD");
    }
}
