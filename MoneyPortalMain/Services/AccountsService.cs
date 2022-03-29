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

        public MoneyAccountsDetailsViewModel GenerateMoneyAccountsDetailsViewModel(int accountId)
        { 
            var account = GetAccountById(accountId);

            return new MoneyAccountsDetailsViewModel()
            {
                AccountName = account.Name,
                CurrentBalance = 37550.94,
                MonthlySpending = 5390.00,
                MonthlyDeposits = 2940.94,
                MonthlyTransactions = 18,
                TransactionsList = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Id = 1,
                        Memo = "Matköp lördag",
                        Amount = 500.00,
                        Date = new DateTime(2022, 3, 26, 0, 0, 0),
                        Type = "Debit Card",
                        Store = "Daglivs Fridhemsplan"
                    },
                    new Transaction()
                    {
                        Id = 3,
                        Memo = "Lunch med AFRY gänget (pannkakor)",
                        Amount = 115.00,
                        Date = new DateTime(2022, 3, 25, 0, 0, 0),
                        Type = "Debit Card",
                        Store = "Sabis Haga"
                    },
                    new Transaction()
                    {
                        Id = 3,
                        Memo = "Lysa Invest",
                        Amount = 3000.00,
                        Date = new DateTime(2022, 3, 25, 0, 0, 0),
                        Type = "Transfer",
                        Store = "Sabis Haga"
                    },
                    new Transaction()
                    {
                        Id = 2,
                        Memo = "Lön",
                        Amount = 23611.00,
                        Date = new DateTime(2022, 3, 25, 0, 0, 0),
                        Type = "Deposit",
                        Store = "Daglivs Fridhemsplan"
                    },
                }
            };
        }
    }
}
