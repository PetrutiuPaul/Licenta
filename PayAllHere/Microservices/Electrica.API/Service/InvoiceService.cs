using Common.ViewModels.RequestViewModel;
using Electrica.API.Repository.Contracts;
using Electrica.API.Service.Contracts;
using System.Threading.Tasks;

namespace Electrica.API.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task AddReceipt(ReceiptRequestViewModel receiptRequestViewModel)
        {
            var receipt = receiptRequestViewModel.ToReceipt();

            await _invoiceRepository.AddReceipt(receiptRequestViewModel.InvoiceId, receipt);
        }
    }
}
