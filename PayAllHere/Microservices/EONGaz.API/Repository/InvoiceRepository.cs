using EONGaz.API.Models;
using EONGaz.API.Repository.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EONGaz.API.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IMongoCollection<Invoice> _collection;

        public InvoiceRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var mongoDatabase = client.GetDatabase("DB_EONGazService");

            _collection = mongoDatabase.GetCollection<Invoice>("Invoices");
        }

        public async Task<Invoice> GetInvoiceById(string id)
        {
            return await (await _collection.FindAsync(x => x.InvoiceId == id)).FirstOrDefaultAsync();
        }

        public async Task<List<Invoice>> GetInvoicesByCNP(string CNP)
        {
            return (await _collection.FindAsync(x => x.UserCNP == CNP)).ToList().OrderByDescending(x => x.EndDate).ToList();
        }

        public async Task AddInvoice(Invoice invoice)
        {
            await _collection.InsertOneAsync(invoice);
        }

        public async Task<double> PayInvoice(string id, double value)
        {
            var invoice = await _collection.Find(x => x.InvoiceId == id).FirstOrDefaultAsync();

            if (0 <= value)
            {
                invoice.PayedValue += value;
            }
            else
            {
                throw new Exception();
            }
            await _collection.ReplaceOneAsync(x => x.InvoiceId == id, invoice
            );

            return invoice.Value - invoice.PayedValue;
        }

        public async Task AddReceipt(string invoiceId, Receipt receipt)
        {
            var invoice = await _collection.Find(x => x.InvoiceId == invoiceId).FirstOrDefaultAsync();

            invoice.Receipt = receipt;

            await _collection.ReplaceOneAsync(x => x.InvoiceId == invoiceId, invoice);

        }
    }
}
