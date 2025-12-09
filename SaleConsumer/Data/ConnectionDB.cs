using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace TomadaStore.SaleConsumer.Data
{
    public class ConnectionDB
    {
        public readonly IMongoCollection<SaleResponseDTO> mongoCollection;
        public ConnectionDB(IOptions<MongoDBSettings> mongoDbSettings)
        {
            MongoClient client = new(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DataBaseName);
            mongoCollection = database.GetCollection<SaleResponseDTO>(mongoDbSettings.Value.CollectionName);
        }

        public IMongoCollection<SaleResponseDTO> GetMongoCollection()
        {
            return mongoCollection;
        }
    }
}
