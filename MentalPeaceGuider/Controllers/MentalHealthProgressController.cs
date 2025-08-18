using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentalPeaceGuider.Data;
using MentalPeaceGuider.Models;
using MentalPeaceGuider.Dtos;

namespace MentalPeaceGuider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentalHealthProgressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MentalHealthProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MentalHealthProgress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MentalHealthProgressDto>>> GetAll()
        {
            var progressList = await _context.MentalHealthProgress
                .Select(p => new MentalHealthProgressDto
                {
                    ProgressID = p.ProgressID,
                    UserID = p.UserID,
                    DateRecorded = p.DateRecorded,
                    MoodLevel = p.MoodLevel,
                    Notes = p.Notes
                })
                .ToListAsync();

            return Ok(progressList);
        }

        // GET: api/MentalHealthProgress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MentalHealthProgressDto>> GetById(int id)
        {
            var progress = await _context.MentalHealthProgress.FindAsync(id);

            if (progress == null)
                return NotFound();

            return new MentalHealthProgressDto
            {
                ProgressID = progress.ProgressID,
                UserID = progress.UserID,
                DateRecorded = progress.DateRecorded,
                MoodLevel = progress.MoodLevel,
                Notes = progress.Notes
            };
        }

        // POST: api/MentalHealthProgress
        [HttpPost]
        public async Task<ActionResult<MentalHealthProgressDto>> Create(CreateMentalHealthProgressDto dto)
        {
            var progress = new MentalHealthProgress
            {
                UserID = dto.UserID,
                MoodLevel = dto.MoodLevel,
                Notes = dto.Notes,
                DateRecorded = DateTime.Now
            };

            _context.MentalHealthProgress.Add(progress);
            await _context.SaveChangesAsync();

            var result = new MentalHealthProgressDto
            {
                ProgressID = progress.ProgressID,
                UserID = progress.UserID,
                DateRecorded = progress.DateRecorded,
                MoodLevel = progress.MoodLevel,
                Notes = progress.Notes
            };

            return CreatedAtAction(nameof(GetById), new { id = progress.ProgressID }, result);
        }

        // PUT: api/MentalHealthProgress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMentalHealthProgressDto dto)
        {
            var progress = await _context.MentalHealthProgress.FindAsync(id);
            if (progress == null)
                return NotFound();

            progress.MoodLevel = dto.MoodLevel;
            progress.Notes = dto.Notes;

            _context.Entry(progress).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/MentalHealthProgress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var progress = await _context.MentalHealthProgress.FindAsync(id);
            if (progress == null)
                return NotFound();

            _context.MentalHealthProgress.Remove(progress);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
