using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayAllHere.Service.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Common.ViewModels.RequestViewModel;

namespace PayAllHere.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IUserService _userService;

        public PaymentController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(PaymentRequestViewModel paymentRequestViewModel)
        {


            return View();
        }


    }
}