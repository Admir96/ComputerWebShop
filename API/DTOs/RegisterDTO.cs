using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? DayOfBirth { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Country { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string? Password { get; set; }

    }
}
