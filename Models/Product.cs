using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Size { get; set; }

        public int NumUnits { get; set; }
        public Supplier Supplier { get; set; }

    }
}
