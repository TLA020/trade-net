using TradeNet.Application.Abstractions;
using TradeNet.Models;

namespace TradeNet.Services
{
    public class BotManager
    {
        private readonly ILogger _logger;
        private readonly IExchangeClient _client;

        private Thresholds _tresholds;
        private BotState _state;
        private List<decimal> _lastOpPrices = new();
        private decimal lastAveragePrice => _lastOpPrices.Average();

        private TradeHistory _tradeHistory = new();


        // temp trade history


        public BotManager(ILogger<BotManager> logger, IExchangeClient client, Thresholds thresholds)
        {
            _state = BotState.Buy;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = client;
            _tresholds = thresholds;
        }

        public async Task Init() 
        {
            var currentPrice = (await _client.GetTickerAsync()).LastPrice;
            _logger.LogInformation("..::Inital price on starting bot: {c}", currentPrice.ToString("c2"));
            _lastOpPrices.Add(currentPrice);


            while (true)
            {
                await AttemptTrade();
                Thread.Sleep(5000);
            }
        }

        private async Task AttemptTrade() 
        {
            var ticker = await _client.GetTickerAsync();
            var currentPrice = ticker.LastPrice;

            _lastOpPrices.Add(currentPrice);
            var diff = (currentPrice - lastAveragePrice) / lastAveragePrice * 100;

            _logger.LogInformation("..:: Current Price: {c}, Difference: {d}%::..", currentPrice.ToString("c2"), decimal.Round(diff, 4));

            _logger.LogInformation("..:: Trades: {x}, Profit: {p}%", _tradeHistory.Trades.Count, decimal.Round(_tradeHistory.Profit, 4));

            if (_state == BotState.Buy)
            {
                TryToBuy(diff, currentPrice);
            }
            else 
            {
                 TryToSell(diff, currentPrice);
            }
        } 

        public void TryToBuy(decimal difference, decimal price)
        {
            if (difference >= _tresholds.UpwardTrend || difference <= _tresholds.Dip)
            {
                _logger.LogInformation("..:: Placed buy order at {p}", price.ToString("c2"));
                _tradeHistory.Start(price);
                _state = BotState.Sell;
                return;
            }
        }

        public void TryToSell(decimal difference, decimal price)
        {
            if (difference >= _tresholds.Profit || difference <= _tresholds.StopLoss)
            {
                _logger.LogInformation("..:: Sold at {p}", price.ToString("c2"));
                _tradeHistory.End(price);

                _logger.LogInformation(":::..PROFIT: {p}%", decimal.Round(_tradeHistory.Profit, 4));
                _state = BotState.Buy;
                return;
            }
        }
    }
}
