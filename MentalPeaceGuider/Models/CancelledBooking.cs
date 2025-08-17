using MentalPeaceGuider.Models; // ensures Booking is recognized
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class CancelledBooking
    {
        [Key]
        [Column("CancelId")]
        public int CancelID { get; set; }
        public int BookingID { get; set; }
        public string CancelledBy { get; set; }
        public string Reason { get; set; }
        public DateTime CancelledAt { get; set; } = DateTime.Now;

        public Booking Booking { get; set; }
    }
}
