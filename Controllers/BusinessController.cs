using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Businesses;
using InventoryControlSystem.Repositories.Orders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Controllers
{
    public class BusinessController : Controller
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IOrderRepository _orderRepository;


        public BusinessController(IBusinessRepository businessRepository, IOrderRepository orderRepository)
        {
            _businessRepository = businessRepository;
            _orderRepository = orderRepository;
        }


        // GET: Business
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Table of Businesses";

            return View(await _businessRepository.GetAllBusinesss());
        }

        // GET: Business/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var business = await _businessRepository.GetBusiness(id);
            if (business == null)
            {
                return NotFound();

            }
            ViewData["Title"] = "View Business";

            return View(business);
        }

        // GET: Business/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New Business";

            return View();
        }

        // POST: Business/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Email,Phone,Address")] Business business)
        {
            if (ModelState.IsValid)
            {
                await _businessRepository.CreateBusiness(business);
                business.ID = business.Id;
                await _businessRepository.UpdateBusiness(business);
                return RedirectToAction(nameof(Index));
            }
            return View(business);
        }

        // GET: Business/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Business business = await _businessRepository.GetBusiness(id);
            if (business == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit Business";

            return View(business);
        }

        // POST: Business/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name,Email,Phone,Address,Selected")] Business business)
        {

            if (ModelState.IsValid)
            {
                var businessFromDb = await _businessRepository.GetBusiness(id);
                if (businessFromDb == null)
                {
                    return new NotFoundResult();
                }
                business.Id = businessFromDb.Id;
                await _businessRepository.UpdateBusiness(business);
                TempData["Message"] = "Business Updated Successfully";

            }
            return RedirectToAction("Index");

        }

        // GET: Business/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Business business = await _businessRepository.GetBusiness(id);
            if (business == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete Business";

            return View(business);
        }

        // POST: Business/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Business business = await _businessRepository.GetBusiness(id);

            await _businessRepository.DeleteBusiness(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Select(string id)
        {
            Business currentSelected = await _businessRepository.GetSelectedBusiness();
            if(currentSelected != null)
            {
                currentSelected.Selected = false;
            }

            Business business = await _businessRepository.GetBusiness(id);
            business.Selected = true;
            await _businessRepository.UpdateBusiness(business);
            return RedirectToAction(nameof(Index));

        }


    }

}
