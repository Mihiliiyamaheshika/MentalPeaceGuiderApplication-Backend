namespace MentalPeaceGuider.DTOs
{
    public class UpdateSlotDto
    {
        public int SlotId { get; set; }
        public DateTime SlotDateTime { get; set; }
        public bool IsBooked { get; set; }
    }
}
