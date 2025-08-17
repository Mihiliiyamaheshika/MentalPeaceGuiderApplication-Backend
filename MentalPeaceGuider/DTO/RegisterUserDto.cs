namespace MentalPeaceGuider.DTOs
{
    public class RegisterUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        // Accept plain password from client
        public string Password { get; set; }

        // Optional: "User", "Counselor", "Admin"
        public string Role { get; set; }
    }
}
