using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ViewModels.RequestViewModel;
using EONGaz.API.Models;

namespace EONGaz.API.Service
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
