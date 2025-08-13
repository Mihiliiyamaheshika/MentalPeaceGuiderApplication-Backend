public class ImmediateCall
{
    public int CallID { get; set; }
    public int UserID { get; set; }
    public int CounselorID { get; set; }
    public DateTime CallRequestedAt { get; set; } = DateTime.Now;
    public string Status { get; set; }

    public User User { get; set; }
    public Counselor Counselor { get; set; }
}
