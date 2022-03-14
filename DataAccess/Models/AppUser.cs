using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
