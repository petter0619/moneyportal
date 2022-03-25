using MoneyPortalMain.DTOs;

namespace MoneyPortalMain.Services
{
    public interface IAccountsService
    {
        public List<AccountDto> GetAllAccounts();
    }
}
