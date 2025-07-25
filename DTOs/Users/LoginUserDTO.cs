using System.ComponentModel.DataAnnotations;

namespace nastrafarmapi.DTOs.Users
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
