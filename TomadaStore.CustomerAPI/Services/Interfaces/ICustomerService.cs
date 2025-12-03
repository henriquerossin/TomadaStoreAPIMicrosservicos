using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task InsertCustomerAsync(Customer customer);
        Task<List<Customer>> GetAllCustomerAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
