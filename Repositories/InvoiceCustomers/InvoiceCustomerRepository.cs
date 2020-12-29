using InventoryControlSystem.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Invoices
{
    public class InvoiceCustomerRepository : IInvoiceCustomerRepository
    {
        private readonly IInvoiceCustomerContext _context;
        private readonly IMongoCollection<InvoiceCustomer> _invoicesContext;

        public InvoiceCustomerRepository(IInvoiceCustomerContext context)
        {
            _context = context;
            _invoicesContext = _context.InvoiceCustomers;
        }


        public async Task<IEnumerable<InvoiceCustomer>> GetAllInvoices()
        {
            return await _invoicesContext.Find(Builders<InvoiceCustomer>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateInvoices(Invoice )
        //{
        //    await _invoicesContext.InsertManyAsync(invoice);
        //}



        public async Task<InvoiceCustomer> GetInvoice(string id)
        {
            FilterDefinition<InvoiceCustomer> filter = Builders<InvoiceCustomer>.Filter.Eq(x => x.Id, id);
            return await _invoicesContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateInvoice(InvoiceCustomer invoice)
        {
            await _invoicesContext.InsertOneAsync(invoice);
        }

        public async Task<bool> UpdateInvoice(InvoiceCustomer invoice)
        {
            ReplaceOneResult updateResult = await _invoicesContext.ReplaceOneAsync(filter: b => b.Id == invoice.Id, replacement: invoice);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteInvoice(string id)
        {
            //var ids = invoice.Select(d => d.Id);

            //var filter = Builders<Invoice>.Filter.In(d => d.Id, ids);

            ////var filter = new BsonDocument("invoice_id", new BsonDocument("$in", new BsonArray(invoice)));

            //DeleteResult deleteResult = await _invoicesContext.DeleteManyAsync(filter);

            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


            DeleteResult deleteResult = await _invoicesContext.DeleteOneAsync(invoice => invoice.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }
    }
}
