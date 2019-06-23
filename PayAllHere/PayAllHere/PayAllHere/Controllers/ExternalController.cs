using System;
using Common.Enums;
using Common.Exception;
using Common.ViewModels;
using Common.ViewModels.RequestViewModel;
using Microsoft.AspNetCore.Mvc;
using PayAllHere.Service.Contracts;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace PayAllHere.Controllers
{
    [Route("api/external")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly IUtilityService _utilityService;
        private readonly ITransactionService _transactionService;

        public ExternalController(IUtilityService utilityService, ITransactionService transactionService)
        {
            _utilityService = utilityService;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody]InvoiceAuthRequestViewModel invoice)
        {
            try
            {
                await _utilityService.AddInvoice(invoice);
            }
            catch (InvoiceAlreadyExistException)
            {
                return Conflict(new ErrorResponseViewModel
                    {Id = (int) ErrorResponseIds.InvoiceAlreadyExist, Message = "Invoice already added"});
            }
            catch (InvalidCredentialException)
            {
                return Conflict(new ErrorResponseViewModel
                    {Id = 404, Message = "Invalid credentials"});
            }
            catch (Exception)
            {
                return Conflict(new ErrorResponseViewModel
                    {Id = 400, Message = "ceva o crapat"});
            }

            return Ok(true);
        }

        [HttpPost]
        [Route("Report")]
        public async Task<IActionResult> GetReport([FromBody]ReportAuthRequestViewModel reportRequestViewModel)
        {
            try
            {
                var list = await _transactionService.GetTransactionsBetween(reportRequestViewModel);

                return Ok(list);
            }
            catch (Exception)
            {
                return Conflict(new ErrorResponseViewModel
                    {Id = 400, Message = "ceva o crapat"});
            }
        }

    }
}