using MentalPeaceGuider.Data;
using MentalPeaceGuider.Dtos;
using MentalPeaceGuider.DTOs;
using MentalPeaceGuider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace MentalPeaceGuider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounselorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CounselorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET all counselors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CounselorDto>>> GetCounselors()
        {
            var counselors = await _context.Counselors
                .Select(c => new CounselorDto
                {
                    CounselorID = c.CounselorID,
                    Title = c.Title ?? "",
                    FullName = c.FullName ?? "",
                    Gender = c.Gender ?? "",
                    Email = c.Email ?? "",
                    ProfileName = c.ProfileName ?? "",
                    Description = c.Description ?? "",
                    ImageUrl = c.ImageUrl ?? "",
                    AvailabilityDays = c.AvailabilityDays ?? "" // include availability
                })
                .ToListAsync();

            if (!counselors.Any())
                return NotFound();

            return Ok(counselors);
        }

        // ✅ GET by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CounselorDto>> GetCounselor(int id)
        {
            var counselor = await _context.Counselors.FindAsync(id);

            if (counselor == null)
                return NotFound();

            return new CounselorDto
            {
                CounselorID = counselor.CounselorID,
                Title = counselor.Title ?? "",
                FullName = counselor.FullName ?? "",
                Gender = counselor.Gender ?? "",
                Email = counselor.Email ?? "",
                ProfileName = counselor.ProfileName ?? "",
                Description = counselor.Description ?? "",
                ImageUrl = counselor.ImageUrl ?? "",
                AvailabilityDays = counselor.AvailabilityDays ?? "" // include availability
            };
        }

        // ✅ POST (Create) – for admin/system use
        [HttpPost]
        public async Task<ActionResult<CounselorDto>> CreateCounselor(CreateCounselorDto dto)
        {
            string hashedPassword = HashPassword(dto.Password);

            var counselor = new Counselor
            {
                Title = dto.Title,
                FullName = dto.FullName,
                Gender = dto.Gender,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                ProfileName = dto.ProfileName,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                AvailabilityDays = dto.AvailabilityDays // handle availability
            };

            _context.Counselors.Add(counselor);
            await _context.SaveChangesAsync();

            var result = new CounselorDto
            {
                CounselorID = counselor.CounselorID,
                Title = counselor.Title ?? "",
                FullName = counselor.FullName ?? "",
                Gender = counselor.Gender ?? "",
                Email = counselor.Email ?? "",
                ProfileName = counselor.ProfileName ?? "",
                Description = counselor.Description ?? "",
                ImageUrl = counselor.ImageUrl ?? "",
                AvailabilityDays = counselor.AvailabilityDays ?? ""
            };

            return CreatedAtAction(nameof(GetCounselor), new { id = counselor.CounselorID }, result);
        }

        // ✅ POST (Signup) – for counselors
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] CreateCounselorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var counselor = new Counselor
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Title = dto.Title,
                Gender = dto.Gender,
                ProfileName = dto.ProfileName,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                AvailabilityDays = string.Join(",", dto.AvailabilityDays) // store as comma-separated string
            };

            _context.Counselors.Add(counselor);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Counselor registered successfully" });
        }

        // ✅ PUT (Update)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCounselor(int id, UpdateCounselorDto dto)
        {
            var counselor = await _context.Counselors.FindAsync(id);
            if (counselor == null)
                return NotFound();

            counselor.Title = dto.Title;
            counselor.FullName = dto.FullName;
            counselor.Gender = dto.Gender;
            counselor.Email = dto.Email;
            counselor.ProfileName = dto.ProfileName;
            counselor.Description = dto.Description;
            counselor.ImageUrl = dto.ImageUrl;
            counselor.AvailabilityDays = dto.AvailabilityDays; // allow updates to availability

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounselor(int id)
        {
            var counselor = await _context.Counselors.FindAsync(id);
            if (counselor == null)
                return NotFound();

            _context.Counselors.Remove(counselor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 🔒 Password hashing helper
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
