namespace MentalPeaceGuider.Dtos
{
    public class CounselorDto
    {
        public int CounselorID { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ProfileName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        // ✅ New field for availability days (stored as comma-separated string or JSON)
        public string AvailabilityDays { get; set; }
    }
}
