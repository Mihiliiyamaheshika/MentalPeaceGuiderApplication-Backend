using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentStatus { get; set; } = string.Empty;

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        // Navigation Properties
        [ForeignKey("BookingID")]
        public Booking Booking { get; set; }

        [ForeignKey("UserID")]
        public Users User { get; set; }
    }
}
