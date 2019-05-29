using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ViewModels.ResponseViewModel;

namespace PayAllHere.Service.Contracts
{
    public interface IUtilityService
    {
        Task<List<InvoiceResponseViewModel>> GetAllElectricaInvoice(string CNP);

        Task<List<InvoiceResponseViewModel>> GetAllEONInvoice(string CNP);
    }
}
