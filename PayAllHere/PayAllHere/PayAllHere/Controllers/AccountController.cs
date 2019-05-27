using Common.Exception;
using Common.ViewModels.RequestViewModel;
using Common.ViewModels.ResponseViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayAllHere.Service.Contracts;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayAllHere.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<ActionResult> Details()
        {
            var user = await _userService.GetUserByCNP(User.Claims.Where(x => x.Type.Contains("nameidentifier")).Select(x => x).First().Value);

            return View(user);

        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserRequestViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                UserResponseViewModel loggedInUser = await _userService.GetUserForLogin(user);

                if (loggedInUser != null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, loggedInUser.FirstName + " " + loggedInUser.LastName));
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loggedInUser.CNP));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "User"));

                    if (loggedInUser.IsAdmin)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    }

                    identity.AddClaim(new Claim("UserId", loggedInUser.Id));

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                        IsPersistent = true,
                    };

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (UserNotFoundException)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Login", user);
            }

            return null;
        }


        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRequestViewModel newUser)
        {
            if (await _userService.UserExists(newUser)) //on error
            {
                ModelState.AddModelError("", "Username or email already exists.");
                return View("Login", newUser);
            }

            if (!ModelState.IsValid)
            {
                return View("Login", newUser);
            }

            await _userService.RegisterUser(newUser);

            return RedirectToAction("Index", "Home");
        }
    }
}