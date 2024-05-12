using BO.Dtos;

namespace Repositories.Contracts
{
    public interface ISystemAccountRepository
    {
        IEnumerable<SystemAccountResponse> GetAccounts();

        SystemAccountResponse Authenticate(string email, string password);
    }
}
