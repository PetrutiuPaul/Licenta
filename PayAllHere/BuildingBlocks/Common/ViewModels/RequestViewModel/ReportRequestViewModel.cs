using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels.RequestViewModel
{
    public class ReportRequestViewModel
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string InternalName { get; set; }
    }

    public class ReportAuthRequestViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string InternalName { get; set; }
    }
}
