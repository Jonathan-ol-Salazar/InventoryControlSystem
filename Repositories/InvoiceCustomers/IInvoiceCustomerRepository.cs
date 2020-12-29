using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.InvoiceCustomers
{
    public interface IInvoiceCustomerRepository
    {
        Task<InvoiceCustomer> GetInvoice(string id);

        Task CreateInvoice(InvoiceCustomer invoice);

        Task<bool> DeleteInvoice(string id);

        Task<IEnumerable<InvoiceCustomer>> GetAllInvoices();

        Task<bool> UpdateInvoice(InvoiceCustomer invoiceChanges);
    }
}
