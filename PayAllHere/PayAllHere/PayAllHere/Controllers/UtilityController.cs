using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayAllHere.Service.Contracts;

namespace PayAllHere.Controllers
{
    public class UtilityController : Controller
    {
        private readonly IUtilityService _utilityService;

        public UtilityController(IUtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        public async Task<IActionResult> ElectricaInvoices()
        {
            var invoices = await _utilityService.GetAllElectricaInvoice(User.Claims.Where(x => x.Type.Contains("nameidentifier")).Select(x => x).First().Value);

            return View(invoices);
        }

        public async Task<IActionResult> EONInvoices()
        {
            var invoices = await _utilityService.GetAllEONInvoice(User.Claims.Where(x => x.Type.Contains("nameidentifier")).Select(x => x).First().Value);

            return View(invoices);
        }
    }
}