using InventoryControlSystem.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IRoleContext _context;
        private readonly IMongoCollection<Role> _rolesContext;

        public RoleRepository(IRoleContext context)
        {
            _context = context;
            _rolesContext = _context.Roles;
        }


        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _rolesContext.Find(Builders<Role>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateRoles(Role )
        //{
        //    await _rolesContext.InsertManyAsync(role);
        //}



        public async Task<Role> GetRole(string id)
        {
            FilterDefinition<Role> filter = Builders<Role>.Filter.Eq(x => x.Id, id);
            return await _rolesContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateRole(Role role)
        {
            await _rolesContext.InsertOneAsync(role);
        }

        public async Task<bool> UpdateRole(Role role)
        {
            ReplaceOneResult updateResult = await _rolesContext.ReplaceOneAsync(filter: b => b.Id == role.Id, replacement: role);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteRole(string id)
        {
            //var ids = role.Select(d => d.Id);

            //var filter = Builders<Role>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("role_id", new BsonDocument("$in", new BsonArray(role)));

            //DeleteResult deleteResult = await _rolesContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _rolesContext.DeleteOneAsync(role => role.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }
    }
}
