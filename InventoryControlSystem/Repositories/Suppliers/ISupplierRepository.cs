using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Suppliers
{
    public interface ISupplierRepository
    {
        Task<Supplier> GetSupplier(string id);

        Task CreateSupplier(Supplier supplier);

        Task<bool> DeleteSupplier(string id);

        Task<IEnumerable<Supplier>> GetAllSuppliers();

        Task<bool> UpdateSupplier(Supplier supplierChanges);
    }
}
