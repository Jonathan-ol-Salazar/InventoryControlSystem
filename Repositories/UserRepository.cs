using InventoryControlSystem.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext _context;
        private readonly IMongoCollection<User> _usersContext;

        public UserRepository(IUserContext context)
        {
            _context = context;
            _usersContext = _context.Users;
        }


        public async Task<IEnumerable<User>> GetAllUsers()
        {

            return await _usersContext.Find(Builders<User>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateUsers(User )
        //{
        //    await _usersContext.InsertManyAsync(user);
        //}



        public async Task<User> GetUser(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(x => x.Id, id);
            return await _usersContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUser(User user)
        {
            await _usersContext.InsertOneAsync(user);
        }

        public async Task<bool> UpdateUser(User user)
        {
            ReplaceOneResult updateResult = await _usersContext.ReplaceOneAsync(filter: b => b.Id == user.Id, replacement: user);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteUser(string id)
        {
            //var ids = user.Select(d => d.Id);

            //var filter = Builders<User>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("user_id", new BsonDocument("$in", new BsonArray(user)));

            //DeleteResult deleteResult = await _usersContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _usersContext.DeleteOneAsync(user => user.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }
    }
}
