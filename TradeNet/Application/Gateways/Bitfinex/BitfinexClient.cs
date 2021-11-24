using TradeNet.Application.Abstractions;
using TradeNet.Models;

namespace TradeNet.Application.Gateways.Bitfinex
{
    public class BitfinexClient : IExchangeClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public BitfinexClient(ILogger<BitfinexClient> logger, IHttpClientFactory httpClientFactory)
        {
            _ = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClientFactory.CreateClient("bitfinex");
        }

        public async Task<Ticker> GetTickerAsync(string tradingPair = "tBTCUSD") 
        {
            var res = await _httpClient.GetFromJsonAsync<List<decimal>>($"ticker/{tradingPair}");

            if (res == null)
            {
                throw new Exception("Something went wrong lik lik");
            }

            var ticker = new Ticker(res);

            return ticker;
        }
    }
}
