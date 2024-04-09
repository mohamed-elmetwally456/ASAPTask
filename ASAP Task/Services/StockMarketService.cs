using System;
using System.Net.Http;
using System.Threading.Tasks;
using ASAP_Task.Mapper;
using ASAP_Task.Models;
using ASAP_Task.Reposatory;
using ASAP_Task.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class StockMarketService
{
    private readonly IConfiguration configuration;
    private readonly IHttpClientFactory httpClientFactory;
    private readonly Context dbContext;
    private readonly IClientRepo clientRepo;
    private readonly IEmailService emailService;

    public StockMarketService(IConfiguration configuration, IHttpClientFactory httpClientFactory, Context dbContext,IClientRepo clientRepo , IEmailService emailService)
    {
        this.configuration = configuration;
        this.httpClientFactory = httpClientFactory;
        this.dbContext = dbContext;
        this.clientRepo = clientRepo;
        this.emailService = emailService;
    }

    public async Task FetchAndStoreStockMarketData()
    {
        try
        {
            string apiKey = configuration["PolygonApiKey"];
            string apiUrl = $"https://api.polygon.io/v2/aggs/ticker/AAPL/range/1/day/2023-01-09/2023-01-09?apiKey={apiKey}";

            using (var client = httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var stockMarketDataResponse = JsonConvert.DeserializeObject<StockMarketDataResponse>(responseData);
                    if (stockMarketDataResponse.Status == "OK" && stockMarketDataResponse.Results.Count > 0)
                    {
                        var result = stockMarketDataResponse.Results[0];
                        var stockMarketData = new StockMarketData
                        {
                            RequestId = stockMarketDataResponse.request_id,
                            Ticker = stockMarketDataResponse.Ticker,
                            QueryCount= stockMarketDataResponse.QueryCount,
                            Status = stockMarketDataResponse.Status,
                            Adjusted = stockMarketDataResponse.Adjusted,
                            ResultsCount = stockMarketDataResponse.ResultsCount,
                            Count = stockMarketDataResponse.Count,
                            Volume = result.V,
                            VWAP = result.VW,
                            OpenPrice = result.O,
                            ClosePrice = result.C,
                            HighPrice = result.H,
                            LowPrice = result.L,
                            NumberOfTrades = result.N,
                        };
                        dbContext.StockMarketData.Add(stockMarketData);
                        await dbContext.SaveChangesAsync();
                        //Sending Email To clients Contains Result
                        string Subject = "Stock market results here...";
                        foreach (var clt in clientRepo.GetAll())
                        {
                            await emailService.SendEmailAsync(clt.Email, Subject, stockMarketData.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid or empty response from Polygon.io API.");
                    }
                }
                else
                {
                    throw new Exception($"Failed to fetch data from Polygon.io API. Status code: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FetchAndStoreStockMarketData: {ex.Message}");
        }
    }
}
