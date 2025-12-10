using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomadaStore.Models.DTOs.Customer;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleResponseDTO
    {
        [BsonElement("customer")]
        public CustomerResponseDTO Customer { get; init; }

        [BsonElement("items")]
        public List<SaleItemMessageDTO> Items { get; init; }

        [BsonElement("date")]
        public DateTime Date = DateTime.Now;

        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; init; }

        [BsonElement("SaleStatus")]
        public bool SaleStatus { get; init; }
    }
}
