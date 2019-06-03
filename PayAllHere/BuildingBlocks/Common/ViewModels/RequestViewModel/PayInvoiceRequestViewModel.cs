using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Common.ViewModels.RequestViewModel
{
    public class PayInvoiceRequestViewModel
    {
        public string InvoiceId { get; set; }

        public PaymentUserType Provider { get; set; }

        public double Value { get; set; }
    }
}
