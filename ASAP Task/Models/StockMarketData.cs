using System.ComponentModel.DataAnnotations;

namespace ASAP_Task.Models
{
    public class StockMarketData
    {
        [Key]
        public int ID{ get; set; }
        public string Ticker { get; set; }
        public int QueryCount { get; set; }
        public int ResultsCount { get; set; }
        public bool Adjusted { get; set; }
        public string Status { get; set; }
        public string RequestId { get; set; }
        public int Count { get; set; }
        public double Volume { get; set; }
        public double VWAP { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double NumberOfTrades { get; set; }
        public override string ToString()
        {
            return $"StockMarketData ID: {ID}\n, Ticker: {Ticker}\n, QueryCount: {QueryCount}\n, " +
                   $"ResultsCount: {ResultsCount}\n, Adjusted: {Adjusted}\n, Status: {Status}\n, " +
                   $"RequestId: {RequestId}\n, Count: {Count}\n, Volume: {Volume}\n, VWAP: {VWAP}\n, " +
                   $"OpenPrice: {OpenPrice}\n, ClosePrice: {ClosePrice}\n, HighPrice: {HighPrice}\n, " +
                   $"LowPrice: {LowPrice}\n, NumberOfTrades: {NumberOfTrades}\n";
        }

    }
}
