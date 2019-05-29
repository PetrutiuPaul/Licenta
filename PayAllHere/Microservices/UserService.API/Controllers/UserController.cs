using System;
using System.Threading.Tasks;
using Common.Enums;
using Common.ViewModels;
using Common.ViewModels.RequestViewModel;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Repository.Contracts;
using UserService.API.Service;

namespace UserService.API.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequestViewModel user)
        {
            var userToReturn = await userRepository.GetUserByUsernamePassword(user.Username, user.Password);

            if (userToReturn == null)
            {
                return NotFound(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.UserInvalid, Message = "Invalid user credentials" });
            }

            return Ok(userToReturn);
        }

        // POST api/values
        [HttpPost]
        [Route("UserExist")]
        public async Task<IActionResult> CheckIfUserExists([FromBody] UserRequestViewModel user)
        {
            var userExists = await userRepository.CheckIfUserExists(user.Email, user.Username);

            if (userExists)
            {
                return Conflict(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.UserAlreadyExists, Message = "Invalid user credentials" });
            }

            return Ok();
        }

        // POST api/values
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRequestViewModel user)
        {
            var dbUser = user.ToUser();

            await userRepository.AddUser(dbUser);

            return Ok(dbUser.ToUserResponseViewModel());
        }


        [HttpGet]
        [Route("GetUserByCNP/{CNP}")]
        public async Task<IActionResult> GetUserByCNP(string CNP)
        {
            var user = await userRepository.GetUserByCNP(CNP);
            if (user == null)
            {
                return NotFound(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.UserInvalid, Message = "User does not exist" });
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("ModifyBalance")]
        public async Task<IActionResult> ModifyBalance([FromBody] UpdateBalanceRequestViewModel balanceViewModel)
        {
            try
            {
                var newValue = await userRepository.ChangeUserBalanceWith(balanceViewModel.Id,balanceViewModel.Value);

                return Ok(newValue);
            }
            catch (Exception)
            {
                return Conflict(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.InvalidBalance, Message = "Not enough money" });
            }

        }

        [HttpGet]
        [Route("GetBalance/{userId}")]
        public async Task<IActionResult> Balance(string userId)
        {
            var balance = await userRepository.GetUserBalance(userId);
            return Ok(balance);
        }


    }
}
