using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class Users
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "User"; // default role is "User"

        // Relationships
        public ICollection<BookingRequest> BookingRequests { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<ImmediateCall> ImmediateCalls { get; set; }
        public ICollection<ChatbotInteraction> ChatbotInteractions { get; set; }
        public ICollection<MentalHealthProgress> MentalHealthProgressRecords { get; set; }
    }
}
