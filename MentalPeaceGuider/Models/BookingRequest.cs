using MentalPeaceGuider.Models; // ensures User and Counselor are recognized
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class BookingRequest
    {
        [Key]
        [Column("RequestID")]
        public int RequestID { get; set; }

        public int UserID { get; set; }

        public int CounselorID { get; set; }

        public DateTime RequestedDateTime { get; set; }

        // Add EndDateTime to store the booking end time
        public DateTime? EndDateTime { get; set; }

        public string Message { get; set; }

        public string Status { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public Users Users { get; set; }

        [ForeignKey("CounselorID")]
        public Counselor Counselor { get; set; }
    }
}
