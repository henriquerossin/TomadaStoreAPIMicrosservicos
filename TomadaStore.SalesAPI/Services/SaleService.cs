using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SalesAPI.Repositories.Interfaces;
using TomadaStore.SalesAPI.Services.Interfaces;

namespace TomadaStore.SalesAPI.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        private readonly ILogger<SaleService> _logger;

        private readonly HttpClient _httpClientProduct;
        private readonly HttpClient _httpClientCustomer;

        public SaleService(
        ISaleRepository saleRepository,
        ILogger<SaleService> logger,
        IHttpClientFactory factory)
        {
            _saleRepository = saleRepository;
            _logger = logger;

            _httpClientCustomer = factory.CreateClient("CustomerAPI");
            _httpClientProduct = factory.CreateClient("ProductAPI");
        }

        public async Task CreateSaleAsync(int idCustomer, List<SaleItemDTO> itemsDTO)
        {
            var customer = await _httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(idCustomer.ToString());

            var items = new List<SaleItemMessageDTO>();

            decimal totalPrice = 0;

            foreach (var item in itemsDTO)
            {
                var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(item.ProductId);
                items.Add(new SaleItemMessageDTO
                {
                    Product = product,
                    Quantity = item.Quantity,
                    TotalPrice = product.Price * item.Quantity
                });

                totalPrice += product.Price * item.Quantity;
            }

            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "Sale",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var message = JsonSerializer.Serialize(new { Customer = customer, Items = items, TotalPrice = totalPrice });
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "Sale", body: body);
        }
    }
}
