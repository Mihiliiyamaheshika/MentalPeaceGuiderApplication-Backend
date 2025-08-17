namespace MentalPeaceGuider.DTOs
{
    public class CreateBookingDto
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public int CounselorID { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public string VideoCallLink { get; set; }
        public string Status { get; set; } // e.g., "Scheduled", "Completed", "Cancelled"
    }
}
