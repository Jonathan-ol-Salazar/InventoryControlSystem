using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public interface IUserRepository
    {
        User GetUser(int ID);

        User CreateUser(User user);

        User DeleteUser(int ID);

        IEnumerable<User> GetAllUsers();

        User UpdateUser(User userChanges);

    }
}
