using System.ComponentModel.DataAnnotations;

namespace Slotz_API.Models
{
    public class ServicePeriod
    {
        [Key]
        public int IdServicePeriod { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeOnly StartHour { get; set; }
        [Required]
        public TimeOnly EndHour { get; set; }
        [Required]
        public decimal Price { get; set; }

        public int? ClientRating { get; set; }
        public int? BidderRating { get; set; }
    }
}
