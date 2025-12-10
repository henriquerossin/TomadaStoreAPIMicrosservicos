using TomadaStore.Models.DTOs.Sale;

namespace SaleConsumer.Repositories.Interfaces
{
    public interface IApprovedSalesRepository
    {
        Task CreateApprovedSaleAsync(SaleResponseDTO sale);
    }
}
