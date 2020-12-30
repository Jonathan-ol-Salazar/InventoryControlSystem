using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryControlSystem.Models;
using InventoryControlSystem.Repositories.Users;
using InventoryControlSystem.ViewModels;
using InventoryControlSystem.Repositories.Roles;
using RestSharp;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;

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



        public string getAccessToken()
        {
            var client = new RestClient("https://bottleo-ics.au.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"RcxHp3J27FtuIk5yTjz1ly7MmVcCwWLE\",\"client_secret\":\"AsOHBk522QRzh2tW9VfcoTncQVceJlnyXrhrGPAqqBQpVLB4DQK6S30CF5mMiVKJ\",\"audience\":\"https://bottleo-ics.au.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            // Parsing into JSON 
            var response2dict = JObject.Parse(response.Content);
            // Retrieving Access Token
            var Auth0ManagementAPI_AccessToken = response2dict.First.First.ToString();

            return Auth0ManagementAPI_AccessToken;
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
        public async Task<IActionResult> Create()
        {

            UserViewModel userViewModel = new UserViewModel
            {
                Roles = await _roleRepository.GetAllRoles()
            };

            ViewData["Title"] = "Create New User";

            return View(userViewModel);
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,Phone,Address,DOB,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.CreateUser(user);
                user.ID = user.Id;

                // Update Auth0 with new user
                string accessToken = getAccessToken();

                // Creating JSON 
                string payload = @"{""email"":""" + user.Email + @""",
	                ""blocked"":false,
	                ""email_verified"":false,
	                ""app_metadata"":{
                        ""roles"":[                
                            """ + user.Role + @"""
		                ]
	                },
                    ""name"":" + user.FirstName + " " + user.LastName + @""",
	                ""connection"":""Username-Password-Authentication"",
	                ""password"":""Password123321"",
	                ""verify_email"":false
                }";

                var client = new RestClient("https://bottleo-ics.au.auth0.com/api/v2/users");
                var request = new RestRequest(Method.POST);
                request.AddHeader("authorization", "Bearer " + accessToken);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", payload, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                user.Auth0ID = JObject.Parse(response.Content).GetValue("user_id").ToString();
                user.Picture = JObject.Parse(response.Content).GetValue("picture").ToString();

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
        public async Task<IActionResult> Edit(string id, [Bind("ID,FirstName,LastName,Email,Phone,Address,DOB,Role,Auth0ID")] User user)
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

                // Update User Role in Auth0                
                string accessToken = getAccessToken();

                var client = new RestClient("https://bottleo-ics.au.auth0.com/api/v2/users/" + user.Auth0ID);
                var request = new RestRequest(Method.PATCH);
                request.AddHeader("authorization", "Bearer " + accessToken);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"name\": \"" + user.FirstName + " " + user.LastName + "\",\"app_metadata\": {\"roles\": [\"" + user.Role + "\"]}}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

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


            // Deleting User from Auth0
            string accessToken = getAccessToken();
            var client = new RestClient("https://bottleo-ics.au.auth0.com/api/v2/users/" + user.Auth0ID);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("authorization", "Bearer " + accessToken);
            request.AddHeader("content-type", "application/json");

            IRestResponse response = client.Execute(request);

            await _userRepository.DeleteUser(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
