using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Transaction.API.Repository.Contracts
{
    public interface ITransactionRepository
    {
        Task AddTransaction(Models.Transaction transaction);

        Task<List<Models.Transaction>> Get(Expression<Func<Models.Transaction, bool>> filter);
    }
}
