using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.OrderLists;
using InventoryControlSystem.Repositories.Orders;
using InventoryControlSystem.ViewModels;
using System.Collections.Generic;
using InventoryControlSystem.Repositories.Funds;
using System.Linq;
using System;

namespace InventoryControlSystem.Controllers
{
    public class OrderListController : Controller
    {
        private readonly IOrderListRepository _orderListRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IFundRepository _fundRepository;



        public OrderListController(IOrderListRepository orderListRepository, IOrderRepository orderRepository, IFundRepository fundRepository)
        {
            _orderListRepository = orderListRepository;
            _orderRepository = orderRepository;
            _fundRepository = fundRepository;

        }


        // GET: OrderList
        public async Task<IActionResult> Index()
        {
            var x = await _orderListRepository.GetAllOrderLists();
            return View(await _orderListRepository.GetAllOrderLists());
        }

        // GET: OrderList/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var orderList = await _orderListRepository.GetOrderList(id);
            if (orderList == null)
            {
                return NotFound();

            }
            ViewData["Title"] = "View OrderList";

            return View(orderList);

        }

        // GET: OrderList/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New OrderList";

            return View();
        }

        // POST: OrderList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Supplier,Business,Products,Orders,Price,OrderDate,SuppliersAddress,ShippingAddress,Confirmed")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                await _orderListRepository.CreateOrderList(orderList);
                orderList.ID = orderList.Id;
                await _orderListRepository.UpdateOrderList(orderList);
                return RedirectToAction(nameof(Index));
            }
           
            return View(orderList);
        }

        // GET: OrderList/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderList orderList = await _orderListRepository.GetOrderList(id);
            if (orderList == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit OrderList";

            return View(orderList);
        }

        // POST: OrderList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Supplier,Business,Products,Orders,Price,OrderDate,SuppliersAddress,ShippingAddress,Confirmed")] OrderList orderList)
        {

            if (ModelState.IsValid)
            {
                var orderListFromDb = await _orderListRepository.GetOrderList(id);
                if (orderListFromDb == null)
                {
                    return new NotFoundResult();
                }
                orderList.Id = orderListFromDb.Id;
                await _orderListRepository.UpdateOrderList(orderList);
                TempData["Message"] = "Customer Updated Successfully";

            }
            return RedirectToAction("Index");

        }

        // GET: OrderList/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderList orderList = await _orderListRepository.GetOrderList(id);
            if (orderList == null)
            {
                return NotFound();
            }

            List<Order> orders = new List<Order>();
            foreach(string ID in orderList.OrdersID)
            {
                orders.Add(await _orderRepository.GetOrder(ID));
            }

            OrderListViewModel orderListViewModel = new OrderListViewModel
            {
                Orders = orders,
                OrderList = orderList

            };

            ViewData["Title"] = "Delete OrderList";

            return View(orderListViewModel);
        }

        // POST: OrderList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            OrderList orderList = await _orderListRepository.GetOrderList(id);

            if(orderList.Confirmed == false)
            {
                // Remove OrderList from each of its Orders
                foreach (string ID in orderList.OrdersID)
                {
                    Order order = await _orderRepository.GetOrder(ID);
                    order.OrderListsID.Remove(id);
                    order.OrderList = false;
                    await _orderRepository.UpdateOrder(order);
                }
            }
      


            await _orderListRepository.DeleteOrderList(id);



            return RedirectToAction(nameof(Index));
        }

        //private async bool OrderListExists(string id)
        //{
        //    OrderList orderList = await _orderListRepository.GetOrderList(id);

        //    if


        //    return await _context.OrderLists.FindAsync(id);

        //}

        public async Task<IActionResult> Confirm(string id)
        {
            // Get OrderList
            // Set to 'Confirmed'
            // Loop through all its order and set it to 'Ordered'

            if (id == null)
            {
                return NotFound();
            }

            OrderList orderList = await _orderListRepository.GetOrderList(id);

            if (orderList == null)
            {
                return NotFound();
            }

            // Set OrderList 'Confirmed' to true
            orderList.Confirmed = true;

            // Set its orders to 'Ordered'
            foreach (string orderID in orderList.OrdersID)
            {
                Order order = await _orderRepository.GetOrder(orderID);

                order.Ordered = true;
                await _orderRepository.UpdateOrder(order);

            }

            await _orderListRepository.UpdateOrderList(orderList);

            // Decreasing Fund
            IEnumerable<Fund> Funds = await _fundRepository.GetAllFunds();
            // Get Fund
            Fund fund = Funds.ToList()[0];
            
            fund.TotalCost += orderList.Price;
            fund.TotalProfit = fund.TotalSales - fund.TotalCost;

            if (DateTime.Now.Date > fund.DateLastCalculated.Date)
            {
                fund.TodaysCost = orderList.Price;
            }
            else
            {
                fund.TodaysCost += orderList.Price;
            }
            fund.TodaysProfit = (fund.TodaysSales - fund.TodaysCost);


            await _fundRepository.UpdateFund(fund);

            return RedirectToAction(nameof(Index));


        }

    }
}
