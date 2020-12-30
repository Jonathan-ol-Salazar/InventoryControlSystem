﻿using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.InvoiceCustomers;
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



        public InvoiceCustomerController(IInvoiceCustomerRepository invoiceCustomerRepository)
        {
            _invoiceCustomerRepository = invoiceCustomerRepository;
        }


        // GET: InvoiceCustomer
        public async Task<IActionResult> Index()
        {
            var x = await _invoiceCustomerRepository.GetAllInvoiceCustomers();
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
            ViewData["Title"] = "View InvoiceCustomer";

            return View(invoiceCustomer);

        }

        // GET: InvoiceCustomer/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New InvoiceCustomer";

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

            ViewData["Title"] = "Edit InvoiceCustomer";

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
            ViewData["Title"] = "Delete InvoiceCustomer";

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