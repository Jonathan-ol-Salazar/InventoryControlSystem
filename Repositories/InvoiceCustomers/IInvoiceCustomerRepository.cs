using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.InvoiceCustomers
{
    public interface IInvoiceCustomerRepository
    {
        Task<InvoiceCustomer> GetInvoiceCustomer(string id);

        Task CreateInvoiceCustomer(InvoiceCustomer invoice);

        Task<bool> DeleteInvoiceCustomer(string id);

        Task<IEnumerable<InvoiceCustomer>> GetAllInvoiceCustomers();

        Task<bool> UpdateInvoiceCustomer(InvoiceCustomer invoiceChanges);
    }
}
