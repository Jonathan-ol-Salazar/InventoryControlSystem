using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Suppliers;
using InventoryControlSystem.Repositories.Products;
using InventoryControlSystem.ViewModels;
using System.Collections.Generic;
using System.Collections;

namespace InventoryControlSystem.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductRepository _productRepository;



        public SupplierController(ISupplierRepository supplierRepository, IProductRepository productRepository)
        {
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
        }


        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Table of Suppliers";
            return View(await _supplierRepository.GetAllSuppliers());
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var supplier = await _supplierRepository.GetSupplier(id);
            if (supplier == null)
            {
                return NotFound();

            }
            List<Product> productList = new List<Product>();
            foreach (string product in supplier.ProductsID)
            {
                productList.Add(await _productRepository.GetProduct(product));

            }
            SupplierViewModel supplierViewModel = new SupplierViewModel()
            {
                Products = productList,
                Supplier = supplier
            };
            ViewData["Title"] = "View Supplier";

            return View(supplierViewModel);

        }

        // GET: Supplier/Create
        public async Task<IActionResult> Create()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel()
            {
                Products = await _productRepository.GetAllProducts()
                
            };
            ViewData["Title"] = "Create New Supplier";

            return View(supplierViewModel);
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Email,Phone,Address,ProductsID")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //// Set OrderListsID to new list
                //supplier.OrderListsID = new List<string>();
                await _supplierRepository.CreateSupplier(supplier);
                supplier.ID = supplier.Id;
                await _supplierRepository.UpdateSupplier(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supplier supplier = await _supplierRepository.GetSupplier(id);
            if (supplier == null)
            {
                return NotFound();
            }
            SupplierViewModel supplierViewModel = new SupplierViewModel()
            {
                Products = await _productRepository.GetAllProducts(),
                Supplier = supplier
            };
            ViewData["Title"] = "Edit Supplier";

            return View(supplierViewModel);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,ID,Name,Email,Phone,Address,ProductsID")] Supplier supplier)
        {

            if (ModelState.IsValid)
            {
                var supplierFromDb = await _supplierRepository.GetSupplier(supplier.ID);
                if (supplierFromDb == null)
                {
                    return new NotFoundResult();
                }
                supplier.Id = supplierFromDb.Id;
                await _supplierRepository.UpdateSupplier(supplier);
                TempData["Message"] = "Supplier Updated Successfully";

            }
            return RedirectToAction("Index");

        }

        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supplier supplier = await _supplierRepository.GetSupplier(id);
            if (supplier == null)
            {
                return NotFound();
            }
            List<Product> productList = new List<Product>();
            foreach(string ID in supplier.ProductsID)
            {
                productList.Add(await _productRepository.GetProduct(ID));
            }

            SupplierViewModel supplierViewModel = new SupplierViewModel
            {
                Products = productList,
                Supplier = supplier
            };
            ViewData["Title"] = "Delete Supplier";

            return View(supplierViewModel);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Supplier supplier = await _supplierRepository.GetSupplier(id);

            // Delete Products
            foreach (string ID in supplier.ProductsID)
            {
                await _productRepository.DeleteProduct(ID);
            }


            await _supplierRepository.DeleteSupplier(id);
            return RedirectToAction(nameof(Index));
        }

        //private async bool SupplierExists(string id)
        //{
        //    Supplier supplier = await _supplierRepository.GetSupplier(id);

        //    if


        //    return await _context.Suppliers.FindAsync(id);

        //}
    }
}
