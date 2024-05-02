using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slotz_API.Models
{
    public class SpaceGarage
    {
        [Key]
        public int IdSpaceGarage { get; set; }
        [Required]
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        [Required]
        public decimal Long { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Status { get; set; }
        [ForeignKey("Garage")]
        public int IdGarage { get; set; }
    }
}
