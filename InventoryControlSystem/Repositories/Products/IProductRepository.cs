using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryControlSystem.Models;

namespace InventoryControlSystem.Repositories.Products
{
    public interface IProductRepository
    {
        Task<Product> GetProduct(string id);

        Task CreateProduct(Product product);

        Task<bool> DeleteProduct(string id);

        Task<IEnumerable<Product>> GetAllProducts();

        Task<bool> UpdateProduct(Product productChanges);

    }
}
