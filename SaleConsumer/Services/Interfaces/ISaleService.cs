using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace SaleConsumer.Services.Interfaces
{
    public interface ISaleService
    {
        Task CreateSaleAsync();
    }
}
