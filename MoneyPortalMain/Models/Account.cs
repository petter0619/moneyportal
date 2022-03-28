namespace MoneyPortalMain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double StartingBalance { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
