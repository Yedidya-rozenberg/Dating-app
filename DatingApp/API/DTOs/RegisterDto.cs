using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8,MinimumLength = 4, ErrorMessage = "You mast specifity password between 4 and 8 char")]
        public string Password { get; set; }
    }
}