using System;
using MentalPeaceGuider.Models; // ensures User, Counselor, and BookingRequest are recognized

namespace MentalPeaceGuider.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public int CounselorID { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public string VideoCallLink { get; set; }
        public string Status { get; set; }

        public BookingRequest BookingRequest { get; set; }
        public User User { get; set; }
        public Counselor Counselor { get; set; }
    }
}
