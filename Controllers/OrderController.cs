using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Orders;

namespace InventoryControlSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;


        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
    }
}
