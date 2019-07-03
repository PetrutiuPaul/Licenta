using Common.Enums;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Transaction.API.Repository;

namespace Transaction.API.Service
{
    public class ValidateBackgroundService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This will cause the loop to stop if the service is stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                var transactionRepository = new TransactionRepository();

                var transactions = await transactionRepository.Get(x =>
                    (x.AddedAt >= DateTime.Now.AddDays(-1) && x.AddedAt <= DateTime.Now && x.Validated == false) && (x.To == PaymentUserType.InternEON.ToString() || x.To == PaymentUserType.InternElectrica.ToString()));

                var transactionPerProvider = new Dictionary<string, double>();

                foreach (var transaction in transactions)
                {
                    if (transactionPerProvider.ContainsKey(transaction.To))
                    {
                        transactionPerProvider[transaction.To] += transaction.Value;
                    }
                    else
                    {
                        transactionPerProvider[transaction.To] = transaction.Value;
                    }

                    await transactionRepository.ValidateTransaction(transaction);
                }

                foreach (var (key, value) in transactionPerProvider)
                {
                    await transactionRepository.AddTransaction(new Transaction.API.Models.Transaction()
                    {
                        AddedAt = DateTime.Now,
                        From = PaymentUserType.App.ToString(),
                        InvoiceId = Guid.NewGuid().ToString(),
                        To = key.Replace("Intern",""),
                        UserId = "App",
                        Validated = true,
                        Value = value
                    });
                }

                await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Store the task we're executing
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // If the task is completed then return it,
            // this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken));
            }
        }

        public void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
