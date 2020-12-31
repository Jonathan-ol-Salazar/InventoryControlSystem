using InventoryControlSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.ViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public OrderList OrderList { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public Product Product {get; set; }
    }
}
