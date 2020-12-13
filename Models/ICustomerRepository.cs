using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    interface ICustomerRepository
    {
        Customer GetCustomer(int ID);

        Customer CreateCustomer(Customer customer);

        Customer DeleteCustomer(int ID);

        IEnumerable<Customer> GetAllCustomers();

        Customer UpdateCustomer(Customer customerChanges);
    }
}
