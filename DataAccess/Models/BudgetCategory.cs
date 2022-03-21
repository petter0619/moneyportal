using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class BudgetCategory
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public double GoalAmount { get; set; }

        public int BudgetId { get; set; }
        public Budget Budget { get; set; }

        public ICollection<BudgetSubCategory> BudgetSubCategory { get; set; }
    }
}
