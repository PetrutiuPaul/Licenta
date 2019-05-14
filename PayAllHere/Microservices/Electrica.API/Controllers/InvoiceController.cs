using Common.Enums;
using Common.ViewModels;
using Common.ViewModels.RequestViewModel;
using Electrica.API.Models;
using Electrica.API.Repository.Contracts;
using Electrica.API.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult PayInvoice([FromBody]PayInvoiceRequestViewModel payInvoiceRequestViewModel)
        {
            try
            {
                _invoiceRepository.PayInvoice(payInvoiceRequestViewModel.InvoiceId, payInvoiceRequestViewModel.Value);
            }
            catch (Exception)
            {
                return Conflict(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.InvalidValue, Message = "InvalidValue" });
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult Get(string id)
        {
            try
            {
                var invoice = _invoiceRepository.GetInvoiceById(id);
                return Ok(invoice);
            }
            catch (Exception)
            {
                return NotFound(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.InvoiceNotExist, Message = "Invoice not exist" });
            }

        }

        [HttpGet]
        [Route("UserInvoices")]
        public IActionResult GetByCNP(string CNP)
        {
            var invoices = _invoiceRepository.GetInvoicesByCNP(CNP);

            return Ok(invoices);
        }


        [HttpPost]
        public IActionResult AddInvoice([FromBody]Invoice invoice)
        {
            try
            {
                _invoiceRepository.AddInvoice(invoice);
            }
            catch (Exception)
            {
                return Conflict(new ErrorResponseViewModel { Id = (int)ErrorResponseIds.InvoiceAlreadyExist, Message = "Invoice already added" });
            }

            return Ok();
        }


        [HttpPost]
        [Route("Receipt")]
        public IActionResult AddReceipt([FromBody]ReceiptRequestViewModel receiptRequestViewModel)
        {
            try
            {
                _invoiceService.AddReceipt(receiptRequestViewModel);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
