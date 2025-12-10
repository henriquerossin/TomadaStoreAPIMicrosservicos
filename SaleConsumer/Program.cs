using MongoDB.Driver;
using SaleConsumer.Repositories.Interfaces;
using TomadaStore.SaleConsumer.Repositories;
using TomadaStore.SaleConsumer.Data;
using SaleConsumer.Services;
using SaleConsumer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Isso aqui lê o bloco de MongoDB do appsettings.json pra conectar o mongo
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

// Registra o ConnectionDB
builder.Services.AddSingleton<ConnectionDB>();

// Repository e Service
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddScoped<IApprovedSalesRepository, ApprovedSalesRepository>();

builder.Services.AddScoped<ISaleService, SaleService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
