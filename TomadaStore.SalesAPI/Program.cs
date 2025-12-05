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

// Lê o bloco MongoDB do appsettings.json
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB")
);

// Registra o ConnectionDB com DI
builder.Services.AddSingleton<ConnectionDB>();

// REPOSITORY E SERVICE
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

// HTTP CLIENTS
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
