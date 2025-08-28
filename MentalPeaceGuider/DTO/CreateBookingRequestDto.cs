using System;
using System.ComponentModel.DataAnnotations;

namespace MentalPeaceGuider.Dtos
{
    public class CreateBookingRequestDto
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int CounselorID { get; set; }

        [Required]
        public DateTime RequestedDateTime { get; set; }

        //  New field for End DateTime 
        public DateTime? EndDateTime { get; set; }

        public string? Message { get; set; }  // Nullable to allow empty messages

        public string Status { get; set; } = "Pending"; // Default to Pending
    }
}
