using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PayAllHere.Service.Contracts;

namespace PayAllHere.Service
{
    public class RouterService : IRouterService
    {
        private readonly IConfiguration _configuration;

        public RouterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetRedirectUrl(string userName, string password)
        {
            return _configuration[userName+":Password"] != password ? string.Empty : _configuration[userName + ":Url"];
        }

        public string GetInternalName(string userName, string password)
        {
            return _configuration[userName + ":Password"] != password ? string.Empty : _configuration[userName + ":InternalName"];
        }
    }
}
