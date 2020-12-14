using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public interface ISupplierRepository
    {
        Supplier GetSupplier(int ID);

        Supplier CreateSupplier(Supplier supplier);

        Supplier DeleteSupplier(int ID);

        IEnumerable<Supplier> GetAllSuppliers();

        Supplier UpdateSupplier(Supplier supplierChanges);
    }
}
