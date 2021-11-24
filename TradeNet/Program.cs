using Microsoft.Extensions.Options;
using TradeNet.Application.Abstractions;
using TradeNet.Application.Gateways.Bitfinex;
using TradeNet.Controllers;
using TradeNet.Models;
using TradeNet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Thresholds>(builder.Configuration.GetSection("Thresholds"));
builder.Services.AddSingleton<Thresholds>(x => x.GetRequiredService<IOptions<Thresholds>>().Value);

var conf = builder.Configuration;
var baseUrl = conf["Bitfinex:Url"];

builder.Services.AddHttpClient("bitfinex", (client) => {
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddSingleton<IExchangeClient, BitfinexClient>();
builder.Services.AddSingleton<BotManager>();

var serviceProvider = builder.Services.BuildServiceProvider();
var manager = serviceProvider.GetRequiredService<BotManager>();
await manager.Init();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
