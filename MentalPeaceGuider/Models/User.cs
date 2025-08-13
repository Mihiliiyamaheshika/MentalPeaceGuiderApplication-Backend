using System;

public class User
{
    public int UserID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    // Relationships
    public ICollection<BookingRequest> BookingRequests { get; set; }
    public ICollection<Booking> Bookings { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public ICollection<ImmediateCall> ImmediateCalls { get; set; }
    public ICollection<ChatbotInteraction> ChatbotInteractions { get; set; }
    public ICollection<MentalHealthProgress> MentalHealthProgressRecords { get; set; }
}
