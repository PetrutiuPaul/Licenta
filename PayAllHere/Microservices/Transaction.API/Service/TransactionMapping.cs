using System;
using Common.ViewModels.RequestViewModel;
using Common.ViewModels.ResponseViewModel;

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
                Value = transactionRequestViewModel.Value,
                AddedAt = DateTime.Now,
                InvoiceId = transactionRequestViewModel.InvoiceId
            };
        }

        public static TransactionResponseViewModel ToTransactionResponseViewModel(this Models.Transaction transaction)
        {
            return new TransactionResponseViewModel()
            {
                From = transaction.From,
                Id = transaction.Id,
                To = transaction.To,
                Value = transaction.Value,
                Validated = transaction.Validated
            };
        }
    }
}
