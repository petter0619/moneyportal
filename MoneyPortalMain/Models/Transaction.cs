namespace MoneyPortalMain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Memo { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Store { get; set; }
    }
}
