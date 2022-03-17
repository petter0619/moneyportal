using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public double StartingBalance { get; set; }
        [Required]
        public string Type { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
