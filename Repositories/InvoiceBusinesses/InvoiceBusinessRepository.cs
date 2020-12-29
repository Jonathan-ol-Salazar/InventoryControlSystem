using InventoryControlSystem.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.InvoiceBusinesses
{
    public class InvoiceBusinessRepository : IInvoiceBusinessRepository
    {
        private readonly IInvoiceBusinessContext _context;
        private readonly IMongoCollection<InvoiceBusiness> _invoicesContext;

        public InvoiceBusinessRepository(IInvoiceBusinessContext context)
        {
            _context = context;
            _invoicesContext = _context.InvoiceBusinesses;
        }


        public async Task<IEnumerable<InvoiceBusiness>> GetAllInvoices()
        {
            return await _invoicesContext.Find(Builders<InvoiceBusiness>.Filter.Empty).ToListAsync();
        }


        //public async Task CreateInvoices(Invoice )
        //{
        //    await _invoicesContext.InsertManyAsync(invoice);
        //}



        public async Task<InvoiceBusiness> GetInvoice(string id)
        {
            FilterDefinition<InvoiceBusiness> filter = Builders<InvoiceBusiness>.Filter.Eq(x => x.Id, id);
            return await _invoicesContext.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateInvoice(InvoiceBusiness invoice)
        {
            await _invoicesContext.InsertOneAsync(invoice);
        }

        public async Task<bool> UpdateInvoice(InvoiceBusiness invoice)
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
