using Common.ViewModels.RequestViewModel;
using System.Threading.Tasks;

namespace Electrica.API.Service.Contracts
{
    public interface IInvoiceService
    {
        Task AddReceipt(ReceiptRequestViewModel receiptRequestViewModel);
    }
}
