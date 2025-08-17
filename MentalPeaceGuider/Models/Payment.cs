using MentalPeaceGuider.Models; // Ensure this points to your User and Booking classes
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class Payment
    {
        [Key]
        [Column("PaymentId")]
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public Booking Booking { get; set; }
        public Users Users { get; set; }
    }
}
