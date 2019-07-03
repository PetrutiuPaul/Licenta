using Common.Enums;
using Common.ViewModels;
using Common.ViewModels.RequestViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Transaction.API.Repository.Contracts;
using Transaction.API.Service;

namespace Transaction.API.Controllers
{
    [Route("api/Transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionRequestViewModel transactionRequestViewModel)
        {
            try
            {
               await _transactionRepository.AddTransaction(transactionRequestViewModel.ToTransaction());
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponseViewModel()
                { Id = (int)ErrorResponseIds.InvalidTransaction, Message = "Invalid Transaction" });
            }

            return Ok(true);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTransaction(string id)
        {
            try
            {
                var transaction = (await _transactionRepository.Get(x => x.Id == id)).FirstOrDefault();

                return Ok(transaction.ToTransactionResponseViewModel());
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponseViewModel()
                { Id = (int)ErrorResponseIds.InvalidTransaction, Message = "Invalid Transaction Id" });
            }
        }


        [HttpGet]
        [Route("All/{userId}")]
        public async Task<IActionResult> GetAllTransaction(string userId)
        {
            try
            {
                var transactions = await _transactionRepository.Get(x => x.UserId == userId);

                return Ok(transactions.Select(x => x.ToTransactionResponseViewModel()));
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponseViewModel()
                    { Id = (int)ErrorResponseIds.InvalidTransaction, Message = "Error" });
            }
        }

        [HttpPost]
        [Route("AllInBetween")]
        public async Task<IActionResult> GetAllTransactionBetween(ReportRequestViewModel reportRequestViewModel)
        {
            try
            {
                var transactions = await _transactionRepository.Get(x => x.AddedAt >= reportRequestViewModel.From && x.AddedAt <= reportRequestViewModel.To && x.Validated && x.To == reportRequestViewModel.InternalName);

                return Ok(transactions.Select(x => x.ToTransactionResponseViewModel()));
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponseViewModel()
                    { Id = (int)ErrorResponseIds.InvalidTransaction, Message = "Error" });
            }
        }
    }
}
