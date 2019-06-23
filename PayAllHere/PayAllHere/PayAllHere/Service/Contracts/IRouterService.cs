namespace PayAllHere.Service.Contracts
{
    public interface IRouterService
    {
        string GetRedirectUrl(string userName, string password);

        string GetInternalName(string userName, string password);
    }
}
