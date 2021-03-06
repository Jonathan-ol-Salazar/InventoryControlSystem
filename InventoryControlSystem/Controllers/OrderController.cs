﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.ViewModels;
using InventoryControlSystem.Repositories.Orders;
using InventoryControlSystem.Repositories.OrderLists;
using InventoryControlSystem.Repositories.Customers;
using InventoryControlSystem.Repositories.Suppliers;
using InventoryControlSystem.Repositories.Funds;
using InventoryControlSystem.Repositories.Products;
using InventoryControlSystem.Repositories.InvoiceCustomers;
using System.Collections.Generic;
using System;
using System.Linq;
using InventoryControlSystem.Repositories.Businesses;

namespace InventoryControlSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderListRepository _orderListRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IFundRepository _fundRepository;
        private readonly IInvoiceCustomerRepository _invoiceCustomerRepository;
        private readonly IBusinessRepository _businessRepository;
        public OrderController(IOrderRepository orderRepository, IOrderListRepository orderListRepository, IProductRepository productRepository, 
            ICustomerRepository customerRepository, ISupplierRepository supplierRepository, IFundRepository fundRepository,
            IInvoiceCustomerRepository invoiceCustomer, IBusinessRepository businessRepository)
        {
            _orderRepository = orderRepository;
            _orderListRepository = orderListRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _supplierRepository = supplierRepository;
            _fundRepository = fundRepository;
            _invoiceCustomerRepository = invoiceCustomer;
            _businessRepository = businessRepository;
        }


        // GET: Order
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Table of Orders";

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

            List<Product> productList = new List<Product>();
            foreach(string product in order.ProductsID)
            {
                productList.Add(await _productRepository.GetProduct(product));
            }

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Products = productList,
                Customers = await _customerRepository.GetAllCustomers(),
                Order = order
            };
            ViewData["Title"] = "View Order";

            return View(orderViewModel);

        }

        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Products = await _productRepository.GetAllProducts(),                
                Customers = await _customerRepository.GetAllCustomers()
            };

            ViewData["Title"] = "Create New Order";

            return View(orderViewModel);
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductsID,CustomerID")] Order order)
        {
            if (ModelState.IsValid)
            {
                // ORDER PROPERTIES

                // Set number of products 
                order.NumProducts = order.ProductsID.Count;
                // Set status to incomplete
                order.Status = "INCOMPLETE";
                // Set orderdate to now
                order.OrderDate = DateTime.Now;
                // Set OrderListsID to new list
                order.OrderListsID = new List<string>();
                // Set TotalCost
                List<string> ProductsID = new List<string>();
                foreach(string productTotalCost in order.ProductsID)
                {
                    order.TotalCost += Convert.ToDouble(productTotalCost.Split(':')[1]);
                    ProductsID.Add(productTotalCost.Split(':')[0]);
                }
                order.ProductsID = ProductsID;

                // Update Funds
                IEnumerable <Fund> Funds = await _fundRepository.GetAllFunds();
                // Create new fund if none exists                
                if(Funds.ToList().Count == 0)
                {
                    Fund fund = new Fund
                    {
                        TotalRevenue = order.TotalCost,
                        TodaysRevenue = order.TotalCost,
                        TotalSales = order.TotalCost,
                        TodaysSales = order.TotalCost,
                        TotalProfit = order.TotalCost,
                        TodaysProfit = order.TotalCost,
                        TotalCost = 0,
                        TodaysCost = 0,
                        DateLastCalculated = DateTime.Now

                    };
                    await _fundRepository.CreateFund(fund);
                    fund.ID = fund.Id;
                    await _fundRepository.UpdateFund(fund);
                }
                else
                {
                    Fund fund = Funds.ToList()[0];
                    fund.TotalRevenue += order.TotalCost;
                    fund.TotalSales += order.TotalCost;
                    fund.TotalProfit += fund.TotalSales - fund.TotalCost;

                    // If new day reset
                    if(DateTime.Now > fund.DateLastCalculated)
                    {
                        fund.TodaysRevenue = order.TotalCost;
                        fund.TodaysSales = order.TotalCost;
                        fund.TodaysProfit = fund.TodaysSales - fund.TodaysCost;
                    }
                    else
                    {
                        fund.TodaysRevenue += order.TotalCost;
                        fund.TodaysSales += order.TotalCost;
                        fund.TodaysProfit += fund.TodaysSales - fund.TodaysCost;
                    }

                    await _fundRepository.UpdateFund(fund);

                }

                //Fund fund = await _fundRepository.GetFund(Funds);

                // Create order
                await _orderRepository.CreateOrder(order);
                // Set order ID
                order.ID = order.Id;
                await _orderRepository.UpdateOrder(order);

                // CUSTOMER PROPERTIES
                // Get customer
                Customer customer = await _customerRepository.GetCustomer(order.CustomerID);
                // Set order
                customer.Orders.Add(order.ID);
                // Update numOrders
                customer.NumOrders = customer.Orders.Count;
                // Update customer
                await _customerRepository.UpdateCustomer(customer);

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

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Products = await _productRepository.GetAllProducts(),
                Customers = await _customerRepository.GetAllCustomers(),
                Order = order
            };

            ViewData["Title"] = "Edit Order";

            return View(orderViewModel);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,ProductsID,Status,Fulfilled,Ordered,OrderList")] Order order)
        {
            if (ModelState.IsValid)
            {
                var orderFromDb = await _orderRepository.GetOrder(order.ID);
                if (orderFromDb == null)
                {
                    return new NotFoundResult();
                }
                
                order.Id = orderFromDb.Id;
                order.CustomerID = orderFromDb.CustomerID;
                order.NumProducts = order.ProductsID.Count;
                // Set TotalCost
                List<string> ProductsID = new List<string>();
                foreach (string productTotalCost in order.ProductsID)
                {
                    order.TotalCost += Convert.ToDouble(productTotalCost.Split(':')[1]);
                    ProductsID.Add(productTotalCost.Split(':')[0]);
                }
                order.ProductsID = ProductsID;


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
            List<Product> productList = new List<Product>();
            foreach (string product in order.ProductsID)
            {
                productList.Add(await _productRepository.GetProduct(product));

            }

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Products = productList,
                Customers = await _customerRepository.GetAllCustomers(),
                Order = order
            };
            ViewData["Title"] = "Delete Order";

            return View(orderViewModel);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Order order = await _orderRepository.GetOrder(id);

            // Remove OrderID from OrderLists
            foreach (string ID in order.OrderListsID)
            {
                OrderList orderList = await _orderListRepository.GetOrderList(ID);
                if (!orderList.Confirmed)
                {
                    // Remove ProductID from OrderList that's in the Order
                    var commonNumbers = order.ProductsID.Intersect(orderList.ProductsID);
                    orderList.ProductsID = orderList.ProductsID.Except(commonNumbers).ToList();

                    // Remove OrderID from OrderList
                    orderList.OrdersID.Remove(order.ID);

                    await _orderListRepository.UpdateOrderList(orderList);

                }
            }
            // Remove OrderID from Customer
            Customer customer = await _customerRepository.GetCustomer(order.CustomerID);
            customer.Orders.Remove(order.ID);
            await _customerRepository.UpdateCustomer(customer);

            await _orderRepository.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }

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

            Order order = await _orderRepository.GetOrder(id);

            // Get Business
            Business business = await _businessRepository.GetSelectedBusiness();


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
                    if(product == null)
                    {
                        order.ProductsID.Remove(productID);
                        break;
                    }

                    // Check if an existing orderlist for current product exists
                    var orderList4Supplier = await _orderListRepository.OrderListExist(product.SuppliersName);

                    // If no OrderList exists, create a new one. Otherwise, add to existing one
                    if (orderList4Supplier == null)
                    {
                        // Get Supplier
                        Supplier supplier = await _supplierRepository.GetSupplier(product.SuppliersID);
                       
                        OrderList newOrderList = new OrderList
                        {
                            SuppliersName = supplier.Name,
                            SuppliersID = supplier.ID,
                            SuppliersAddress = supplier.Address,
                            SuppliersEmail = supplier.Email,
                            SuppliersPhone = supplier.Phone,
                            BusinessesName = business.Name,
                            BusinessesID = business.ID,
                            BusinessesAddress = business.Address,
                            BusinessesEmail = business.Email,
                            BusinessesPhone = business.Phone,
                            ProductsID = new List<string> {product.ID},
                            OrdersID = new List<string> {order.ID},
                            TotalCost = product.TotalCost,
                            OrderDate = DateTime.Now,

                            Confirmed = false
                        };
                        // Create new OrderList and update ID to reflect new Id
                        await _orderListRepository.CreateOrderList(newOrderList);
                        newOrderList.ID = newOrderList.Id;
                        await _orderListRepository.UpdateOrderList(newOrderList);
                        
                        // Add OrderListID to Order
                        if(order.OrderListsID != null)
                        {
                            order.OrderListsID.Add(newOrderList.ID);
                        }
                        else
                        {
                            order.OrderListsID = new List<string>
                                {
                                    newOrderList.ID
                                };
                        }
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

                        // Check if OrderID is already in list
                        if (!orderList4Supplier.OrdersID.Contains(order.ID))
                        {
                            // if not add to list
                            orderList4Supplier.OrdersID.Add(order.ID);
                        }

                        // Check if OrderListID is already in list
                        if (!order.OrderListsID.Contains(orderList4Supplier.ID))
                        {
                            // if not add to list
                            order.OrderListsID.Add(orderList4Supplier.ID);
                        }


                        // Increase price of OrderList
                        orderList4Supplier.TotalCost += product.TotalCost;

                        // Increase quantity of units
                        product.NumUnits += 1;

                        // Update Product repo
                        await _productRepository.UpdateProduct(product);

                        //// Add Order to OrderList
                        //orderList4Supplier.OrdersID.Add(order.ID);

                        //// Add OrderList to Order
                        //order.OrderListsID.Add(orderList4Supplier.ID);

                        // Update OrderList repo
                        await _orderListRepository.UpdateOrderList(orderList4Supplier);

                      


                    }
                }

                // Creating InvoiceCustomer
                Customer customer = await _customerRepository.GetCustomer(order.CustomerID);
                InvoiceCustomer invoiceCustomer = new InvoiceCustomer
                {
                    OrderID = order.ID,
                    ReceiverName = customer.FirstName + " " + customer.LastName,
                    ReceiverEmail = customer.Email,
                    ReceiverAddress = customer.Address,
                    ReceiverPhone = customer.Phone,
                    SenderName = business.Name,
                    SenderAddress = business.Address,
                    SenderEmail = business.Email,
                    SenderPhone = business.Phone,
                    Date = order.OrderDate,
                    ProductsID = order.ProductsID,
                    TotalCost = order.TotalCost
                };

                await _invoiceCustomerRepository.CreateInvoiceCustomer(invoiceCustomer);
                invoiceCustomer.ID = invoiceCustomer.Id;
                await _invoiceCustomerRepository.UpdateInvoiceCustomer(invoiceCustomer);


                // Delete order if it has no products
                if (order.NumProducts == 0)
                {
                    RedirectToAction(nameof(DeleteConfirmed), order.ID);
                }
                else
                {
                    await _orderRepository.UpdateOrder(order);
                }


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
            order.Status = "COMPLETE";

            await _orderRepository.UpdateOrder(order);



            return RedirectToAction(nameof(Index));


        }

        // ToOrder
        public async Task<IActionResult> ToOrder()
        {
            // get all the entries that have 'ordered' as false
            var toOrder = await _orderRepository.ToOrder();
            ViewData["Title"] = "To Order";
            return View("Index", toOrder);
        }

        // ToFulfill
        public async Task<IActionResult> ToFulfill()
        {
            // get all the entries that have 'fulfilled' as false
            var toFulfill = await _orderRepository.ToFulfill();
            ViewData["Title"] = "To Filfill";
            return View("Index", toFulfill);
        }

    }
}