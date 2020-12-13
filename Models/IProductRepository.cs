using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public interface IProductRepository
    {
        Product GetProduct(int ID);

        Product CreateProduct(Product product);

        Product DeleteProduct(int ID);

        IEnumerable<Product> GetAllProducts();

        Product UpdateProduct(Product productChanges);

    }
}
