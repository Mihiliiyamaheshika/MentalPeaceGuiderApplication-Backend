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

        public string Message { get; set; }

        public string Status { get; set; } = "Pending"; // Default to Pending
    }
}
