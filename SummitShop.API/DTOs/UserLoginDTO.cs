using System.ComponentModel.DataAnnotations;

namespace SummitShop.API.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
