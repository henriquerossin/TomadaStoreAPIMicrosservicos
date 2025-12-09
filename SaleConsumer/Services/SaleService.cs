using Microsoft.AspNetCore.Connections;
using MongoDB.Bson.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SaleConsumer.Repositories.Interfaces;
using SaleConsumer.Services.Interfaces;
using System.Text;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace SaleConsumer.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository saleRepository, ILogger<SaleService> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        public async Task CreateSaleAsync()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "Sale", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                var finalSale = JsonSerializer.Deserialize<SaleResponseDTO>(message);

                _logger.LogInformation("Received: ", message);

                await _saleRepository.CreateSaleAsync(finalSale);
            };

            var sale = await channel.BasicConsumeAsync("Sale", autoAck: true, consumer: consumer);
        }
    }
}
