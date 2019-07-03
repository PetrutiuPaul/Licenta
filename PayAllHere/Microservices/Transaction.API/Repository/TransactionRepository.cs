using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using Transaction.API.Repository.Contracts;

namespace Transaction.API.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMongoCollection<Models.Transaction> _collection;

        public TransactionRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var mongoDatabase = client.GetDatabase("DB_TransactionService");

            _collection = mongoDatabase.GetCollection<Models.Transaction>("transactions");
        }

        public async Task AddTransaction(Models.Transaction transaction)
        {
            transaction.Id = ObjectId.GenerateNewId().ToString();
            await _collection.InsertOneAsync(transaction);
        }

        public async Task<List<Models.Transaction>> Get(Expression<Func<Models.Transaction, bool>> filter)
        {
            return (await _collection.Find(filter).ToListAsync()).OrderByDescending(x => x.AddedAt).ToList();
        }

        public async Task ValidateTransaction(Models.Transaction transaction)
        {
            var filter = Builders<Models.Transaction>.Filter.Eq(s => s.Id, transaction.Id);
            transaction.Validated = true;
            await _collection.ReplaceOneAsync(filter, transaction);
        }
    }
}
