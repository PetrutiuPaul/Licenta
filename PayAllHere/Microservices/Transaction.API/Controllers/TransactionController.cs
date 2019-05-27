using Common.Enums;
using Common.ViewModels;
using Common.ViewModels.RequestViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
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

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTransaction(string id)
        {
            try
            {
                var transaction = await _transactionRepository.Get(x => x.Id == id);

                return Ok(transaction);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponseViewModel()
                { Id = (int)ErrorResponseIds.InvalidTransaction, Message = "Invalid Transaction Id" });
            }
        }


        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllTransaction()
        {
            try
            {
                var transactions = await _transactionRepository.Get(x => x.Id != "");

                return Ok(transactions);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponseViewModel()
                    { Id = (int)ErrorResponseIds.InvalidTransaction, Message = "Invalid Transaction Id" });
            }
        }
    }
}
