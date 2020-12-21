using InventoryControlSystem.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.OrderLists
{
    public class OrderListRepository : IOrderListRepository
    {
        private readonly IOrderListContext _context;
        private readonly IMongoCollection<OrderList> _orderListsContext;

        public OrderListRepository(IOrderListContext context)
        {
            _context = context;
            _orderListsContext = _context.OrderLists;
        }


        public async Task<IEnumerable<OrderList>> GetAllOrderLists()
        {
            return await _orderListsContext.Find(Builders<OrderList>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateOrderLists(OrderList )
        //{
        //    await _orderListsContext.InsertManyAsync(orderList);
        //}



        public async Task<OrderList> GetOrderList(string id)
        {
            FilterDefinition<OrderList> filter = Builders<OrderList>.Filter.Eq(x => x.Id, id);
            return await _orderListsContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateOrderList(OrderList orderList)
        {
            await _orderListsContext.InsertOneAsync(orderList);
        }

        public async Task<bool> UpdateOrderList(OrderList orderList)
        {
            ReplaceOneResult updateResult = await _orderListsContext.ReplaceOneAsync(filter: b => b.Id == orderList.Id, replacement: orderList);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteOrderList(string id)
        {
            //var ids = orderList.Select(d => d.Id);

            //var filter = Builders<OrderList>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("orderList_id", new BsonDocument("$in", new BsonArray(orderList)));

            //DeleteResult deleteResult = await _orderListsContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _orderListsContext.DeleteOneAsync(orderList => orderList.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        public async Task<OrderList> OrderListExist(string suppliersName)
        {
            FilterDefinition<OrderList> filter = Builders<OrderList>.Filter.Eq(x => x.SuppliersName, suppliersName);
            return await _orderListsContext.Find(filter).FirstOrDefaultAsync();
        }
    }
}
