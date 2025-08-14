using System;
using MentalPeaceGuider.Models; // ensures Booking is recognized

namespace MentalPeaceGuider.Models
{
    public class RescheduleRequest
    {
        public int RescheduleID { get; set; }
        public int BookingID { get; set; }
        public DateTime ProposedDateTime { get; set; }
        public string Status { get; set; }

        public Booking Booking { get; set; }
    }
}

