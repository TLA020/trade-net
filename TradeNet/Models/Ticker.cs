using System.ComponentModel;

namespace TradeNet.Models
{
    public class Ticker
    {
        [Description("Price of last highest bid")]
        public decimal Bid { get; set; }

        [Description("Sum of the 25 highest bid sizes)")
            ]
        public decimal BidSize { get; set; }

        [Description("Price of last lowest ask")]
        public decimal Ask { get; set; }

        [Description("Sum of the 25 lowest ask sizes")]
        public decimal AskSize { get; set; }

        [Description("Amount that the last price has changed since yesterday")]
        public decimal DailyChange { get; set; }

        [Description("Relative price change since yesterday (*100 for percentage change)")]
        public decimal DailyChangeRelative { get; set; }

        [Description("Price of the last trade")]
        public decimal LastPrice { get; set; }

        [Description("Daily volume")]
        public decimal Volume { get; set; }

        [Description("Daily high")]
        public decimal High { get; set; }

        [Description("Daily low")]
        public decimal Low { get; set; }


        public Ticker(List<decimal> bitFinex)
        {
            Bid = bitFinex[0];
            BidSize = bitFinex[1];
            Ask = bitFinex[2];  
            AskSize = bitFinex[3];
            DailyChange = bitFinex[4];
            DailyChangeRelative = bitFinex[5];
            LastPrice = bitFinex[6];   
            Volume = bitFinex[7];   
            High = bitFinex[8]; 
            Low= bitFinex[9];   
        }
    }
}
