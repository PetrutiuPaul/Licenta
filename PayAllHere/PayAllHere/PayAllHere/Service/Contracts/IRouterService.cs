using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayAllHere.Service.Contracts
{
    public interface IRouterService
    {
        string GetRedirectUrl(string userName, string password);
    }
}
