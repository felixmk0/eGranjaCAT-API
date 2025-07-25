using nastrafarmapi.Entities;
using System.ComponentModel.DataAnnotations;

namespace nastrafarmapi.DTOs.Users
{
    public class CreateUserDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Lastname { get; set; }
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        [EnumDataType(typeof(UserRole))]
        public required string Role { get; set; } 
    }
}
