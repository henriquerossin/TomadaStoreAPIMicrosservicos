using Dapper;
using Microsoft.Data.SqlClient;
using TomadaStore.CustomerAPI.Data;
using TomadaStore.CustomerAPI.Repository.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly SqlConnection _connection;

        public CustomerRepository(ILogger<CustomerRepository> logger, ConnectionDB connectionDB)
        {
            _logger = logger;
            _connection = connectionDB.GetConnection();
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                var insertSql =
                    @"INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber) 
                    VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";

                await _connection.ExecuteAsync(
                    insertSql, new
                    {
                        customer.FirstName,
                        customer.LastName,
                        customer.Email,
                        customer.PhoneNumber
                    });
            }
            catch (SqlException e)
            {
                _logger.LogError("SQL Error inserting customer: " + e.StackTrace);
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Error inserting customer: " + e.StackTrace);
                throw new Exception(e.Message);
            }
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                var insertSql =
                    @"SELECT Id, FirstName, LastName, Email, PhoneNumber FROM Customers
                    WHERE Id = @CustomerId";

                return await _connection.QueryFirstOrDefaultAsync<CustomerResponseDTO>(insertSql, new { CustomerId = id });
            }
            catch (SqlException e)
            {
                _logger.LogError("SQL Error retriving customer: " + e.StackTrace);
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retriving customer: " + e.StackTrace);
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                var insertSql =
                    @"SELECT Id, FirstName, LastName, Email, PhoneNumber FROM Customers";

                var customers = await _connection.QueryAsync<CustomerResponseDTO>(insertSql);

                return customers.ToList();
            }
            catch (SqlException e)
            {
                _logger.LogError("SQL Error retriving customers: " + e.StackTrace);
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retriving customers: " + e.StackTrace);
                throw new Exception(e.Message);
            }
        }
    }
}
