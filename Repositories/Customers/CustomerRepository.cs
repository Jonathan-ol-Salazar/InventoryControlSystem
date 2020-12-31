using InventoryControlSystem.Repositories.Customers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerContext _context;
        private readonly IMongoCollection<Customer> _customersContext;

        public CustomerRepository(ICustomerContext context)
        {
            _context = context;
            _customersContext = _context.Customers;
        }


        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customersContext.Find(Builders<Customer>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateCustomers(Customer )
        //{
        //    await _customersContext.InsertManyAsync(customer);
        //}



        public async Task<Customer> GetCustomer(string id)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            return await _customersContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateCustomer(Customer customer)
        {
            await _customersContext.InsertOneAsync(customer);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            ReplaceOneResult updateResult = await _customersContext.ReplaceOneAsync(filter: b => b.Id == customer.Id, replacement: customer);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCustomer(string id)
        {
            //var ids = customer.Select(d => d.Id);

            //var filter = Builders<Customer>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("customer_id", new BsonDocument("$in", new BsonArray(customer)));

            //DeleteResult deleteResult = await _customersContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _customersContext.DeleteOneAsync(customer => customer.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }
    }
}