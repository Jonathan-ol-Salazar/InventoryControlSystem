using InventoryControlSystem.Models;
using System.Collections.Generic;

namespace InventoryControlSystem.ViewModels
{
    public class InvoiceBusinessViewModel
    {
        public Business Business { get; set; }
        public Customer Customer { get; set; }
        public InvoiceBusiness InvoiceBusiness { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
