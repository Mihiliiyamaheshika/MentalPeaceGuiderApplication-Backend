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

        public string Message { get; set; }

        public string Status { get; set; }

        [ForeignKey("UserID")]
        public Users Users { get; set; }

        [ForeignKey("CounselorID")]
        public Counselor Counselor { get; set; }
    }
}
