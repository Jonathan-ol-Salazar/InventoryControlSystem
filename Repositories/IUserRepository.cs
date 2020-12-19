using InventoryControlSystem.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem
{
    public interface IUserRepository
    {
        Task<User> GetUser(string id);

        Task CreateUser(User user);

        Task<bool> DeleteUser(string id);

        Task<IEnumerable<User>> GetAllUsers();

        Task<bool> UpdateUser(User userChanges);

    }
}
