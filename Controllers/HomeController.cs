using InventoryControlSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using InventoryControlSystem.Models;
using InventoryControlSystem.ViewModels;
using InventoryControlSystem.Repositories.Orders;
using InventoryControlSystem.Repositories.OrderLists;
using InventoryControlSystem.Repositories.Customers;
using InventoryControlSystem.Repositories.Suppliers;
using InventoryControlSystem.Repositories.Funds;
using InventoryControlSystem.Repositories.Products;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using InventoryControlSystem.Repositories.Users;

namespace InventoryControlSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderListRepository _orderListRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IFundRepository _fundRepository;
        private readonly IUserRepository _userRepository;
        public HomeController(IOrderRepository orderRepository, IOrderListRepository orderListRepository, IProductRepository productRepository, 
            ICustomerRepository customerRepository, ISupplierRepository supplierRepository, IFundRepository fundRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderListRepository = orderListRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _supplierRepository = supplierRepository;
            _fundRepository = fundRepository;
            _userRepository = userRepository;
        }


        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated == true)
            {
                // Creating View Model

                IEnumerable<Order> order = new List<Order>();
                if (User.IsInRole("Team Member"))
                {
                    order = await _orderRepository.ToFulfill();
                }
                else
                {
                    order = await _orderRepository.ToOrderList();
                }

                IEnumerable<OrderList> orderList = await _orderListRepository.ToConfirm();
                IEnumerable<Fund> funds = await _fundRepository.GetAllFunds();
                Fund fund = funds.ToList()[0];

                HomeViewModel homeViewModel = new HomeViewModel
                {
                    Orders = order,
                    OrderLists = orderList,
                    Fund = fund
                };



                // New Users
                string Auth0ID = User.Claims.ToList()[7].Value;

                if (await _userRepository.Auth0IDExists(Auth0ID) == false)
                {
                    User user = new User
                    {
                        Auth0ID = Auth0ID,
                        Email = User.Claims.ToList()[5].Value,
                        Role = User.Claims.ToList()[0].Value
                    };

                    await _userRepository.CreateUser(user);
                    user.ID = user.Id;
                    await _userRepository.UpdateUser(user);
                }
                return View(homeViewModel);

            }
            return View("~/Views/Shared/Blank.cshtml" );

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
