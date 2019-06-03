using Common.Enums;
using Common.ViewModels.RequestViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayAllHere.Service.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PayAllHere.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        private readonly IUtilityService _utilityService;

        public PaymentController(IUserService userService, ITransactionService transactionService, IUtilityService utilityService)
        {
            _userService = userService;
            _transactionService = transactionService;
            _utilityService = utilityService;
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
                var transaction = new TransactionRequestViewModel()
                {
                    From = PaymentUserType.ExternalAccount.ToString(),
                    To = PaymentUserType.Me.ToString(),
                    UserId = userId,
                    Validated = true,
                    Value = paymentRequestViewModel.Value
                };

                var ok = await _transactionService.AddTransaction(transaction);

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

        [HttpGet]
        public ActionResult Pay(PayInvoiceRequestViewModel payInvoiceRequestViewModel)
        {
            return View(payInvoiceRequestViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> PayInvoice(PayInvoiceRequestViewModel payInvoiceRequestViewModel)
        {

            var userId = User.Claims.Where(x => x.Type.Contains("primarysid")).Select(x => x).First().Value;
            switch (payInvoiceRequestViewModel.Provider)
            {
                case PaymentUserType.InternEON:
                    await _utilityService.PayEON(payInvoiceRequestViewModel);
                    break;
                case PaymentUserType.InternElectrica:
                    await _utilityService.PayElectrica(payInvoiceRequestViewModel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            var transaction = new TransactionRequestViewModel()
            {
                From = PaymentUserType.Me.ToString(),
                To = payInvoiceRequestViewModel.Provider.ToString(),
                UserId = userId,
                Validated = false,
                Value = payInvoiceRequestViewModel.Value
            };

            var ok = await _transactionService.AddTransaction(transaction);
            return RedirectToAction("Index", "Home");
        }
    }
}