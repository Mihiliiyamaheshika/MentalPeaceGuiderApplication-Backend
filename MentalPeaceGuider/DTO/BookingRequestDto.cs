using System;

namespace MentalPeaceGuider.Dtos
{
    public class BookingRequestDto
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public int CounselorID { get; set; }
        public DateTime RequestedDateTime { get; set; }
        public string? Message { get; set; }
        public string Status { get; set; } = "Pending";

        // Optional: include counselor name for frontend
        public string? CounselorName { get; set; }
    }
}
