using InventoryControlSystem.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Funds
{
    public class FundRepository : IFundRepository
    {
        private readonly IFundContext _context;
        private readonly IMongoCollection<Fund> _fundsContext;

        public FundRepository(IFundContext context)
        {
            _context = context;
            _fundsContext = _context.Funds;
        }


        public async Task<IEnumerable<Fund>> GetAllFunds()
        {
            return await _fundsContext.Find(Builders<Fund>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateFunds(Fund )
        //{
        //    await _fundsContext.InsertManyAsync(fund);
        //}



        public async Task<Fund> GetFund(string id)
        {
            FilterDefinition<Fund> filter = Builders<Fund>.Filter.Eq(x => x.Id, id);
            return await _fundsContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateFund(Fund fund)
        {
            await _fundsContext.InsertOneAsync(fund);
        }

        public async Task<bool> UpdateFund(Fund fund)
        {
            ReplaceOneResult updateResult = await _fundsContext.ReplaceOneAsync(filter: b => b.Id == fund.Id, replacement: fund);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteFund(string id)
        {
            //var ids = fund.Select(d => d.Id);

            //var filter = Builders<Fund>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("fund_id", new BsonDocument("$in", new BsonArray(fund)));

            //DeleteResult deleteResult = await _fundsContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _fundsContext.DeleteOneAsync(fund => fund.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

    }
}
