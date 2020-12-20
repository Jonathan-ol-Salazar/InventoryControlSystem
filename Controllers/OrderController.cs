using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Orders;
using InventoryControlSystem.Repositories.OrderLists;
using System.Collections.Generic;
using System;
using InventoryControlSystem.Repositories.Products;
using InventoryControlSystem.ViewModels;
using System.Linq;
using InventoryControlSystem.Repositories.Customers;

namespace InventoryControlSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderListRepository _orderListRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderController(IOrderRepository orderRepository, IOrderListRepository orderListRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _orderListRepository = orderListRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
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
        public async Task<IActionResult> CreateAsync()
        {
            OrderCreateViewModel orderCreateViewModel = new OrderCreateViewModel()
            {
                Products = await _productRepository.GetAllProducts(),                
                Customers = await _customerRepository.GetAllCustomers()
            };

            ViewData["Title"] = "Create New Order";

            return View(orderCreateViewModel);
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OrderID,ProductsID,Customer")] Order order)
        {
            if (ModelState.IsValid)
            {
                // Set number of products 
                order.NumProducts = order.ProductsID.Count();
                // Set status to incomplete
                order.Status = "INCOMPLETE";
                // Set orderdate to now
                order.OrderDate = DateTime.Now;          

                // Create order
                await _orderRepository.CreateOrder(order);

                order.ID = order.Id;
                await _orderRepository.UpdateOrder(order);

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
        public async Task<IActionResult> Edit(string id, [Bind("ID,OrderID,NumProducts,ProductsID,Customer,Status,Fulfilled,Ordered,OrderList,OrderDate")] Order order)
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

                // Add each product from order to an orderlist
                foreach (string productID in order.ProductsID)
                {
                    Product product = await _productRepository.GetProduct(productID);
                    // Check if an existing orderlist for current product exists
                    var orderList4Supplier = await _orderListRepository.OrderListExist(product.SupplierName);

                    // If no OrderList exists, create a new one. Otherwise, add to existing one
                    if (orderList4Supplier == null)
                    {

                        OrderList newOrderList = new OrderList
                        {
                            SupplierName = product.SupplierName,
                            Business = "",
                            ProductsID = new List<string> {product.ID},
                            OrdersID = new List<string> {order.ID},
                            Price = 0,
                            OrderDate = DateTime.Now,
                            BillingAddress = "",
                            ShippingAddress = "",
                            Confirmed = false
                        };
                        // Create new OrderList and update ID to reflect new Id
                        await _orderListRepository.CreateOrderList(newOrderList);
                        newOrderList.ID = newOrderList.Id;
                        await _orderListRepository.UpdateOrderList(newOrderList);

                    }
                    else
                    {
                        // Add product to existing list


                        // Check if product is already in list
                        if(!orderList4Supplier.ProductsID.Contains(product.ID))
                        {
                            // if not add to list
                            orderList4Supplier.ProductsID.Add(product.ID);
                        }


                        // Increase price of OrderList
                        orderList4Supplier.Price += product.Price;

                        // Increase quantity of units
                        product.NumUnits += 1;
                        await _productRepository.UpdateProduct(product);

                        // Add order to OrderList
                        orderList4Supplier.OrdersID.Add(order.ID);

                        await _orderListRepository.UpdateOrderList(orderList4Supplier);
                       


                    }
                }               
                await _orderRepository.UpdateOrder(order);

                return RedirectToAction(nameof(Index));
            }
            return View("Index");        
        }

        public async Task<IActionResult> Fulfill(string id)
        {
            // Get Order
            // Set 'Fulfilled' to true

            Order order = await _orderRepository.GetOrder(id);

            order.Fulfilled = true;

            await _orderRepository.UpdateOrder(order);



            return RedirectToAction(nameof(Index));


        }

    }
}