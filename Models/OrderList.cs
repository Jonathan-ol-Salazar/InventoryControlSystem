using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class OrderList
    {
        public int ID { get; set; }
        public Supplier Supplier { get; set; }

        public string Business { get; set; }
        public List<Product> Products { get; set; }

        public int Price { get; set; }
        public DateTime OrderDate  { get; set; }

        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }

        public bool Confirmed { get; set; }
  

}
}
