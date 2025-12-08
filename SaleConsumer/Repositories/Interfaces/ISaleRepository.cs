using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;

namespace SaleConsumer.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task CreateSaleAsync(Sale sale);
    }
}
