using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels.RequestViewModel
{
    public class ReceiptRequestViewModel
    {
        public string Id { get; set; }

        public string InvoiceId { get; set; }
        public double Value { get; set; }

        public string OtherData { get; set; }
    }
}
