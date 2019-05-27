using Common.ViewModels.RequestViewModel;
namespace Transaction.API.Service
{
    public static class TransactionMapping
    {
        public static Models.Transaction ToTransaction(this TransactionRequestViewModel transactionRequestViewModel)
        {
            return new Models.Transaction()
            {
                To = transactionRequestViewModel.To,
                From = transactionRequestViewModel.From,
                Id = transactionRequestViewModel.Id,
                UserId = transactionRequestViewModel.UserId,
                Validated = transactionRequestViewModel.Validated,
                Value = transactionRequestViewModel.Value
            };
        }
    }
}
