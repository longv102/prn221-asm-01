using BO.Dtos;
using DAL;
using Repositories.Contracts;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public SystemAccountResponse Authenticate(string email, string password)
            => SystemAccountDAO.Instance.CheckLogin(email, password);

        public IEnumerable<SystemAccountResponse> GetAccounts()
            => SystemAccountDAO.Instance.GetAccounts();
    }
}
