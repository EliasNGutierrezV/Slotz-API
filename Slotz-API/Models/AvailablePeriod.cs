using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slotz_API.Models
{
    public class AvailablePeriod
    {
        [Key]
        public int IdPeriod { get; set; }
        [Required]
        public TimeOnly StartHour { get; set; }
        [Required]
        public TimeOnly EndHour { get; set;}
        
        public int Status { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("SpaceGarage")]
        public int IdSpaceGarage { get; set; }
    }
}
