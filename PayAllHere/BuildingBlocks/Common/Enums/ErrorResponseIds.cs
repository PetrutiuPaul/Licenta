using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum ErrorResponseIds
    {
        UserAlreadyExist = 4000,
        UserInvalid = 4001,
        UserAlreadyExists = 4002,
        InvalidBalance = 4003,
        InvoiceNotExist = 4004,
        InvoiceAlreadyExist = 4005,
        InvalidValue = 4006
    }
}
