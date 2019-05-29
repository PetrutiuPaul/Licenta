using System;
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
        private readonly ITransactionService _transactionService;

        public PaymentController(IUserService userService, ITransactionService transactionService)
        {
            _userService = userService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Deposit(PaymentRequestViewModel paymentRequestViewModel)
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type.Contains("primarysid")).Select(x => x).First().Value;
                var ok = await _transactionService.AddCredit(paymentRequestViewModel, userId);

                if (!ok) return View();

                var model = new UpdateBalanceRequestViewModel()
                {
                    Value = paymentRequestViewModel.Value,
                    Id = userId
                };

                await _userService.ModifyBalance(model);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }


    }
}