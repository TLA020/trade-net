namespace TradeNet.Models
{
    public class TradeHistory
    {
        public decimal Profit => Trades.Sum(t => t.Difference);
        public List<Trade> Trades { get; set; } = new List<Trade>();

        public Trade Current => Trades.First(t => t.EndedAt == default);


        public void Start(decimal price)
        {
            var trade = new Trade
            {
                StartedAt = DateTime.Now,
                BuyPrice = price
            };

            Trades.Add(trade);
        }

        public void End(decimal price)
        {
            Current.EndedAt = DateTime.Now;
            Current.SellPrice = price;
        }
    }
}
