﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryControlSystem.Models;

namespace InventoryControlSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;


        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.GetAllUsers());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();

            }
            ViewData["Title"] = "View User";

            return View(user);

        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New User";

            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,Phone,Address,DOB")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.CreateUser(user);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
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

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,FirstName,LastName,Email,Phone,Address,DOB")] User user)
        {

            if (ModelState.IsValid)
            {
                var userFromDb = await _userRepository.GetUser(user.ID);
                if (userFromDb == null)
                {
                    return new NotFoundResult();
                }
                user.Id = userFromDb.Id;
                await _userRepository.UpdateUser(user);
                TempData["Message"] = "Customer Updated Successfully";

            }
            return RedirectToAction("Index");

        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
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
            ViewData["Title"] = "Delete User";

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            User user = await _userRepository.GetUser(id);

            await _userRepository.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        //private async bool UserExists(string id)
        //{
        //    User user = await _userRepository.GetUser(id);

        //    if


        //    return await _context.Users.FindAsync(id);

        //}
    }
}
