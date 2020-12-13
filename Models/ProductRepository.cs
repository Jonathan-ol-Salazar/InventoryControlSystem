using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Product CreateProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int ID)
        {
            Product product = context.Products.Find(ID);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products;
        }

        public Product GetProduct(int ID)
        {
            return context.Products.Find(ID);
        }

        public Product UpdateProduct(Product productChanges)
        {
            var product = context.Products.Attach(productChanges);

            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();


            return productChanges;
        }
    }
}
