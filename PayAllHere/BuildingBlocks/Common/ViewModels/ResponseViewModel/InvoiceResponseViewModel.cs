using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels.ResponseViewModel
{
    public class InvoiceResponseViewModel
    {
        public string InvoiceId { get; set; }

        public string ProviderName { get; set; }

        public string UserCNP { get; set; }

        public string Address { get; set; }

        public double Value { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime PaymentDate { get; set; }

        public double PayedValue { get; set; }

        public ReceiptResponseViewModel Receipt { get; set; }
    }

    public class ReceiptResponseViewModel
    {
        public double Value { get; set; }

        public string OtherData { get; set; }
    }
}

