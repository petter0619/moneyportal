namespace MoneyPortalMain.Models
{
    public class MoneyAccountsDetailsViewModel
    {
        public string AccountName { get; set; }
        public double CurrentBalance { get; set; }
        public double MonthlySpending { get; set; }
        public double MonthlyDeposits { get; set; }
        public int MonthlyTransactions { get; set; }
        public List<Transaction> TransactionsList { get; set; } = new List<Transaction>();
    }
}
