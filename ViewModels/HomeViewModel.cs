using InventoryControlSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<OrderList> OrderLists { get; set; }

        public Fund Fund { get; set; }

        public Order Order { get; set; }
        public OrderList OrderList { get; set; }

    }
}
