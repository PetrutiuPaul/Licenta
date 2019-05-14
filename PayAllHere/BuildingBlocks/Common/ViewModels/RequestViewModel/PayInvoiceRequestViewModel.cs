using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels.RequestViewModel
{
    public class PayInvoiceRequestViewModel
    {
        public string InvoiceId { get; set; }

        public double Value { get; set; }
    }
}
