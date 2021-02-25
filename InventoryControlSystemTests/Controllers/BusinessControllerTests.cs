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
        [TestMethod]
        public async Task Index_WhenCalled_ReturnAllBusinesses()
        {
            // Arrange
            var businesses = new List<Business>
            {
                new Business{
                    ID = "",
                    Id = "",
                    Name = "",
                    Email = "",
                    Address = "",
                    Phone = 000,
                    Selected = false
                }
            };
            var businessRepo = new Mock<IBusinessRepository>();
            businessRepo.Setup(r => r.GetAllBusinesss()).ReturnsAsync(businesses);
            var orderRepo = new Mock<IOrderRepository>();
            
            var controller = new BusinessController(businessRepo.Object, orderRepo.Object); ;

            // Act
            var result = await controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
