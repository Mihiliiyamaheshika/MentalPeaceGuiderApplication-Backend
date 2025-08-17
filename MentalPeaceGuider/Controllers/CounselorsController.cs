using MentalPeaceGuider.Data;
using MentalPeaceGuider.Dtos;
using MentalPeaceGuider.DTOs;
using MentalPeaceGuider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // ✅ GET all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CounselorDto>>> GetCounselors()
        {
            return await _context.Counselors
                .Select(c => new CounselorDto
                {
                    CounselorID = c.CounselorID,
                    Title = c.Title,
                    FullName = c.FullName,
                    Gender = c.Gender,
                    Email = c.Email,
                    ProfileName = c.ProfileName,
                    Description = c.Description
                })
                .ToListAsync();
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
                Title = counselor.Title,
                FullName = counselor.FullName,
                Gender = counselor.Gender,
                Email = counselor.Email,
                ProfileName = counselor.ProfileName,
                Description = counselor.Description
            };
        }

        // ✅ POST (Create)
        [HttpPost]
        public async Task<ActionResult<CounselorDto>> CreateCounselor(CreateCounselorDto dto)
        {
            var counselor = new Counselor
            {
                Title = dto.Title,
                FullName = dto.FullName,
                Gender = dto.Gender,
                Email = dto.Email,
                PasswordHash = dto.Password,
                ProfileName = dto.ProfileName,
                Description = dto.Description
            };

            _context.Counselors.Add(counselor);
            await _context.SaveChangesAsync();

            // return created counselor as DTO
            var result = new CounselorDto
            {
                CounselorID = counselor.CounselorID,
                Title = counselor.Title,
                FullName = counselor.FullName,
                Gender = counselor.Gender,
                Email = counselor.Email,
                ProfileName = counselor.ProfileName,
                Description = counselor.Description
            };

            return CreatedAtAction(nameof(GetCounselor), new { id = counselor.CounselorID }, result);
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
    }
}
