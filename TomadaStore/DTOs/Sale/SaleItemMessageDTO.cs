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
        public ProductResponseDTO Product { get; set; }
        public int Quantity { get; set; }
    }
}
