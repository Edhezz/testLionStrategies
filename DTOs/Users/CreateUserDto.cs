using System.ComponentModel.DataAnnotations;

namespace LionStrategiesTest.DTOs.Users
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(admin|user)$", ErrorMessage = "Role must be 'admin' or 'user'")]
        public string Role { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}