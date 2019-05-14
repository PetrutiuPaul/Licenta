using Common.ViewModels.RequestViewModel;
using Common.ViewModels.ResponseViewModel;
using UserService.API.Models;

namespace UserService.API.Service
{
    public static class UserMapping
    {
        public static UserResponseViewModel ToUserResponseViewModel(this User user)
        {
            return new UserResponseViewModel()
            {
                Address = user.Address,
                IsAdmin = user.IsAdmin,
                Balance = user.Balance,
                CNP = user.CNP,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                FullName = user.FullName,
                Id = user.Id,
                LastName = user.LastName,
                Username = user.Username
            };
        }

        public static User ToUser(this UserRequestViewModel userRequestViewModer)
        {
            return new User()
            {
                Address = userRequestViewModer.Address,
                IsAdmin = false,
                Balance = 0,
                CNP = userRequestViewModer.CNP,
                DateOfBirth = userRequestViewModer.DateOfBirth,
                Email = userRequestViewModer.Email,
                FirstName = userRequestViewModer.FirstName,
                FullName = $"{userRequestViewModer.FirstName} {userRequestViewModer.LastName}",
                LastName = userRequestViewModer.LastName,
                Password = userRequestViewModer.Password,
                Username = userRequestViewModer.Username,
            };
        }
    }
}
