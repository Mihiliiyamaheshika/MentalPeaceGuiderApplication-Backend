using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MentalPeaceGuider.Data;
using MentalPeaceGuider.DTOs;
using MentalPeaceGuider.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MentalPeaceGuider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Allowed roles
            var allowedRoles = new[] { "User", "Counselor", "Admin" };

            // Hash the password
            string hashedPassword = HashPassword(dto.Password);

            var user = new Users
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = allowedRoles.Contains(dto.Role) ? dto.Role : "User" // default to "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!", role = user.Role });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto dto)
        {
            // 1️⃣ Try to find user in Users table
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user != null)
            {
                if (user.PasswordHash != HashPassword(dto.Password))
                    return Unauthorized(new { message = "Invalid email or password" });

                // Generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    token = jwtToken,
                    user = new
                    {
                        user.UserId,
                        user.FullName,
                        user.Email,
                        user.Role
                    }
                });
            }

            // 2️⃣ Try to find counselor in Counselors table
            var counselor = _context.Counselors.FirstOrDefault(c => c.Email == dto.Email);

            if (counselor != null)
            {
                if (counselor.PasswordHash != HashPassword(dto.Password))
                    return Unauthorized(new { message = "Invalid email or password" });

                // Generate JWT token for counselor
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, counselor.CounselorID.ToString()),
                new Claim(ClaimTypes.Email, counselor.Email),
                new Claim(ClaimTypes.Role, "counselor")
            }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    token = jwtToken,
                    user = new
                    {
                        userId = counselor.CounselorID,
                        fullName = counselor.FullName,
                        email = counselor.Email,
                        role = "counselor"
                    }
                });
            }

            // 3️⃣ If neither found
            return Unauthorized(new { message = "Invalid email or password" });
        }


        [HttpGet("users/byrole/{role}")]
        public IActionResult GetUsersByRole(string role)
        {
            var allowedRoles = new[] { "User", "Counselor", "Admin" };
            if (!allowedRoles.Contains(role))
                return BadRequest(new { message = "Invalid role specified." });

            var users = _context.Users
                .Where(u => u.Role == role)
                .ToList();

            return Ok(users);
        }

        // Simple SHA256 password hashing
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
