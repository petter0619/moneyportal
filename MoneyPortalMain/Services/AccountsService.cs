using MoneyPortalMain.Models;

namespace MoneyPortalMain.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IUserService _userService;

        private readonly new List<Account> _exampleAccounts;

        public AccountsService(IUserService userService)
        {
            _userService = userService;
            _exampleAccounts = new List<Account>()
            {
                new Account() { Id = 1, Name = "Skandia", Type = "Checking Account" },
                new Account() { Id = 2, Name = "ICA", Type = "Savings Account" },
                new Account() { Id = 4, Name = "Cash", Type = "Cash" },
                new Account() { Id = 5, Name = "Coop Kreditkort", Type = "Credit Card" },
                new Account() { Id = 3, Name = "Revolut", Type = "Savings Account" }
            };
        }

        public List<Account> GetAllAccounts()
        {
            return _exampleAccounts;
        }

        public Account GetAccountById(int accountId)
        {
            return _exampleAccounts.FirstOrDefault(a => a.Id == accountId);
        }
    }
}
