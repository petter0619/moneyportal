
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string AuthId { get; set; }

        public ICollection<Account> Account { get; set; }
    }
}
