using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ViewModels.RequestViewModel;
using EONGaz.API.Models;
using EONGaz.API.Repository.Contracts;
using EONGaz.API.Service.Contracts;

namespace EONGaz.API.Service
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
