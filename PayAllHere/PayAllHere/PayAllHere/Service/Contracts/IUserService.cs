using Common.ViewModels.RequestViewModel;
using Common.ViewModels.ResponseViewModel;
using System.Threading.Tasks;

namespace PayAllHere.Service.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// Posts a user's credentials to the UserService API, and retrieves a UserViewModel if the user is valid
        /// </summary>
        /// <param name="user">Model containing username and password used to login</param>
        /// <returns>
        /// Model containing user's full data in case of success
        /// Throws InvalidUserException in case of failure
        /// </returns>
        Task<UserResponseViewModel> GetUserForLogin(UserRequestViewModel user);

        /// <summary>
        /// Posts a user's credential to the UserService API, registers said user, if valid
        /// </summary>
        /// <param name="user">model containing user data</param>
        /// <returns>
        /// model containing user's data in case of success
        /// null model in case of failure
        /// </returns>
        Task<UserResponseViewModel> RegisterUser(UserRequestViewModel user);

        /// <summary>
        /// checks whether or not a user exists with the same email or username
        /// </summary>
        /// <param name="user">model containing user data</param>
        /// <returns>
        /// whether or not the user exists
        /// </returns>
        Task<bool> UserExists(UserRequestViewModel user);

        /// <summary>
        /// Gets a user by its objectId
        /// </summary>
        /// <param name="userId">id of user</param>
        /// <returns>
        /// User data
        /// </returns>
        Task<UserResponseViewModel> GetUserByCNP(string userId);
    }
}
