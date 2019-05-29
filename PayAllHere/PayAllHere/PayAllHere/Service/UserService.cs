using Common.Enums;
using Common.Exception;
using Common.RequestHelper;
using Common.ViewModels;
using Common.ViewModels.RequestViewModel;
using Common.ViewModels.ResponseViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PayAllHere.Service.Contracts;
using System;
using System.Threading.Tasks;

namespace PayAllHere.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<UserResponseViewModel> GetUserForLogin(UserRequestViewModel user)
        {
            var url = $"{_configuration["UserServiceAPIUrl"]}/api/User/Login";

            try
            {
                var responseString = await HTTPRequestSender.PostAsync(url, user);
                var responseUser = JsonConvert.DeserializeObject<UserResponseViewModel>(responseString);

                return responseUser;
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.UserInvalid)
                {
                    throw new UserNotFoundException();
                }

                throw;
            }
        }

        public async Task<UserResponseViewModel> RegisterUser(UserRequestViewModel user)
        {
            var url = $"{_configuration["UserServiceAPIUrl"]}/api/User/Register";
            try
            {
                var responseString = await HTTPRequestSender.PostAsync(url, user);

                var responseUser = JsonConvert.DeserializeObject<UserResponseViewModel>(responseString);

                return responseUser;
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.UserAlreadyExists)
                {
                    throw new UserAlreadyExistsException();
                }

                throw;
            }
        }

        public async Task<bool> UserExists(UserRequestViewModel user)
        {
            var url = $"{_configuration["UserServiceAPIUrl"]}/api/User/UserExist";
            try
            {
                var responseString = await HTTPRequestSender.PostAsync(url, user);

                return false;
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                return errorResponse.Id == (int)ErrorResponseIds.UserAlreadyExists || true;
            }
        }

        public async Task<UserResponseViewModel> GetUserByCNP(string CNP)
        {
            var url = $"{_configuration["UserServiceAPIUrl"]}/api/User/GetUserByCNP/{CNP}";

            try
            {
                var responseString = await HTTPRequestSender.GetAsync(url);

                return JsonConvert.DeserializeObject<UserResponseViewModel>(responseString);
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.UserInvalid)
                {
                    throw new UserNotFoundException();
                }
                throw new Exception("Unknown Exception");
            }
        }

        public async Task<double> ModifyBalance(UpdateBalanceRequestViewModel updateBalanceRequestViewModel)
        {
            var url = $"{_configuration["UserServiceAPIUrl"]}/api/User/ModifyBalance";
            try
            {   
                var responseString = await HTTPRequestSender.PostAsync(url, updateBalanceRequestViewModel);

                var responseUser = JsonConvert.DeserializeObject<double>(responseString);

                return responseUser;
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.UserAlreadyExists)
                {
                    throw new UserAlreadyExistsException();
                }

                throw;
            }
        }
    }
}
