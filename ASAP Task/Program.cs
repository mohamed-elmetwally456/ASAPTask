using ASAP_Task.Models;
using ASAP_Task.Reposatory;
using ASAP_Task.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace ASAP_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string txt = "hi";
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("Con1")));
            builder.Services.AddHangfireServer();
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<StockMarketService>();
            builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("Con1")));
            builder.Services.AddScoped<IClientRepo,ClientRepo>();
            builder.Services.AddScoped<IFilterRepo,FilterRepo>();
            builder.Services.AddTransient<IEmailService,EmailService>();

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(txt);

            app.UseAuthorization();

            app.UseHangfireDashboard(pathMatch:"/Dashboard");

            RecurringJob.AddOrUpdate<StockMarketService>("FetchAndStoreStockMarketData",
            service => service.FetchAndStoreStockMarketData(),
            Cron.HourInterval(6));

            app.MapControllers();

            app.Run();
        }
    }
}
