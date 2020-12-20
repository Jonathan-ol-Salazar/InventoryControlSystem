using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Customers;

namespace InventoryControlSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;


        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var x = await _customerRepository.GetAllCustomers();
            return View(await _customerRepository.GetAllCustomers());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();

            }
            ViewData["Title"] = "View Customer";

            return View(customer);

        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New Customer";

            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,Phone,Address,Orders")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.CreateCustomer(customer);
                customer.ID = customer.Id;
                await _customerRepository.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit Customer";

            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ID,FirstName,LastName,Email,Phone,Address,Orders")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                var customerFromDb = await _customerRepository.GetCustomer(id);
                if (customerFromDb == null)
                {
                    return new NotFoundResult();
                }
                customer.Id = customerFromDb.Id;
                await _customerRepository.UpdateCustomer(customer);
                TempData["Message"] = "Customer Updated Successfully";

            }
            return RedirectToAction("Index");

        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete Customer";

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Customer customer = await _customerRepository.GetCustomer(id);

            await _customerRepository.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }

        //private async bool CustomerExists(string id)
        //{
        //    Customer customer = await _customerRepository.GetCustomer(id);

        //    if


        //    return await _context.Customers.FindAsync(id);

        //}
    }
}
