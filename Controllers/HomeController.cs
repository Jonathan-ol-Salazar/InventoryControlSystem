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
        public HomeController(IOrderRepository orderRepository, IOrderListRepository orderListRepository, IProductRepository productRepository, ICustomerRepository customerRepository, ISupplierRepository supplierRepository, IFundRepository fundRepository)
        {
            _orderRepository = orderRepository;
            _orderListRepository = orderListRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _supplierRepository = supplierRepository;
            _fundRepository = fundRepository;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Order> order = await _orderRepository.ToOrderList();
            IEnumerable<OrderList> orderList = await _orderListRepository.ToConfirm();
            IEnumerable<Fund> funds = await _fundRepository.GetAllFunds();
            Fund fund = funds.ToList()[0];

            HomeViewModel homeViewModel = new HomeViewModel
            {
                Orders = order,
                OrderLists = orderList,
                Fund = fund
            };
            return View(homeViewModel);
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
