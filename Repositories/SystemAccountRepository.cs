using BO.Dtos;
using DAL;
using Repositories.Contracts;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public SystemAccountResponse Authenticate(string email, string password)
            => SystemAccountDAO.Instance.CheckLogin(email, password);

        public bool CreateAccount(SystemAccountRequest request)
            => SystemAccountDAO.Instance.CreateAccount(request);

        public bool DeleteAccount(short accountId) => SystemAccountDAO.Instance.DeleteAccount(accountId);

        public SystemAccountResponse GetAccountById(short id)
            => SystemAccountDAO.Instance.Get(id);

        public IEnumerable<SystemAccountResponse> GetAccounts()
            => SystemAccountDAO.Instance.GetAccounts();

        public IEnumerable<SystemAccountResponse> SearchAccountByName(string name)
            => SystemAccountDAO.Instance.GetAccountsByName(name);

        public bool UpdateAccount(SystemAccountRequest request)
            => SystemAccountDAO.Instance.UpdateAccount(request);

        public bool UpdateAccountForStaff(SystemAccountRequest request)
            => SystemAccountDAO.Instance.UpdateAccountForStaff(request);
    }
}
