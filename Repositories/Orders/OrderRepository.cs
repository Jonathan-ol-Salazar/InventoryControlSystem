using InventoryControlSystem.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderContext _context;
        private readonly IMongoCollection<Order> _ordersContext;

        public OrderRepository(IOrderContext context)
        {
            _context = context;
            _ordersContext = _context.Orders;
        }


        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _ordersContext.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateOrders(Order )
        //{
        //    await _ordersContext.InsertManyAsync(order);
        //}



        public async Task<Order> GetOrder(string id)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(x => x.Id, id);
            return await _ordersContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateOrder(Order order)
        {
            await _ordersContext.InsertOneAsync(order);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            ReplaceOneResult updateResult = await _ordersContext.ReplaceOneAsync(filter: b => b.Id == order.Id, replacement: order);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteOrder(string id)
        {
            //var ids = order.Select(d => d.Id);

            //var filter = Builders<Order>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("order_id", new BsonDocument("$in", new BsonArray(order)));

            //DeleteResult deleteResult = await _ordersContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _ordersContext.DeleteOneAsync(order => order.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        public async Task<IEnumerable<Order>> ToOrder()
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Where(x => x.Ordered == false);
            return await _ordersContext.Find(filter).ToListAsync();
        }
        public async Task<IEnumerable<Order>> ToFulfill()
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Where(x => x.Fulfilled == false);
            return await _ordersContext.Find(filter).ToListAsync();
        }
    }
}
