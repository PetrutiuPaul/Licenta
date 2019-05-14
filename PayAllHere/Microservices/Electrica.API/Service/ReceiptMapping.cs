using Common.ViewModels.RequestViewModel;
using Electrica.API.Models;

namespace Electrica.API.Service
{
    public static class ReceiptMapping
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
    }
}
