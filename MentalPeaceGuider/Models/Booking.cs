using MentalPeaceGuider.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class Booking
    {
        [Key]
        [Column("BookingID")]
        public int BookingID { get; set; }

        public int RequestID { get; set; }

        public int UserID { get; set; }

        public int CounselorID { get; set; }   

        public DateTime ScheduledDateTime { get; set; }

        // Nullable
        public string? VideoCallLink { get; set; }

        // Always required
        public string Status { get; set; } = string.Empty;

        public bool IsPaid { get; set; } = false;

        // Nullable
        public string? PaymentReference { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey("RequestID")]
        public BookingRequest BookingRequest { get; set; }

        [ForeignKey("UserID")]
        public Users Users { get; set; }

        [ForeignKey("CounselorID")]
        public Counselor Counselor { get; set; }
    }
}
