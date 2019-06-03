﻿using Common.Enums;
using Common.Exception;
using Common.RequestHelper;
using Common.ViewModels;
using Common.ViewModels.ResponseViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PayAllHere.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ViewModels.RequestViewModel;

namespace PayAllHere.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IConfiguration _configuration;

        public TransactionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<List<TransactionResponseViewModel>> GetAllUserTransaction(string userId)
        {
            var url = $"{_configuration["TransactionAPIUrl"]}/api/Transaction/All/{userId}";

            try
            {
                var responseString = await HTTPRequestSender.GetAsync(url);

                return JsonConvert.DeserializeObject<List<TransactionResponseViewModel>>(responseString);
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.InvalidTransaction)
                {
                    throw new InvalidTransactionException();
                }
                throw new Exception("Unknown Exception");
            }
        }

        public async Task<TransactionResponseViewModel> GetTransactionById(string id)
        {
            var url = $"{_configuration["TransactionAPIUrl"]}/api/Transaction/{id}";

            try
            {
                var responseString = await HTTPRequestSender.GetAsync(url);

                return JsonConvert.DeserializeObject<TransactionResponseViewModel>(responseString);
            }
            catch (Exception e)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(e.Message);

                if (errorResponse.Id == (int)ErrorResponseIds.InvalidTransaction)
                {
                    throw new InvalidTransactionException();
                }
                throw new Exception("Unknown Exception");
            }
        }

       
        public async Task<bool> AddTransaction(TransactionRequestViewModel transactionRequestViewModel)
        {
            var url = $"{_configuration["TransactionAPIUrl"]}/api/Transaction";

            try
            {

                var responseString = await HTTPRequestSender.PostAsync(url, transactionRequestViewModel);
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
    }
}
