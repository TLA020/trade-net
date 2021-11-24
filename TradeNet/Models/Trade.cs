namespace TradeNet.Models
{
    public class Trade
    {
        public decimal BuyPrice { get; set; } 
        public decimal SellPrice { get; set; } 

        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }

        public decimal Difference => (SellPrice - BuyPrice) / BuyPrice * 100;

    }
}
