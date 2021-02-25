using InventoryControlSystem.Controllers;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Businesses;
using InventoryControlSystem.Repositories.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControlSystemTests.Controllers
{
    [TestClass]
    public class BusinessControllerTests
    {
        Business business;
        List<Business> businesses;
        Mock<IBusinessRepository> businessRepo;
        Mock<IOrderRepository> orderRepo;
        BusinessController controller;

        [TestInitialize]
        public void SetUp()
        {
            // Arrange
            business = new Business
            {
                ID = "",
                Id = "",
                Name = "",
                Email = "",
                Address = "",
                Phone = 000,
                Selected = false
            };

            businesses = new List<Business>
            {
                business
            };
            businessRepo = new Mock<IBusinessRepository>();
            businessRepo.Setup(r => r.GetAllBusinesss()).ReturnsAsync(businesses);
            orderRepo = new Mock<IOrderRepository>();

            controller = new BusinessController(businessRepo.Object, orderRepo.Object); ;

        }

        [TestMethod]
        public async Task Index_WhenCalled_ReturnModel()
        {      
            // Act
            var result = await controller.Index() as ViewResult;
            var model = result.Model;

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(businesses,(System.Collections.ICollection)model);
        }

        [TestMethod]
        public async Task Index_WhenCalled_ReturnViewResult()
        {
            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public async Task Details_WhenCalledWithExistingID_ReturnBusiness()
        {
            // Arrange
            businessRepo.Setup(r => r.GetBusiness(business.Id)).ReturnsAsync(business);

            controller = new BusinessController(businessRepo.Object, orderRepo.Object);

            // Act
            var result = await controller.Details(business.Id) as ViewResult;
            var model = result.Model;

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(business, model);
        }

        [DataTestMethod]
        [DataRow("9")]
        public async Task Details_WhenCalledWithNonExistingID_ReturnNotFound(string id)
        {
            // Arrange
            businessRepo.Setup(r => r.GetBusiness(id));
            controller = new BusinessController(businessRepo.Object,orderRepo.Object);

            // Act
            var result = await controller.Details(id);
            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
