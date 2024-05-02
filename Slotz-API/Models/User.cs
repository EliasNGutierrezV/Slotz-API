using System.ComponentModel.DataAnnotations;

namespace Slotz_API.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        public string Email { get; set; }
        
        public int Status { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
