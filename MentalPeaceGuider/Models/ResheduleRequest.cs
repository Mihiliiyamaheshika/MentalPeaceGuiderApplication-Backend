using MentalPeaceGuider.Models; // ensures Booking is recognized
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class RescheduleRequest
    {
        [Key]
        [Column("RescheduleId")]
        public int RescheduleID { get; set; }
        public int BookingID { get; set; }
        public DateTime ProposedDateTime { get; set; }
        public string Status { get; set; }

        public Booking Booking { get; set; }
    }
}

