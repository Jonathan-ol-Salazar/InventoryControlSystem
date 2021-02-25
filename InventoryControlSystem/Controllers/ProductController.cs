using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Products;
using InventoryControlSystem.Repositories.Suppliers;
using InventoryControlSystem.ViewModels;

namespace InventoryControlSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;


        public ProductController(IProductRepository productRepository, ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
        }


        // GET: Product
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Table of Products";
            return View(await _productRepository.GetAllProducts());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();

            }
            ViewData["Title"] = "View Product";

            return View(product);

        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                Suppliers = await _supplierRepository.GetAllSuppliers()
            };

            ViewData["Title"] = "Create New Product";

            return View(productViewModel);
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Type,Brand,NumBottles,BottleSize,NumUnits,TotalCost,SuppliersID")] Product product)
        {
            if (ModelState.IsValid)
            {
                string[] productIDName = product.SuppliersID.Split(':');
                product.SuppliersID = productIDName[0];
                product.SuppliersName = productIDName[1];

                await _productRepository.CreateProduct(product);
                product.ID = product.Id;
                await _productRepository.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel productViewModel = new ProductViewModel
            {
                Suppliers = await _supplierRepository.GetAllSuppliers(),
                Product = product
            };
            ViewData["Title"] = "Edit Product";

            return View(productViewModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,Name,Type,Brand,NumBottles,BottleSize,NumUnits,TotalCost,SuppliersID")] Product product)
        {
            if (ModelState.IsValid)
            {
                var productFromDb = await _productRepository.GetProduct(product.ID);
                if (productFromDb == null)
                {
                    return new NotFoundResult();
                }
                string[] productIDName = product.SuppliersID.Split(':');
                product.SuppliersID = productIDName[0];
                product.SuppliersName = productIDName[1];

                product.Id = productFromDb.Id;
                await _productRepository.UpdateProduct(product);
                TempData["Message"] = "Customer Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete Product";

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Product product = await _productRepository.GetProduct(id);

            // Supplier - remove ProductID
            Supplier supplier = await _supplierRepository.GetSupplier(product.SuppliersID);
            supplier.ProductsID.Remove(product.ID);

            await _supplierRepository.UpdateSupplier(supplier);


            await _productRepository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        //private async bool ProductExists(string id)
        //{
        //    Product product = await _productRepository.GetProduct(id);

        //    if


        //    return await _context.Products.FindAsync(id);

        //}
    }
}
