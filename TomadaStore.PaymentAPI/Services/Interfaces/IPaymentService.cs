using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.PaymentAPI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task ProcessSalesAsync();

        Task ValidateSaleAsync(SaleResponseDTO sale);
    }
}
