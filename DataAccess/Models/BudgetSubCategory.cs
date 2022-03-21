using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class BudgetSubCategory
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public double GoalAmount { get; set; }

        public int BudgetCategoryId { get; set; }
        public Budget BudgetCategory { get; set; }

        public ICollection<Transaction> Transaction { get; set; }
    }
}
