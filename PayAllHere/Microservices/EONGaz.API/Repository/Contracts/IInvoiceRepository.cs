using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EONGaz.API.Models;

namespace EONGaz.API.Repository.Contracts
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetInvoiceById(string id);

        Task<List<Invoice>> GetInvoicesByCNP(string CNP);

        Task AddInvoice(Invoice invoice);

        Task<double> PayInvoice(string id, double value);

        Task AddReceipt(string invoiceId, Receipt receipt);
    }
}
