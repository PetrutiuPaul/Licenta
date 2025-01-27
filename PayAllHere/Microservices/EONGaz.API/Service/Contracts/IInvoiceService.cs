﻿using Common.ViewModels.RequestViewModel;
using System.Threading.Tasks;

namespace EONGaz.API.Service.Contracts
{
    public interface IInvoiceService
    {
        Task AddReceipt(ReceiptRequestViewModel receiptRequestViewModel);
    }
}
