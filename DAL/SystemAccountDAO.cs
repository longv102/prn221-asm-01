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
                if (!accounts.Any())
                    throw new Exception("Account does not exist!");
                foreach(var account in accounts)
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

        public bool DeleteAccount(short accountId)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                var account = context.SystemAccounts.Find(accountId);
                if (account is null)
                    throw new KeyNotFoundException("User is not found!");
                // Check whether the account has created post 
                var checkPost = context.NewsArticles.Any(x => x.CreatedById == account.AccountId);
                if (checkPost)
                    throw new Exception("Cannot delete the user, because the post is created by this user!");
                
                context.Remove(account);
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<SystemAccountResponse> GetAccountsByName(string name)
        {
            try
            {
                var context = new FunewsManagementDbContext();
                var mappedResult = new List<SystemAccountResponse>();

                var accounts = context.SystemAccounts
                    .Where(x => x.AccountName.Contains(name))
                    .ToList();
                if (!accounts.Any()) throw new Exception("Account does not exist!");
                foreach (var account in accounts)
                {
                    var mappedAccount = new SystemAccountResponse();
                    mappedAccount.AccountId = account.AccountId;
                    mappedAccount.AccountName = account.AccountName;
                    mappedAccount.AccountEmail = account.AccountEmail;
                    mappedAccount.AccountRole = account.AccountRole;

                    mappedResult.Add(mappedAccount);
                }
                return mappedResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CreateAccount(SystemAccountRequest request)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                if (request.AccountId <= 0)
                    throw new Exception("Invalid account id!");
                if (string.IsNullOrEmpty(request.AccountName))
                    throw new Exception("Account name is required!");
                if (string.IsNullOrEmpty(request.AccountEmail))
                    throw new Exception("Account email is required!");

                var idDuplication = context.SystemAccounts.Any(x => x.AccountId == request.AccountId);
                if (idDuplication)
                    throw new Exception("Account id has already existed!");
                var emailDuplication = context.SystemAccounts.Any(x => x.AccountEmail == request.AccountEmail);
                if (emailDuplication)
                    throw new Exception("Account email has already existed!");

                var account = new SystemAccount()
                {
                    AccountId = request.AccountId,
                    AccountName = request.AccountName,
                    AccountEmail = request.AccountEmail,
                    AccountPassword = "@123@!",     // default password
                    AccountRole = request.AccountRole,
                };
                context.Add(account);
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool UpdateAccount(SystemAccountRequest request)
        {
            var result = false;
            try
            {
                var context = new FunewsManagementDbContext();
                if (string.IsNullOrEmpty(request.AccountName))
                    throw new Exception("Account name is required!");
                if (string.IsNullOrEmpty(request.AccountEmail))
                    throw new Exception("Account email is required!");
                
                var emailDuplication = context.SystemAccounts.Any(x => x.AccountEmail == request.AccountEmail);
                if (emailDuplication)
                    throw new Exception("Account email has already existed!");

                var account = context.SystemAccounts.Find(request.AccountId);
                if (account is null)
                    throw new Exception("Account email has already existed!");
                // Update
                account.AccountName = request.AccountName;
                account.AccountEmail = request.AccountEmail;
                context.Update(account);
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
