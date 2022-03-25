using MoneyPortalMain.DTOs;

namespace MoneyPortalMain.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IUserService _userService;

        public AccountsService(IUserService userService)
        {
            _userService = userService;
        }

        public List<AccountDto> GetAllAccounts()
        {
            return new List<AccountDto>()
            {
                new AccountDto() { Id = 1, Name = "Skandia", Type = "Checking Account" },
                new AccountDto() { Id = 2, Name = "ICA", Type = "Savings Account" },
                new AccountDto() { Id = 4, Name = "Cash", Type = "Cash" },
                new AccountDto() { Id = 5, Name = "Coop Kreditkort", Type = "Credit Card" },
                new AccountDto() { Id = 3, Name = "Revolut", Type = "Savings Account" }
            };
        }
    }
}
