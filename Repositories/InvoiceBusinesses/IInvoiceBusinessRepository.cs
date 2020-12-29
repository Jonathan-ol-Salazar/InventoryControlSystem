using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.InvoiceBusinesses
{
    public interface IInvoiceBusinessRepository
    {
        Task<InvoiceBusiness> GetInvoice(string id);

        Task CreateInvoice(InvoiceBusiness invoice);

        Task<bool> DeleteInvoice(string id);

        Task<IEnumerable<InvoiceBusiness>> GetAllInvoices();

        Task<bool> UpdateInvoice(InvoiceBusiness invoiceChanges);
    }
}
