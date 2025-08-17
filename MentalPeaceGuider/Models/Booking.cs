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

        public string VideoCallLink { get; set; }

        public string Status { get; set; }

        [ForeignKey("RequestID")]
        public BookingRequest BookingRequest { get; set; }

        [ForeignKey("UserID")]
        public Users Users { get; set; }

        [ForeignKey("CounselorID")]
        public Counselor Counselor { get; set; }
    }
}
