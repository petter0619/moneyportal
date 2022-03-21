
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required, MaxLength(500)]
        public string Memo { get; set; }
        [Required]
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        [Required, MaxLength(25)]
        public string Type { get; set; }
        [Required, MaxLength(100)]
        public string Store { get; set; }
        public bool IsPaid { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
