using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleItemMessageDTO
    {
        public ProductResponseDTO Product { get; init; }
        public int Quantity { get; init; }
        public decimal TotalPrice { get; init; }
    }
}
