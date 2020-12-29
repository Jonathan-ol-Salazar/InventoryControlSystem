using InventoryControlSystem.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Invoices
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IInvoiceContext _context;
        private readonly IMongoCollection<Invoice> _invoicesContext;

        public InvoiceRepository(IInvoiceContext context)
        {
            _context = context;
            _invoicesContext = _context.Invoices;
        }


        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            return await _invoicesContext.Find(Builders<Invoice>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateInvoices(Invoice )
        //{
        //    await _invoicesContext.InsertManyAsync(invoice);
        //}



        public async Task<Invoice> GetInvoice(string id)
        {
            FilterDefinition<Invoice> filter = Builders<Invoice>.Filter.Eq(x => x.Id, id);
            return await _invoicesContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateInvoice(Invoice invoice)
        {
            await _invoicesContext.InsertOneAsync(invoice);
        }

        public async Task<bool> UpdateInvoice(Invoice invoice)
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
