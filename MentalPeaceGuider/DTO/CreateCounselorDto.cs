namespace MentalPeaceGuider.Dtos
{
    public class CreateCounselorDto
    {
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // plain password, will be hashed before saving
        public string ProfileName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
