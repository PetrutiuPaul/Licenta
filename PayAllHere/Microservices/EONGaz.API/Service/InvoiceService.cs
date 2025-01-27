﻿using Common.ViewModels.RequestViewModel;
using EONGaz.API.Repository.Contracts;
using EONGaz.API.Service.Contracts;
using System.Threading.Tasks;

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
