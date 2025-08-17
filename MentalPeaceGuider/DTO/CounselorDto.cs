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
        // Notice: No PasswordHash here!
    }
}
