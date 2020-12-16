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
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OrderID,NumProducts,Status,Fulfilled,Ordered,OrderList")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OrderID,NumProducts,Status,Fulfilled,Ordered,OrderList")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
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
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }


        // ToOrder
        public async Task<IActionResult> ToOrder()
        {
            // get all the entries that have 'ordered' as false
            var toOrder = await _context.Orders.Where(p => p.OrderList == false).ToListAsync();
            return View("Index",toOrder);
        }

        // ToFulfill
        public async Task<IActionResult> ToFulfill()
        {
            // get all the entries that have 'fulfilled' as false
            var toFulfill = await _context.Orders.Where(p => p.Fulfilled == false).ToListAsync();
            return View("Index", toFulfill);
        }

        public async Task<IActionResult> Add2OrderList(int? id)
        {
            // Get selected item 
            // Set 'Order List' to true
            // Add item to an orderlist, create one if none exists
            // Refresh the screen to /toOrder
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderList = true;
            _context.Update(order);
            //await _context.SaveChangesAsync();

          
            // DUMB PRODUCTS LIST
            List<Product> Products = new List<Product>();
            
            Products.Add(new Product
            {
                Name = "",
                Type = "",
                Brand = "",
                Quantity = 1,
                Price = 1,
                Size = 1,
                NumUnits = 1,
                Supplier = new Supplier
                {
                    Name = "",
                    Email = "",
                    Phone = 0,
                    Address = "", 
                    Orders = ""
                }

            });

            order.Products = Products;

            //var toConfirm = await _context.OrderLists.Where(p => p.Confirmed == false).ToListAsync();

            // Loop through each product in order and add to order list
            foreach(Product product in order.Products)
            {
                // Check if OrderList from supplier exists
                var orderList4Supplier = await _context.OrderLists.Where(p => p.Supplier == product.Supplier && p.Confirmed == false).ToListAsync();

                // If no OrderList exists, create a new one
                if (orderList4Supplier == null || orderList4Supplier.Count() == 0)
                {
                 
                    OrderList newOrderList = new OrderList
                    {
                        Supplier = product.Supplier,
                        Business = "",
                        Products = new List<Product> {product},
                        Orders = new List<Order> {order},
                        Price = 0,
                        OrderDate = DateTime.Now,
                        BillingAddress = "",
                        ShippingAddress = "",
                        Confirmed = false
                    };

                    _context.OrderLists.Add(newOrderList);

                    await _context.SaveChangesAsync();

                }
                else
                {
                    // Add product to existing list


                    // Check if product is already in list
                    if (!orderList4Supplier[0].Products.Contains(product))
                    {
                        orderList4Supplier[0].Products.Add(product);
                    }

                    // Increase price of OrderList
                    orderList4Supplier[0].Price += product.Price;

                    // Increase quantity of units
                    orderList4Supplier[0].Products.Find(x => x.ID == product.ID).NumUnits += 1;

                    // Add order to OrderList
                    orderList4Supplier[0].Orders.Add(order);
                    
                    _context.OrderLists.Update(orderList4Supplier[0]);
                    //await _context.SaveChangesAsync();


                }

            }


            //await _context.SaveChangesAsync();


            return RedirectToAction(nameof(ToOrder));


        }


    }
}
