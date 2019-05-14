using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ViewModels.RequestViewModel;

namespace EONGaz.API.Service.Contracts
{
    public interface IInvoiceService
    {
        Task AddReceipt(ReceiptRequestViewModel receiptRequestViewModel);
    }
}
