using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryControlSystem.Models;
using MongoDB.Driver;

namespace InventoryControlSystem.Repositories.Suppliers
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ISupplierContext _context;
        private readonly IMongoCollection<Supplier> _suppliersContext;

        public SupplierRepository(ISupplierContext context)
        {
            _context = context;
            _suppliersContext = _context.Suppliers;
        }


        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            return await _suppliersContext.Find(Builders<Supplier>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateSuppliers(Supplier )
        //{
        //    await _suppliersContext.InsertManyAsync(supplier);
        //}



        public async Task<Supplier> GetSupplier(string id)
        {
            FilterDefinition<Supplier> filter = Builders<Supplier>.Filter.Eq(x => x.Id, id);
            return await _suppliersContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateSupplier(Supplier supplier)
        {
            await _suppliersContext.InsertOneAsync(supplier);
        }

        public async Task<bool> UpdateSupplier(Supplier supplier)
        {
            ReplaceOneResult updateResult = await _suppliersContext.ReplaceOneAsync(filter: b => b.Id == supplier.Id, replacement: supplier);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteSupplier(string id)
        {
            //var ids = supplier.Select(d => d.Id);

            //var filter = Builders<Supplier>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("supplier_id", new BsonDocument("$in", new BsonArray(supplier)));

            //DeleteResult deleteResult = await _suppliersContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _suppliersContext.DeleteOneAsync(supplier => supplier.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }
    }
}
