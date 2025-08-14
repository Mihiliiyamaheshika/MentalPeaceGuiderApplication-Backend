using System;
using System.Collections.Generic;
using MentalPeaceGuider.Models; // ensures related models are recognized

namespace MentalPeaceGuider.Models
{
    public class Counselor
    {
        public int CounselorID { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ProfileName { get; set; }
        public string Description { get; set; }

        public ICollection<BookingRequest> BookingRequests { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<ImmediateCall> ImmediateCalls { get; set; }
        public ICollection<AvailableSlot> AvailableSlots { get; set; }
    }
}

