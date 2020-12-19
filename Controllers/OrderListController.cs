using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.OrderLists;

namespace InventoryControlSystem.Controllers
{
    public class OrderListController : Controller
    {
        private readonly IOrderListRepository _orderListRepository;


        public OrderListController(IOrderListRepository orderListRepository)
        {
            _orderListRepository = orderListRepository;
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
        public async Task<IActionResult> Create([Bind("ID,Supplier,Business,Products,Orders,Price,OrderDate,BillingAddress,ShippingAddress,Confirmed")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                await _orderListRepository.CreateOrderList(orderList);

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
        public async Task<IActionResult> Edit(string id, [Bind("ID,Supplier,Business,Products,Orders,Price,OrderDate,BillingAddress,ShippingAddress,Confirmed")] OrderList orderList)
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
            ViewData["Title"] = "Delete OrderList";

            return View(orderList);
        }

        // POST: OrderList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            OrderList orderList = await _orderListRepository.GetOrderList(id);

            await _orderListRepository.DeleteOrderList(id);
            return RedirectToAction(nameof(Index));
        }

        //private async bool OrderListExists(string id)
        //{
        //    OrderList orderList = await _orderListRepository.GetOrderList(id);

        //    if


        //    return await _context.OrderLists.FindAsync(id);

        //}
    }
}
