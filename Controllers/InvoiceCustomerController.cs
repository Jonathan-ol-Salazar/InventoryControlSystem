using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.InvoiceCustomers;
using InventoryControlSystem.Repositories.Products;
using InventoryControlSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Controllers
{
    public class InvoiceCustomerController : Controller
    {
        private readonly IInvoiceCustomerRepository _invoiceCustomerRepository;
        private readonly IProductRepository _productRepository;



        public InvoiceCustomerController(IInvoiceCustomerRepository invoiceCustomerRepository, IProductRepository productRepository)
        {
            _invoiceCustomerRepository = invoiceCustomerRepository;
            _productRepository = productRepository;
        }


        // GET: InvoiceCustomer
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Table of Customer Invoices";
            return View(await _invoiceCustomerRepository.GetAllInvoiceCustomers());
        }

        // GET: InvoiceCustomer/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceCustomer(id);
            if (invoiceCustomer == null)
            {
                return NotFound();

            }
            List<Product> products = new List<Product>();
            foreach(string ID in invoiceCustomer.ProductsID)
            {
                products.Add(await _productRepository.GetProduct(ID));
            }

            InvoiceCustomerViewModel invoiceCustomerViewModel = new InvoiceCustomerViewModel
            {
                InvoiceCustomer = invoiceCustomer,
                Products = products
            };

            ViewData["Title"] = "View Customer Invoices";

            return View(invoiceCustomerViewModel);

        }

        public async Task<IActionResult> PrintInvoice(string id)
        {
            var invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceCustomer(id);

            List<Product> products = new List<Product>();
            foreach (string ID in invoiceCustomer.ProductsID)
            {
                products.Add(await _productRepository.GetProduct(ID));
            }

            InvoiceCustomerViewModel invoiceCustomerViewModel = new InvoiceCustomerViewModel
            {
                InvoiceCustomer = invoiceCustomer,
                Products = products
            };

            ViewData["Title"] = "View Customer Invoices";

            return View(invoiceCustomerViewModel);
        }


        // GET: InvoiceCustomer/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New Customer Invoices";

            return View();
        }

        // POST: InvoiceCustomer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] InvoiceCustomer invoiceCustomer)
        {
            if (ModelState.IsValid)
            {
                await _invoiceCustomerRepository.CreateInvoiceCustomer(invoiceCustomer);
                invoiceCustomer.ID = invoiceCustomer.Id;
                await _invoiceCustomerRepository.UpdateInvoiceCustomer(invoiceCustomer);
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceCustomer);
        }

        // GET: InvoiceCustomer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InvoiceCustomer invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceCustomer(id);
            if (invoiceCustomer == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit Customer Invoices";

            return View();
        }

        // POST: InvoiceCustomer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name")] InvoiceCustomer invoiceCustomer)
        {

            if (ModelState.IsValid)
            {
                var invoiceCustomerFromDb = await _invoiceCustomerRepository.GetInvoiceCustomer(id);
                if (invoiceCustomerFromDb == null)
                {
                    return new NotFoundResult();
                }
                invoiceCustomer.Id = invoiceCustomerFromDb.Id;
                await _invoiceCustomerRepository.UpdateInvoiceCustomer(invoiceCustomer);
                TempData["Message"] = "Customer Updated Successfully";

            }
            return RedirectToAction("Index");

        }

        // GET: InvoiceCustomer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InvoiceCustomer invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceCustomer(id);
            if (invoiceCustomer == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete Customer Invoices";

            return View(invoiceCustomer);
        }

        // POST: InvoiceCustomer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            InvoiceCustomer invoiceCustomer = await _invoiceCustomerRepository.GetInvoiceCustomer(id);

            await _invoiceCustomerRepository.DeleteInvoiceCustomer(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
