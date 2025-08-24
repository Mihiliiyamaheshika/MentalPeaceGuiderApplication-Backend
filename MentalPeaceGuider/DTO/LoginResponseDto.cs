namespace MentalPeaceGuider.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public int Id { get; set; }             // main id from user table
        public int? UserId { get; set; }        // filled if role = User
        public int? CounselorId { get; set; }   // filled if role = Counselor
        public string Role { get; set; }
        public string FullName { get; set; }
    }
}
