using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task InsertCustomerAsync(Customer customer);
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
