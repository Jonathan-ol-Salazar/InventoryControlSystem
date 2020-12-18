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
        Task<User> GetUser(int ID);

        Task CreateUser(User user);

        Task<bool> DeleteUser(ObjectId id);

        Task<IEnumerable<User>> GetAllUsers();

        Task<bool> UpdateUser(User userChanges);

    }
}
