using MongoDB.Driver;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Repository;
using TomadaStore.SalesAPI.Data;
using TomadaStore.SalesAPI.Repositories;
using TomadaStore.SalesAPI.Repositories.Interfaces;
using TomadaStore.SalesAPI.Services;
using TomadaStore.SalesAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Isso aqui lê o bloco de MongoDB do appsettings.json pra conectar o mongo
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

// Registra o ConnectionDB
builder.Services.AddSingleton<ConnectionDB>();

// Repository e Service
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

// HTTP Clients para comunicação com CustomerAPI e ProductAPI
builder.Services.AddHttpClient("CustomerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/v1/Customer/");
});

builder.Services.AddHttpClient("ProductAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:6001/api/v1/Product/");
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
