using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Roles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;



        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }


        // GET: Role
        public async Task<IActionResult> Index()
        {
            var x = await _roleRepository.GetAllRoles();
            return View(await _roleRepository.GetAllRoles());
        }

        // GET: Role/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return NotFound();

            }
            ViewData["Title"] = "View Role";

            return View(role);

        }

        // GET: Role/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New Role";

            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleRepository.CreateRole(role);
                role.ID = role.Id;
                await _roleRepository.UpdateRole(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit Role";

            return View();
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name")] Role role)
        {

            if (ModelState.IsValid)
            {
                var roleFromDb = await _roleRepository.GetRole(id);
                if (roleFromDb == null)
                {
                    return new NotFoundResult();
                }
                role.Id = roleFromDb.Id;
                await _roleRepository.UpdateRole(role);
                TempData["Message"] = "Customer Updated Successfully";

            }
            return RedirectToAction("Index");

        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete Role";

            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Role role = await _roleRepository.GetRole(id);

            await _roleRepository.DeleteRole(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
