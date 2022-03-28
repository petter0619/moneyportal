using MoneyPortalMain.Models;

namespace MoneyPortalMain.Services
{
    public interface IAccountsService
    {
        public List<Account> GetAllAccounts();
        public Account GetAccountById(int accountId);
    }
}
