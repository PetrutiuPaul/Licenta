using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels.RequestViewModel
{
    public class TransactionRequestViewModel
    {
        public string Id { get; set; }

        public double Value { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string UserId { get; set; }

        public bool Validated { get; set; }

        public string InvoiceId { get; set; }
    }
}
