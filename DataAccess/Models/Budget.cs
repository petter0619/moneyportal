using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Budget
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public double GoalAmount { get; set; }
        [Required, MaxLength(50)]
        public string Interval { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<BudgetCategory> BudgetCategory { get; set; }
    }
}
