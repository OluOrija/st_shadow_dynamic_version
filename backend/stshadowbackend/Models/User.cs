using System.ComponentModel.DataAnnotations;

namespace stshadowbackend.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        public string? Role { get; set; }
    }
}
