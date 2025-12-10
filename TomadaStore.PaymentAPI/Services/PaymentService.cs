using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.PaymentAPI.Services.Interfaces;

namespace TomadaStore.PaymentAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
        }

        public async Task ProcessSalesAsync()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };

                using var connection = await factory.CreateConnectionAsync();

                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "Sale",
                                                durable: false,
                                                exclusive: false,
                                                autoDelete: false,
                                                arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();

                    var message = Encoding.UTF8.GetString(body);

                    var finalSale = JsonSerializer.Deserialize<SaleResponseDTO>(message);

                    _logger.LogInformation("RAW MESSAGE: " + message);

                    _logger.LogInformation("Sale received: " + message);

                    await ValidateSaleAsync(finalSale);
                };

                await channel.BasicConsumeAsync(queue: "Sale",
                                                autoAck: true,
                                                consumer: consumer);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to process the received sale");
                throw;
            }
        }

        public async Task ValidateSaleAsync(SaleResponseDTO sale)
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };

                using var connection = await factory.CreateConnectionAsync();

                using var channel = await connection.CreateChannelAsync();

                if (sale.TotalPrice <= 1000)
                {
                    sale = new SaleResponseDTO
                    {
                        Customer = sale.Customer,
                        Items = sale.Items,
                        Date = sale.Date,
                        TotalPrice = sale.TotalPrice,
                        SaleStatus = true
                    };
                }
                else
                {
                    sale = new SaleResponseDTO
                    {
                        Customer = sale.Customer,
                        Items = sale.Items,
                        Date = sale.Date,
                        TotalPrice = sale.TotalPrice,
                        SaleStatus = false
                    };
                }

                await channel.QueueDeclareAsync(queue: "FinalSale",
                                                    durable: false,
                                                    exclusive: false,
                                                    autoDelete: false,
                                                    arguments: null);

                var saleString = JsonSerializer.Serialize(sale);

                var body = Encoding.UTF8.GetBytes(saleString);

                await channel.BasicPublishAsync(exchange: string.Empty,
                                                routingKey: "FinalSale",
                                                body: body);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to send back  the received sale");
                throw;
            }
        }
    }
}
