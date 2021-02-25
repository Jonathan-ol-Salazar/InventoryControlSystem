using InventoryControlSystem.Controllers;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Users;
using InventoryControlSystem.ViewModels;
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
    public class AccountControllerTests
    {
        User user;
        Mock<IUserRepository> repo;
        AccountController controller;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            user = new User
            {
                FirstName = "",
                LastName = "",
                Address = "",
                DOB = "",
                Email = "f",
                Phone = "",
                Picture = "",
                Role = "",
                Auth0ID = "",
                ID = "",
                Id = ""
            };
            repo = new Mock<IUserRepository>();
            repo.Setup(r => r.GetUser(user.ID)).ReturnsAsync(user);

            controller = new AccountController(repo.Object);
        }
        //[TestMethod]
        //public async Task Index_WhenCalled_ReturnViewModel()
        //{
        //    // Act
        //    var result = await controller.Index() as ViewResult;
        //    var model = result.Model as UserViewModel;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(user, model);
        //}

        //[TestMethod]
        //public  void Index_WhenCalled_ReturnView()
        //{
        //    // Act
        //    var result =  controller.Index();

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));

        //}

        [TestMethod]
        public void yeet()
        {
            Assert.AreEqual(1, 1);
        }
    }
}

