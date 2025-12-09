using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.Models
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("customer")]
        public Customer Customer { get; set; }

        [BsonElement("products")]
        public List<Product> Products { get; set; }

        [BsonElement("saleDate")]
        public DateTime SaleDate { get; set; }

        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; set; }

        [BsonConstructor]
        public Sale() { }

        [BsonConstructor]
        public Sale(
            ObjectId id,
            Customer customer,
            List<Product> products,
            DateTime saleDate,
            decimal totalPrice
        )
        {
            Id = id;
            Customer = customer;
            Products = products;
            SaleDate = saleDate;
            TotalPrice = totalPrice;
        }

        public Sale(
            Customer customer,
            List<Product> products,
            decimal totalPrice
        )
        {
            Id = ObjectId.GenerateNewId();
            Customer = customer;
            Products = products;
            SaleDate = DateTime.UtcNow;
            TotalPrice = totalPrice;
        }
    }
}
