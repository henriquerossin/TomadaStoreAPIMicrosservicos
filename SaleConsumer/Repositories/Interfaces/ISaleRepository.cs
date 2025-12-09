using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace SaleConsumer.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task CreateSaleAsync(SaleResponseDTO sale);
    }
}
