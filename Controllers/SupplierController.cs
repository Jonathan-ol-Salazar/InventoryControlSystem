using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Suppliers;

namespace InventoryControlSystem.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;


        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }


        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            var x = await _supplierRepository.GetAllSuppliers();
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
            ViewData["Title"] = "View Supplier";

            return View(supplier);

        }

        // GET: Supplier/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New Supplier";

            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,Phone,Address,Orders")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierRepository.CreateSupplier(supplier);

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
            ViewData["Title"] = "Edit Supplier";

            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ID,FirstName,LastName,Email,Phone,Address,Orders")] Supplier supplier)
        {

            if (ModelState.IsValid)
            {
                var supplierFromDb = await _supplierRepository.GetSupplier(id);
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
            ViewData["Title"] = "Delete Supplier";

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Supplier supplier = await _supplierRepository.GetSupplier(id);

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
