using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.InvoiceBusinesses
{
    public interface IInvoiceBusinessRepository
    {
        Task<InvoiceBusiness> GetInvoiceBusiness(string id);

        Task CreateInvoiceBusiness(InvoiceBusiness invoice);

        Task<bool> DeleteInvoiceBusiness(string id);

        Task<IEnumerable<InvoiceBusiness>> GetAllInvoiceBusinesses();

        Task<bool> UpdateInvoiceBusiness(InvoiceBusiness invoiceChanges);
    }
}
