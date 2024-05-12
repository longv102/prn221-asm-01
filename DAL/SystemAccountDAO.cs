using BO.Dtos;
using DAL.Databases;

namespace DAL
{
    public class SystemAccountDAO
    {
        private SystemAccountDAO()
        {
        }

        private static SystemAccountDAO? _instance = null;
        private static readonly object _instanceLock = new object();
        public static SystemAccountDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SystemAccountDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<SystemAccountResponse> GetAccounts()
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var response = new List<SystemAccountResponse>();

                var accounts = context.SystemAccounts
                    .OrderBy(x => x.AccountId)
                    .ToList();
                foreach(var account in accounts )
                {
                    var mappedAccount = new SystemAccountResponse();

                    mappedAccount.AccountId = account.AccountId;
                    mappedAccount.AccountName = account.AccountName;
                    mappedAccount.AccountEmail = account.AccountEmail;
                    mappedAccount.AccountRole = account.AccountRole;
                    response.Add(mappedAccount);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SystemAccountResponse CheckLogin(string email, string password)
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var account = context.SystemAccounts.FirstOrDefault(x => x.AccountEmail.Trim().ToLower() == email.Trim().ToLower() 
                    && x.AccountPassword.Trim().ToLower() == password.Trim().ToLower());
                if (account is null)
                    throw new KeyNotFoundException("User is not found!");
                var mappedAccount = new SystemAccountResponse()
                {
                    AccountId = account.AccountId,
                    AccountName = account.AccountName,
                    AccountEmail = account.AccountEmail,
                    AccountRole = account.AccountRole,
                };
                return mappedAccount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
