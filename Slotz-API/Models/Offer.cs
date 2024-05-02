using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slotz_API.Models
{
    public class Offer
    {
        [Key]
        public int IdOffer { get; set; }
        [ForeignKey("User")]
        public int IdClient { get; set; }
        [ForeignKey("User")]
        public int IdBidder { get; set; }
        [ForeignKey("AvailablePeriod")]
        public int IdAvailablePeriod { get; set; }
        [ForeignKey("IdVehicle")]
        public int IdVehicle { get; set; }
        [Required]
        public decimal LastOffer {  get; set; }

        public string? Decline {  get; set; }
        public int Status { get; set; }

        [ForeignKey("ServicePeriod")]
        public int? IdServicePeriod { get; set; }
    }
}
