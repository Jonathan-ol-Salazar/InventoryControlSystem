using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext context)
        {
            this.context = context;
        }


        public Customer CreateCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }

        public Customer DeleteCustomer(int ID)
        {
            Customer customer = context.Customers.Find(ID);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
            return customer;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return context.Customers;
        }

        public Customer GetCustomer(int ID)
        {
            return context.Customers.Find(ID);
        }

        public Customer UpdateCustomer(Customer customerChanges)
        {
            var customer = context.Customers.Attach(customerChanges);

            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();


            return customerChanges;
        }
    }
}
