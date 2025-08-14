using System;
using MentalPeaceGuider.Models; // ensures Booking is recognized

namespace MentalPeaceGuider.Models
{
    public class CancelledBooking
    {
        public int CancelID { get; set; }
        public int BookingID { get; set; }
        public string CancelledBy { get; set; }
        public string Reason { get; set; }
        public DateTime CancelledAt { get; set; } = DateTime.Now;

        public Booking Booking { get; set; }
    }
}
