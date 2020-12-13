using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }


        public User CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public User DeleteUser(int ID)
        {
            User user = context.Users.Find(ID);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users;
        }

        public User GetUser(int ID)
        {
            return context.Users.Find(ID);
        }

        public User UpdateUser(User userChanges)
        {
            var user = context.Users.Attach(userChanges);

            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();


            return userChanges;
        }
    }
}
