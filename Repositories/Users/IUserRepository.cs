using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User> GetUser(string id);

        Task CreateUser(User user);

        Task<bool> DeleteUser(string id);

        Task<IEnumerable<User>> GetAllUsers();

        Task<bool> UpdateUser(User userChanges);

        Task<bool> Auth0IDExists(string Auth0ID);

        Task<User> GetUserAuth0(string Auth0ID);

    }
}
