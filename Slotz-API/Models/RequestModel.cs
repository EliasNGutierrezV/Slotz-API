using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Slotz_API.Models
{
    public class RequestModel
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? Token { get; set; }
    }
}

