using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slotz_API.Models
{
    public class Vehicle
    {
        [Key]
        public int IdVehicle { get; set; }
        [Required]
        public decimal Height { get; set; }
        [Required]
        public decimal Width { get; set; }
        [Required]
        public decimal Long { get; set; }
        [Required]
        public string Plate { get; set; }

        public int Status { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }
        
        public DateTime UpdateDate { get; set; }
        public string? Description { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
    }
}
