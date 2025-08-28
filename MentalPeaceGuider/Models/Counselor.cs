using MentalPeaceGuider.Models; // ensures related models are recognized
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class Counselor
    {
        [Key]
        [Column("CounselorId")]
        public int CounselorID { get; set; }

        public string Title { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ProfileName { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        // New property for availability
        public string AvailabilityDays { get; set; }

        public ICollection<BookingRequest> BookingRequests { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<ImmediateCall> ImmediateCalls { get; set; }
        public ICollection<AvailableSlot> AvailableSlots { get; set; }
    }
}
