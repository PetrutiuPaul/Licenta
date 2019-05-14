using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using UserService.API.Models;
using UserService.API.Repository.Contracts;

namespace UserService.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var mongoDatabase = client.GetDatabase("DSN_UserService");

            _collection = mongoDatabase.GetCollection<User>("users");
        }

        public async Task AddUser(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            await _collection.InsertOneAsync(user);
        }

        public async Task<double> ChangeUserBalanceWith(string id, double value)
        {
            var user = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (user.Balance >= value)
            {
                user.Balance += value;
            }
            else
            {
                throw new Exception();
            }
            await _collection.ReplaceOneAsync(x => x.Id == id, user);

            return user.Balance;
        }

        public async Task<bool> CheckIfUserExists(string email, string CNP)
        {
            return await _collection.Find(x => x.Email == email || x.CNP == CNP).AnyAsync();
        }

        public async Task<double> GetUserBalance(string id)
        {
            return (await _collection.Find(x => x.Id == id).FirstOrDefaultAsync()).Balance;
        }

        public async Task<User> GetUserByCNP(string cnp)
        {
            return await _collection.Find(x => x.CNP == cnp).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernamePassword(string userUsername, string userPassword)
        {
            return await _collection.Find(x => x.Username == userUsername && x.Password == userPassword).FirstOrDefaultAsync();
        }
    }
}
