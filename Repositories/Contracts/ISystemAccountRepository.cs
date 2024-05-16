using BO.Dtos;

namespace Repositories.Contracts
{
    public interface ISystemAccountRepository
    {
        IEnumerable<SystemAccountResponse> GetAccounts();

        IEnumerable<SystemAccountResponse> SearchAccountByName(string name);

        SystemAccountResponse Authenticate(string email, string password);

        SystemAccountResponse GetAccountById(short id);

        bool DeleteAccount(short accountId);   

        bool CreateAccount(SystemAccountRequest request);

        bool UpdateAccount(SystemAccountRequest request);

        bool UpdateAccountForStaff(SystemAccountRequest request);
    }
}
