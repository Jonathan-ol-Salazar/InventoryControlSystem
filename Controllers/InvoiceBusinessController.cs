using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.InvoiceBusinesses;
using InventoryControlSystem.Repositories.Products;
using InventoryControlSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Controllers
{
    public class InvoiceBusinessController : Controller
    {
        private readonly IInvoiceBusinessRepository _invoiceCustomerRepository;
        private readonly IProductRepository _productRepository;



        public InvoiceBusinessController(IInvoiceBusinessRepository invoiceCustomerRepository, IProductRepository productRepository)
        {
            _invoiceCustomerRepository = invoiceCustomerRepository;
            _productRepository = productRepository;
        }


        // GET: InvoiceBusiness
        public async Task<IActionResult> Index()
        {
            var x = await _invoiceCustomerRepository.GetAllInvoiceBusinesses();
            return View(await _invoiceCustomerRepository.GetAllInvoiceBusinesses());
        }

        // GET: InvoiceBusiness/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceBusiness(id);
            if (invoiceCustomer == null)
            {
                return NotFound();

            }
            List<Product> products = new List<Product>();
            foreach (string ID in invoiceCustomer.ProductsID)
            {
                products.Add(await _productRepository.GetProduct(ID));
            }

            InvoiceBusinessViewModel invoiceCustomerViewModel = new InvoiceBusinessViewModel
            {
                InvoiceBusiness = invoiceCustomer,
                Products = products
            };

            ViewData["Title"] = "View InvoiceBusiness";

            return View(invoiceCustomerViewModel);

        }

        public async Task<IActionResult> PrintInvoice(string id)
        {
            var invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceBusiness(id);

            List<Product> products = new List<Product>();
            foreach (string ID in invoiceCustomer.ProductsID)
            {
                products.Add(await _productRepository.GetProduct(ID));
            }

            InvoiceBusinessViewModel invoiceCustomerViewModel = new InvoiceBusinessViewModel
            {
                InvoiceBusiness = invoiceCustomer,
                Products = products
            };

            ViewData["Title"] = "View InvoiceBusiness";

            return View(invoiceCustomerViewModel);
        }


        // GET: InvoiceBusiness/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New InvoiceBusiness";

            return View();
        }

        // POST: InvoiceBusiness/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] InvoiceBusiness invoiceCustomer)
        {
            if (ModelState.IsValid)
            {
                await _invoiceCustomerRepository.CreateInvoiceBusiness(invoiceCustomer);
                invoiceCustomer.ID = invoiceCustomer.Id;
                await _invoiceCustomerRepository.UpdateInvoiceBusiness(invoiceCustomer);
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceCustomer);
        }

        // GET: InvoiceBusiness/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InvoiceBusiness invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceBusiness(id);
            if (invoiceCustomer == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit InvoiceBusiness";

            return View();
        }

        // POST: InvoiceBusiness/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name")] InvoiceBusiness invoiceCustomer)
        {

            if (ModelState.IsValid)
            {
                var invoiceCustomerFromDb = await _invoiceCustomerRepository.GetInvoiceBusiness(id);
                if (invoiceCustomerFromDb == null)
                {
                    return new NotFoundResult();
                }
                invoiceCustomer.Id = invoiceCustomerFromDb.Id;
                await _invoiceCustomerRepository.UpdateInvoiceBusiness(invoiceCustomer);
                TempData["Message"] = "Customer Updated Successfully";

            }
            return RedirectToAction("Index");

        }

        // GET: InvoiceBusiness/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InvoiceBusiness invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceBusiness(id);
            if (invoiceCustomer == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete InvoiceBusiness";

            return View(invoiceCustomer);
        }

        // POST: InvoiceBusiness/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            InvoiceBusiness invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceBusiness(id);

            await _invoiceCustomerRepository.DeleteInvoiceBusiness(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
