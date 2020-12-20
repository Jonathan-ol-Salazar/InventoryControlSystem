using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.ViewModels
{
    public class OrderCreateViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public Order order { get; set; }
    }
}
