using Common.ViewModels.ResponseViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ViewModels.RequestViewModel;

namespace PayAllHere.Service.Contracts
{
    public interface ITransactionService
    {
        Task<List<TransactionResponseViewModel>> GetAllUserTransaction(string userId);

        Task<TransactionResponseViewModel> GetTransactionById(string id);

        Task<bool> AddTransaction(TransactionRequestViewModel transactionRequestViewModel);
    }
}
