using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slotz_API.Models
{
    public class Garage
    {
        [Key]
        public int IdGarage { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Description { get; set; }

        public int? Status { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
    }
}
