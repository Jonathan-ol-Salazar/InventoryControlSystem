using InventoryControlSystem.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Businesses
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly IBusinessContext _context;
        private readonly IMongoCollection<Business> _businesssContext;

        public BusinessRepository(IBusinessContext context)
        {
            _context = context;
            _businesssContext = _context.Businesses;
        }


        public async Task<IEnumerable<Business>> GetAllBusinesss()
        {
            return await _businesssContext.Find(Builders<Business>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateBusinesss(Business )
        //{
        //    await _businesssContext.InsertManyAsync(business);
        //}



        public async Task<Business> GetBusiness(string id)
        {
            FilterDefinition<Business> filter = Builders<Business>.Filter.Eq(x => x.Id, id);
            return await _businesssContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateBusiness(Business business)
        {
            await _businesssContext.InsertOneAsync(business);
        }

        public async Task<bool> UpdateBusiness(Business business)
        {
            ReplaceOneResult updateResult = await _businesssContext.ReplaceOneAsync(filter: b => b.Id == business.Id, replacement: business);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteBusiness(string id)
        {
            //var ids = business.Select(d => d.Id);

            //var filter = Builders<Business>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("business_id", new BsonDocument("$in", new BsonArray(business)));

            //DeleteResult deleteResult = await _businesssContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _businesssContext.DeleteOneAsync(business => business.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }
    }
}
