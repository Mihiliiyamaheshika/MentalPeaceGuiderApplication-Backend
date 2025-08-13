public class Payment
{
    public int PaymentID { get; set; }
    public int BookingID { get; set; }
    public int UserID { get; set; }
    public decimal Amount { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    public Booking Booking { get; set; }
    public User User { get; set; }
}
