using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomer(string id);

        Task CreateCustomer(Customer customer);

        Task<bool> DeleteCustomer(string id);

        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<bool> UpdateCustomer(Customer customerChanges);
    }
}
