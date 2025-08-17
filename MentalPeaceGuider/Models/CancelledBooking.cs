using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class CancelledBooking
    {
        [Key]
        [Column("CancelID")]
        public int CancelID { get; set; }

        [ForeignKey("Booking")]
        public int BookingID { get; set; }

        public string CancelledBy { get; set; }   // e.g., "User" / "Counselor" / "Admin"
        public string Reason { get; set; }
        public DateTime CancelledAt { get; set; }

        // Navigation property
        public Booking Booking { get; set; }
    }
}
