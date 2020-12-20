using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Orders;
using InventoryControlSystem.Repositories.OrderLists;
using System.Collections.Generic;
using System;

namespace InventoryControlSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderListRepository _orderListRepository;



        public OrderController(IOrderRepository orderRepository, IOrderListRepository orderListRepository)
        {
            _orderRepository = orderRepository;
            _orderListRepository = orderListRepository;

        }


        // GET: Order
        public async Task<IActionResult> Index()
        {
            var x = await _orderRepository.GetAllOrders();
            return View(await _orderRepository.GetAllOrders());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var order = await _orderRepository.GetOrder(id);
            if (order == null)
            {
                return NotFound();

            }
            ViewData["Title"] = "View Order";

            return View(order);

        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New Order";

            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OrderID,NumProducts,Products,Customer,Status,Fulfilled,Ordered,OrderList,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.CreateOrder(order);

                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _orderRepository.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit Order";

            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,OrderID,NumProducts,Products,Customer,Status,Fulfilled,Ordered,OrderList,OrderDate")] Order order)
        {

            if (ModelState.IsValid)
            {
                var orderFromDb = await _orderRepository.GetOrder(id);
                if (orderFromDb == null)
                {
                    return new NotFoundResult();
                }
                order.Id = orderFromDb.Id;
                await _orderRepository.UpdateOrder(order);
                TempData["Message"] = "Order Updated Successfully";

            }
            return RedirectToAction("Index");

        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _orderRepository.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete Order";

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Order order = await _orderRepository.GetOrder(id);

            await _orderRepository.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }

        //private async bool OrderExists(string id)
        //{
        //    Order order = await _orderRepository.GetOrder(id);

        //    if


        //    return await _context.Orders.FindAsync(id);

        //}
        // ToOrder
        //public async Task<IActionResult> ToOrder()
        //{
        //    // get all the entries that have 'ordered' as false
        //    var toOrder = await _orderRepository.GetAllOrders();
        //    return View("Index", toOrder);
        //}

        //// ToFulfill
        //public async Task<IActionResult> ToFulfill()
        //{
        //    // get all the entries that have 'fulfilled' as false
        //    var toFulfill = await _context.Orders.Where(p => p.Fulfilled == false).ToListAsync();
        //    return View("Index", toFulfill);
        //}


        public async Task<IActionResult> Add2OrderList(string id)
        {
            // Get selected item 
            // Set 'Order List' to true
            // Add item to an orderlist, create one if none exists
            // Refresh the screen to /toOrder
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.GetOrder(id);


            if (order == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Set orderList to true
                order.OrderList = true;

                //// DUMB PRODUCTS LIST
                List<Product> Products = new List<Product>();

                Products.Add(new Product
                {
                    Name = "Smirnoff Vodka",
                    Type = "Vodka",
                    Brand = "Smirnoff",
                    Quantity = 1,
                    Price = 1,
                    Size = 1,
                    NumUnits = 1,
                    SupplierName = "Smirnoff",
                    SupplierID = "1"
                });

                order.Products = Products;

                // Add each product from order to an orderlist
                foreach (Product product in order.Products)
                {
                    // Check if an existing orderlist for current product exists
                    var orderList4Supplier = await _orderListRepository.OrderListExist(product.SupplierName);

                    // If no OrderList exists, create a new one. Otherwise, add to existing one
                    if (orderList4Supplier == null)
                    {

                        OrderList newOrderList = new OrderList
                        {
                            SupplierName = product.SupplierName,
                            Business = "",
                            Products = new List<Product> { product },
                            Orders = new List<Order> { order },
                            Price = 0,
                            OrderDate = DateTime.Now,
                            BillingAddress = "",
                            ShippingAddress = "",
                            Confirmed = false
                        };

                        await _orderListRepository.CreateOrderList(newOrderList);                       

                    }
                    else
                    {
                        // Add product to existing list


                        // Check if product is already in list
                        if(!orderList4Supplier.Products.Contains(product))
                        {
                            // if not add to list
                            orderList4Supplier.Products.Add(product);
                        }


                        // Increase price of OrderList
                        orderList4Supplier.Price += product.Price;

                        // Increase quantity of units
                        orderList4Supplier.Products.Find(x => x.ID == product.ID).NumUnits += 1;

                        // Add order to OrderList
                        orderList4Supplier.Orders.Add(order);

                        await _orderListRepository.UpdateOrderList(orderList4Supplier);
                       


                    }

                }




    

               
                await _orderRepository.UpdateOrder(order);

                return RedirectToAction(nameof(Index));
            }
            return View("Index");        
        }
    }
}