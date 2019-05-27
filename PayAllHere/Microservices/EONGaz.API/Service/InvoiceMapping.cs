using Common.ViewModels.RequestViewModel;
using EONGaz.API.Models;

namespace EONGaz.API.Service
{
    public static class InvoiceMapping
    {
        public static Receipt ToReceipt(this ReceiptRequestViewModel receiptRequestViewModel)
        {
            return new Receipt()
            {
                Id = receiptRequestViewModel.Id,
                OtherData = receiptRequestViewModel.OtherData,
                Value = receiptRequestViewModel.Value
            };
        }
        public static Invoice ToInvoice(this InvoiceRequestViewModel invoiceRequestViewModel)
        {
            return new Invoice()
            {
                InvoiceId = invoiceRequestViewModel.InvoiceId,
                Address = invoiceRequestViewModel.Address,
                EndDate = invoiceRequestViewModel.EndDate,
                IsPayed = invoiceRequestViewModel.IsPayed,
                PayedValue = invoiceRequestViewModel.PayedValue,
                PaymentDate = invoiceRequestViewModel.PaymentDate,
                ProviderName = invoiceRequestViewModel.ProviderName,
                Receipt = null,
                StartDate = invoiceRequestViewModel.StartDate,
                UserCNP = invoiceRequestViewModel.UserCNP
            };
        }
    }
}
