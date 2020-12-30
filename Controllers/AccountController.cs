using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Users;
using InventoryControlSystem.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Logout URLs** settings for the app.
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User user = await _userRepository.GetUserAuth0(User.Claims.ToList()[7].Value);

            AccountViewModel accountViewModel = new AccountViewModel
            {
                User = user
            };

            return View(accountViewModel);
        }

        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit User";

            return View(user);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,FirstName,LastName,Email,Phone,Address,Address,DOB,Auth0ID,Role")] User user)
        {

            if (ModelState.IsValid)
            {
                var userFromDb = await _userRepository.GetUser(id);
                if (userFromDb == null)
                {
                    return new NotFoundResult();
                }
                user.Id = userFromDb.Id;
                await _userRepository.UpdateUser(user);
                TempData["Message"] = "User Updated Successfully";

            }
            return RedirectToAction("Index");

        }

    }
}
