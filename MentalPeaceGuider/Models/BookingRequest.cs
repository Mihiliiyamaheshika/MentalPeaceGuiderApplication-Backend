public class BookingRequest
{
    public int RequestID { get; set; }
    public int UserID { get; set; }
    public int CounselorID { get; set; }
    public DateTime RequestedDateTime { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }

    public User User { get; set; }
    public Counselor Counselor { get; set; }
}
