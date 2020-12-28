using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Users;
using InventoryControlSystem.ViewModels;
using InventoryControlSystem.Repositories.Roles;

namespace InventoryControlSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;



        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }


        // GET: User
        public async Task<IActionResult> Index()
        {
            var x = await _userRepository.GetAllUsers();
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
                user.ID = user.Id;
                await _userRepository.UpdateUser(user);
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

            UserViewModel userViewModel = new UserViewModel
            {
                User = user,
                Roles = await _roleRepository.GetAllRoles()
            };

            ViewData["Title"] = "Edit User";

            return View(userViewModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,FirstName,LastName,Email,Phone,Address,DOB,Role")] User user)
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
