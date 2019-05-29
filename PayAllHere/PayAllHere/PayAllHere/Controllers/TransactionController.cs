using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayAllHere.Service.Contracts;

namespace PayAllHere.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllUserTransaction(User.Claims.Where(x => x.Type.Contains("primarysid")).Select(x => x).First().Value);

            return View(transactions);
        }

        public async Task<IActionResult> Details(string id)
        {
            var transaction = await _transactionService.GetTransactionById(id);

            return View(transaction);
        }
    }
}