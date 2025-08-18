using System;

namespace MentalPeaceGuider.Dtos
{
    // For creating a new immediate call request
    public class ImmediateCallCreateDto
    {
        public int UserID { get; set; }
        public int CounselorID { get; set; }

        // New fields added
        public DateTime? CallRequestedAt { get; set; }  // optional in request
        public string? Status { get; set; }             // optional in request
    }

    // For updating status (example: counselor accepts or ends call)
    public class ImmediateCallUpdateDto
    {
        public string Status { get; set; }
    }

    // For returning call info to frontend
    public class ImmediateCallReadDto
    {
        public int CallID { get; set; }
        public int UserID { get; set; }
        public int CounselorID { get; set; }
        public DateTime CallRequestedAt { get; set; }
        public string Status { get; set; }
    }
}
