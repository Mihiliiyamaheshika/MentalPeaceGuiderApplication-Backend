public class AvailableSlot
{
    public int SlotID { get; set; }
    public int CounselorID { get; set; }
    public DateTime SlotDateTime { get; set; }
    public bool IsBooked { get; set; }

    public Counselor Counselor { get; set; }
}
