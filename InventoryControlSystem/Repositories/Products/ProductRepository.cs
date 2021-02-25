using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryControlSystem.Models;

namespace InventoryControlSystem.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;
        private readonly IMongoCollection<Product> _productsContext;

        public ProductRepository(IProductContext context)
        {
            _context = context;
            _productsContext = _context.Products;
        }


        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productsContext.Find(Builders<Product>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateProducts(Product )
        //{
        //    await _productsContext.InsertManyAsync(product);
        //}



        public async Task<Product> GetProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            return await _productsContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _productsContext.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            ReplaceOneResult updateResult = await _productsContext.ReplaceOneAsync(filter: b => b.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            //var ids = product.Select(d => d.Id);

            //var filter = Builders<Product>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("product_id", new BsonDocument("$in", new BsonArray(product)));

            //DeleteResult deleteResult = await _productsContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _productsContext.DeleteOneAsync(product => product.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }
    }
}

