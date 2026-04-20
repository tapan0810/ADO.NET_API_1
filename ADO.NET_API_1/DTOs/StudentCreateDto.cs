using System.ComponentModel.DataAnnotations;

namespace ADO.NET_API_1.DTOs
{
    public class StudentCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Range(1,150)]
        public int Age { get; set; }

        [Required]
        [MaxLength(100)]
        public string Department { get; set; } = string.Empty;
    }
}
