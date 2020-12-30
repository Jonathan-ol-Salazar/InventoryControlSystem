using InventoryControlSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.ViewModels
{
    public class InvoiceCustomerViewModel
    {
        public Business Business { get; set; }
        public Customer Customer { get; set; }
        public InvoiceCustomer InvoiceCustomer { get; set; }
        public IEnumerable<Product> Products { get; set; }


    }
}
