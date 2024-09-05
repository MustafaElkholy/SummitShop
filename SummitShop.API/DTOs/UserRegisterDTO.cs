using System.ComponentModel.DataAnnotations;

namespace SummitShop.API.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{6,}$",
             ErrorMessage = "Password must be at least 6 characters long " +
                            "and contain at least one uppercase letter, one lowercase letter, one digit" +
                            ",and one non-alphanumeric character.")]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

    }
}
