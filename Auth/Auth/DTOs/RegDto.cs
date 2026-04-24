using System.ComponentModel.DataAnnotations;

namespace Auth.Auth.DTOs
{
    public class RegDto
    {
        [Required]
        public string Name { get; set;}

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(10,MinimumLength =4)]
        public string Username { get; set; }

        [Required]
        [StringLength(8)]
        public string Password { get; set; }

        [Required]
        [StringLength(8)]
        public string ConfPassword { get; set; }
    }
}
