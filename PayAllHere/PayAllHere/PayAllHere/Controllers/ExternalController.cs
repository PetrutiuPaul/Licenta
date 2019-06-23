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

        public ExternalController(IUtilityService utilityService)
        {
            _utilityService = utilityService;
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

    }
}