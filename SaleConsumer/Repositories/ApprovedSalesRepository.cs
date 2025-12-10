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
    public class ApprovedSalesRepository : IApprovedSalesRepository
    {
        private readonly ILogger<SaleRepository> _logger;

        private readonly IMongoCollection<SaleResponseDTO> _mongoCollection;

        private readonly ConnectionDB _connection;

        public ApprovedSalesRepository(
            ILogger<SaleRepository> logger,
            ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _mongoCollection = connection.GetMongoCollectionAprovedSales();
        }

        public async Task CreateApprovedSaleAsync(SaleResponseDTO sale)
        {
            await _mongoCollection.InsertOneAsync(sale);
        }
    }
}
