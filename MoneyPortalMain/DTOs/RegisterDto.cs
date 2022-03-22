using System.ComponentModel.DataAnnotations;

namespace MoneyPortalMain.DTOs
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, RegularExpression(
            "(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$", 
            ErrorMessage = "Password must be longer than 8 characters and contain a number, a lower case and a upper case character")
        ]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(50)]
        public string? DisplayName { get; set; }
    }
}
