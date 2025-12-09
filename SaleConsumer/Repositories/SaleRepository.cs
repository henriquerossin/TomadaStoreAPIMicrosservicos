using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SaleConsumer.Repositories.Interfaces;
using System.Text;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SaleConsumer.Data;

namespace TomadaStore.SaleConsumer.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ILogger<SaleRepository> _logger;

        private readonly IMongoCollection<SaleResponseDTO> _mongoCollection;

        private readonly ConnectionDB _connection;

        public SaleRepository(
            ILogger<SaleRepository> logger,
            ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _mongoCollection = connection.GetMongoCollection();
        }

        public async Task CreateSaleAsync(SaleResponseDTO sale)
        {
            //var products = new List<Product>();
            //decimal total = 0;

            //foreach (var (productDTO, quantity) in items)
            //{
            //    var category = new Category(
            //        new ObjectId(productDTO.Category.Id),
            //        productDTO.Category.Name,
            //        productDTO.Category.Description
            //    );

            //    var product = new Product(
            //        new ObjectId(productDTO.Id),
            //        productDTO.Name,
            //        productDTO.Description,
            //        productDTO.Price,
            //        category,
            //        quantity
            //    );

            //    products.Add(product);

            //    total += productDTO.Price * quantity;
            //}

            //var customer = new Customer(
            //    customerDTO.Id,
            //    customerDTO.FirstName,
            //    customerDTO.LastName,
            //    customerDTO.Email,
            //    customerDTO.PhoneNumber
            //);

            //var finalSale = new Sale(customer, products, total);

            await _mongoCollection.InsertOneAsync(sale);
        }
    }
}
