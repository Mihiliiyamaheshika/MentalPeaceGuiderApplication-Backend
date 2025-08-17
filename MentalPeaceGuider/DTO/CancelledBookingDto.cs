using System;
using System.ComponentModel.DataAnnotations;

namespace MentalPeaceGuider.Dtos
{
    public class CreateCancelledBookingDto
    {
        [Required]
        public int BookingID { get; set; }

        [Required]
        public string CancelledBy { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public DateTime CancelledAt { get; set; }
    }

    public class UpdateCancelledBookingDto
    {
        [Required]
        public string CancelledBy { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public DateTime CancelledAt { get; set; }
    }
}
