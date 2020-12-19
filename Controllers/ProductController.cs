using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Products;

namespace InventoryControlSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;


        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        // GET: Product
        public async Task<IActionResult> Index()
        {
            var x = await _productRepository.GetAllProducts();
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
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New Product";

            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, [Bind("ID,Name,Type,Brand,Quantity,Price,Size,NumUnits")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.CreateProduct(product);

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
            ViewData["Title"] = "Edit Product";

            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name,Type,Brand,Quantity,Price,Size,NumUnits")] Product product)
        {

            if (ModelState.IsValid)
            {
                var productFromDb = await _productRepository.GetProduct(id);
                if (productFromDb == null)
                {
                    return new NotFoundResult();
                }
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
