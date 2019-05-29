using Common.ViewModels.RequestViewModel;
using Common.ViewModels.ResponseViewModel;
using Electrica.API.Models;

namespace Electrica.API.Service
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

        public static InvoiceResponseViewModel TorInvoiceResponseViewModel(this Invoice invoice)
        {
            return new InvoiceResponseViewModel()
            {
                Address = invoice.Address,
                EndDate = invoice.EndDate,
                InvoiceId = invoice.InvoiceId,
                PayedValue = invoice.PayedValue,
                PaymentDate = invoice.PaymentDate,
                ProviderName = invoice.ProviderName,
                Receipt = invoice.Receipt != null ? new ReceiptResponseViewModel()
                {
                    OtherData = invoice.Receipt.OtherData,
                    Value = invoice.Receipt.Value
                } : null,
                StartDate = invoice.StartDate,
                UserCNP = invoice.UserCNP,
                Value = invoice.Value
            };
        }
    }
}
