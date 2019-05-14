using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EONGaz.API.Models
{
    public class Receipt
    {
        public string Id { get; set; }

        public double Value { get; set; }

        public string OtherData { get; set; }
    }
}
