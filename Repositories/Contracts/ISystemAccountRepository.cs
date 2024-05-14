using BO.Dtos;

namespace Repositories.Contracts
{
    public interface ISystemAccountRepository
    {
        IEnumerable<SystemAccountResponse> GetAccounts();

        IEnumerable<SystemAccountResponse> SearchAccountByName(string name);

        SystemAccountResponse Authenticate(string email, string password);

        bool DeleteAccount(short accountId);   

        bool CreateAccount(SystemAccountRequest request);

        bool UpdateAccount(SystemAccountRequest request);   
    }
}
