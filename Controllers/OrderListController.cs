using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryControlSystem.Models;

namespace InventoryControlSystem.Controllers
{
    public class OrderListController : Controller
    {
        private readonly AppDbContext _context;

        public OrderListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderList
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderLists.ToListAsync());
        }

        // GET: OrderList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderList = await _context.OrderLists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderList == null)
            {
                return NotFound();
            }

            return View(orderList);
        }

        // GET: OrderList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Type,Quantity,Price")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderList);
        }

        // GET: OrderList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderList = await _context.OrderLists.FindAsync(id);
            if (orderList == null)
            {
                return NotFound();
            }
            return View(orderList);
        }

        // POST: OrderList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Type,Quantity,Price")] OrderList orderList)
        {
            if (id != orderList.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderListExists(orderList.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderList);
        }

        // GET: OrderList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderList = await _context.OrderLists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderList == null)
            {
                return NotFound();
            }

            return View(orderList);
        }

        // POST: OrderList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderList = await _context.OrderLists.FindAsync(id);
            _context.OrderLists.Remove(orderList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderListExists(int id)
        {
            return _context.OrderLists.Any(e => e.ID == id);
        }
    }
}
