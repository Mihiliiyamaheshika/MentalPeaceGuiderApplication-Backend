using System;

namespace MentalPeaceGuider.Dtos
{
    public class CreateRescheduleRequestDto
    {
        public int BookingId { get; set; }
        public DateTime ProposedDateTime { get; set; }
        public string Status { get; set; }
    }

    public class UpdateRescheduleRequestDto
    {
        public DateTime ProposedDateTime { get; set; }
        public string Status { get; set; }
    }
}
