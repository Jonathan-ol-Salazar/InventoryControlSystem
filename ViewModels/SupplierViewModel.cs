using InventoryControlSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.ViewModels
{
    public class SupplierViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Supplier Supplier { get; set; }
    }
}
