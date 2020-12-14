using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext context;

        public SupplierRepository(AppDbContext context)
        {
            this.context = context;
        }


        public Supplier CreateSupplier(Supplier supplier)
        {
            context.Suppliers.Add(supplier);
            context.SaveChanges();
            return supplier;
        }

        public Supplier DeleteSupplier(int ID)
        {
            Supplier supplier = context.Suppliers.Find(ID);
            if (supplier != null)
            {
                context.Suppliers.Remove(supplier);
                context.SaveChanges();
            }
            return supplier;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return context.Suppliers;
        }

        public Supplier GetSupplier(int ID)
        {
            return context.Suppliers.Find(ID);
        }

        public Supplier UpdateSupplier(Supplier supplierChanges)
        {
            var supplier = context.Suppliers.Attach(supplierChanges);

            supplier.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();


            return supplierChanges;
        }
    }
}
