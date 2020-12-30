using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.InvoiceBusinesses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Controllers
{
    public class InvoiceBusinessController : Controller
    {
        private readonly IInvoiceBusinessRepository _invoiceBusinessRepository;



        public InvoiceBusinessController(IInvoiceBusinessRepository invoiceBusinessRepository)
        {
            _invoiceBusinessRepository = invoiceBusinessRepository;
        }


        // GET: InvoiceBusiness
        public async Task<IActionResult> Index()
        {
            var x = await _invoiceBusinessRepository.GetAllInvoiceBusinesses();
            return View(await _invoiceBusinessRepository.GetAllInvoiceBusinesses());
        }

        // GET: InvoiceBusiness/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var invoiceBusiness = await _invoiceBusinessRepository.GetInvoiceBusiness(id);
            if (invoiceBusiness == null)
            {
                return NotFound();

            }
            ViewData["Title"] = "View InvoiceBusiness";

            return View(invoiceBusiness);

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
        public async Task<IActionResult> Create([Bind("ID,Name")] InvoiceBusiness invoiceBusiness)
        {
            if (ModelState.IsValid)
            {
                await _invoiceBusinessRepository.CreateInvoiceBusiness(invoiceBusiness);
                invoiceBusiness.ID = invoiceBusiness.Id;
                await _invoiceBusinessRepository.UpdateInvoiceBusiness(invoiceBusiness);
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceBusiness);
        }

        // GET: InvoiceBusiness/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InvoiceBusiness invoiceBusiness = await _invoiceBusinessRepository.GetInvoiceBusiness(id);
            if (invoiceBusiness == null)
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
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name")] InvoiceBusiness invoiceBusiness)
        {

            if (ModelState.IsValid)
            {
                var invoiceBusinessFromDb = await _invoiceBusinessRepository.GetInvoiceBusiness(id);
                if (invoiceBusinessFromDb == null)
                {
                    return new NotFoundResult();
                }
                invoiceBusiness.Id = invoiceBusinessFromDb.Id;
                await _invoiceBusinessRepository.UpdateInvoiceBusiness(invoiceBusiness);
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

            InvoiceBusiness invoiceBusiness = await _invoiceBusinessRepository.GetInvoiceBusiness(id);
            if (invoiceBusiness == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete InvoiceBusiness";

            return View(invoiceBusiness);
        }

        // POST: InvoiceBusiness/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            InvoiceBusiness invoiceBusiness = await _invoiceBusinessRepository.GetInvoiceBusiness(id);

            await _invoiceBusinessRepository.DeleteInvoiceBusiness(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
