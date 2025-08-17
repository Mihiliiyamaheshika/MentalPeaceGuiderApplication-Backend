using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class RescheduleRequest
    {
        [Key]
        [Column("RescheduleId")]
        public int RescheduleId { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }

        public DateTime ProposedDateTime { get; set; }

        public string Status { get; set; }

        // Navigation property
        public Booking Booking { get; set; }
    }
}
