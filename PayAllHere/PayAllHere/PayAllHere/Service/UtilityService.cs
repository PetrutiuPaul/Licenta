using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using Common.Enums;
using Common.Exception;
using Common.RequestHelper;
using Common.ViewModels;
using Common.ViewModels.RequestViewModel;
using Common.ViewModels.ResponseViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PayAllHere.Service.Contracts;

namespace PayAllHere.Service
{
    public class UtilityService : IUtilityService
    {
        private readonly IConfiguration _configuration;
        private readonly IRouterService _routerService;

        public UtilityService(IConfiguration configuration, IRouterService routerService)
        {
            _configuration = configuration;
            _routerService = routerService;
        }

        public async Task<List<InvoiceResponseViewModel>> GetAllElectricaInvoice(string CNP)
        {
            var url = $"{_configuration["ElectricaAPIUrl"]}/api/invoice/UserInvoices/{CNP}";

            try
            {
                var responseString = await HTTPRequestSender.GetAsync(url);

                return JsonConvert.DeserializeObject<List<InvoiceResponseViewModel>>(responseString);
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.InvoiceNotExist)
                {
                    throw new InvoiceNotExistException();
                }
                throw new Exception("Unknown Exception");
            }
        }

        public async Task<List<InvoiceResponseViewModel>> GetAllEONInvoice(string CNP)
        {
            var url = $"{_configuration["EONGazAPIUrl"]}/api/invoice/UserInvoices/{CNP}";

            try
            {
                var responseString = await HTTPRequestSender.GetAsync(url);

                return JsonConvert.DeserializeObject<List<InvoiceResponseViewModel>>(responseString);
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.InvoiceNotExist)
                {
                    throw new InvoiceNotExistException();
                }
                throw new Exception("Unknown Exception");
            }
        }

        public async Task<bool> PayEON(PayInvoiceRequestViewModel payInvoiceRequestViewModel)
        {
            var url = $"{_configuration["EONGazAPIUrl"]}/api/invoice/pay";

            try
            {

                var responseString = await HTTPRequestSender.PostAsync(url, payInvoiceRequestViewModel);
                var responseUser = JsonConvert.DeserializeObject<bool>(responseString);

                return responseUser;
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.UserInvalid)
                {
                    throw new UserNotFoundException();
                }

                throw;
            }
        }

        public async Task<bool> PayElectrica(PayInvoiceRequestViewModel payInvoiceRequestViewModel)
        {
            var url = $"{_configuration["ElectricaAPIUrl"]}/api/invoice/pay";

            try
            {

                var responseString = await HTTPRequestSender.PostAsync(url, payInvoiceRequestViewModel);
                var responseUser = JsonConvert.DeserializeObject<bool>(responseString);

                return responseUser;
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.UserInvalid)
                {
                    throw new UserNotFoundException();
                }

                throw;
            }
        }

        public async Task<bool> AddInvoice(InvoiceAuthRequestViewModel invoice)
        {
            try
            {
                var url = _routerService.GetRedirectUrl(invoice.Username, invoice.Password);

                if(string.IsNullOrEmpty(url))
                    throw new InvalidCredentialException();

                url += "/api/invoice";

                var responseString = await HTTPRequestSender.PostAsync(url, invoice);
                var responseUser = JsonConvert.DeserializeObject<bool>(responseString);

                return responseUser;
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.InvoiceAlreadyExist)
                {
                    throw new InvoiceAlreadyExistException();
                }

                throw;
            }
        }
    }
}
