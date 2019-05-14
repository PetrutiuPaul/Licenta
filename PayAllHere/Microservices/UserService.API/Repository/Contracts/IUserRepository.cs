using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.API.Models;

namespace UserService.API.Repository.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernamePassword(string userUsername, string userPassword);

        Task<bool> CheckIfUserExists(string email, string username);

        Task<User> GetUserByCNP(string cnp);

        Task AddUser(User user);

        Task<double> GetUserBalance(string id);

        Task<double> ChangeUserBalanceWith(string id, double value);
    }
}
