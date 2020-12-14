using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string OrderID { get; set; }
        public int NumProducts { get; set; }
        public List<Product> Products { get; set; }
        public Customer Customer { get; set; }
        public string Status { get; set; }
        public bool Fulfilled { get; set; }
        public bool Ordered { get; set; }

    }
}
