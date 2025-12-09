using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleItemDTO
    {
        [BsonElement("productId")]
        public string ProductId { get; init; }
        [BsonElement("quantity")]
        public int Quantity { get; init; }
    }
}
