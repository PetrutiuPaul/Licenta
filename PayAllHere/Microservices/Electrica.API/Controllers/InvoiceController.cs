using Common.Enums;
using Common.ViewModels;
using Common.ViewModels.RequestViewModel;
using Electrica.API.Models;
using Electrica.API.Repository.Contracts;
using Electrica.API.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Electrica.API.Service;

namespace Electrica.API.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceRepository invoiceRepository, IInvoiceService invoiceService)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceService = invoiceService;
        }

        [HttpPost]
        [Route("Pay")]
        public async Task<IActionResult> PayInvoice([FromBody]PayInvoiceRequestViewModel payInvoiceRequestViewModel)
        {
            try
            {
                await _invoiceRepository.PayInvoice(payInvoiceRequestViewModel.InvoiceId, payInvoiceRequestViewModel.Value);
            }
            catch (Exception)
            {
                return Conflict(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.InvalidValue, Message = "InvalidValue" });
            }

            return Ok(true);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var invoice = await _invoiceRepository.GetInvoiceById(id);
                return Ok(invoice.TorInvoiceResponseViewModel());
            }
            catch (Exception)
            {
                return NotFound(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.InvoiceNotExist, Message = "Invoice not exist" });
            }

        }

        [HttpGet]
        [Route("UserInvoices/{CNP}")]
        public async Task<IActionResult> GetByCNP(string CNP)
        {
            var invoices = await _invoiceRepository.GetInvoicesByCNP(CNP);

            return Ok(invoices.Select(x => x.TorInvoiceResponseViewModel()));
        }


        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody]InvoiceRequestViewModel invoice)
        {
            try
            {
               await _invoiceRepository.AddInvoice(invoice.ToInvoice());
            }
            catch (Exception)
            {
                return Conflict(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.InvoiceAlreadyExist, Message = "Invoice already added" });
            }

            return Ok(true);
        }


        [HttpPost]
        [Route("Receipt")]
        public async Task<IActionResult> AddReceipt([FromBody]ReceiptRequestViewModel receiptRequestViewModel)
        {
            try
            {
                await _invoiceService.AddReceipt(receiptRequestViewModel);
            }
            catch (Exception)
            {
                return NotFound(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.InvoiceAlreadyExist, Message = "Receipt already added" });
            }

            return Ok(true);
        }
    }
}
