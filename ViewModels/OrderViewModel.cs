using InventoryControlSystem.Models;
using System.Collections.Generic;

namespace InventoryControlSystem.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }


    }
}
