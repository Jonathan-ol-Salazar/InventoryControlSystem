using InventoryControlSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Invoices
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetInvoice(string id);

        Task CreateInvoice(Invoice invoice);

        Task<bool> DeleteInvoice(string id);

        Task<IEnumerable<Invoice>> GetAllInvoices();

        Task<bool> UpdateInvoice(Invoice invoiceChanges);
    }
}
