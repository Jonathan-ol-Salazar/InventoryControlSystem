using InventoryControlSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; }

        public Product Product { get; set; }

        public Supplier Supplier { get; set; }

    }
}
