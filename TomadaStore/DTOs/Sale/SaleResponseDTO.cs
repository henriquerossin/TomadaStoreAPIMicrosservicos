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
        public CustomerResponseDTO customer { get; init; }

        [BsonElement("items")]
        public List<SaleItemDTO> Items { get; init; }
    }
}
