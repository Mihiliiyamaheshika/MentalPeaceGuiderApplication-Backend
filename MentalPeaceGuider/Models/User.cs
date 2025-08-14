using System;
using System.Collections.Generic;

namespace MentalPeaceGuider.Models
{

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        // Relationships
        public ICollection<BookingRequest> BookingRequests { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<ImmediateCall> ImmediateCalls { get; set; }
        public ICollection<ChatbotInteraction> ChatbotInteractions { get; set; }
        public ICollection<MentalHealthProgress> MentalHealthProgressRecords { get; set; }
    }
}