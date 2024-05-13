using BO.Dtos;

namespace Repositories.Contracts
{
    public interface ISystemAccountRepository
    {
        IEnumerable<SystemAccountResponse> GetAccounts();

        SystemAccountResponse Authenticate(string email, string password);

        bool DeleteAccount(short accountId);   

        IEnumerable<SystemAccountResponse> SearchAccountByName(string name);

        bool CreateAccount(SystemAccountRequest request);

        bool UpdateAccount(SystemAccountRequest request);   
    }
}
