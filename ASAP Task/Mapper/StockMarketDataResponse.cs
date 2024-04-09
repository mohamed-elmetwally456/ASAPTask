namespace ASAP_Task.Mapper
{
    public class StockMarketDataResponse
    {
        public string Ticker { get; set; }
        public int QueryCount { get; set; }
        public int ResultsCount { get; set; }
        public bool Adjusted { get; set; }
        public List<ResultItem> Results { get; set; }
        public string Status { get; set; }
        public string request_id { get; set; }
        public int Count { get; set; }
    }
}
