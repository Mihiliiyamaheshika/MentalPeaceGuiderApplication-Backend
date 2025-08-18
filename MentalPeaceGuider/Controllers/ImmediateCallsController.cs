using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentalPeaceGuider.Data;
using MentalPeaceGuider.Models;
using MentalPeaceGuider.Dtos;

namespace MentalPeaceGuider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImmediateCallsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImmediateCallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/ImmediateCalls
        [HttpPost]
        public async Task<ActionResult<ImmediateCallReadDto>> CreateImmediateCall(ImmediateCallCreateDto dto)
        {
            var call = new ImmediateCall
            {
                UserID = dto.UserID,
                CounselorID = dto.CounselorID,
                CallRequestedAt = dto.CallRequestedAt ?? DateTime.UtcNow,  // default to now if not provided
                Status = !string.IsNullOrEmpty(dto.Status) ? dto.Status : "Pending"
            };

            _context.ImmediateCalls.Add(call);
            await _context.SaveChangesAsync();

            return new ImmediateCallReadDto
            {
                CallID = call.CallID,
                UserID = call.UserID,
                CounselorID = call.CounselorID,
                CallRequestedAt = call.CallRequestedAt,
                Status = call.Status
            };
        }

        // PUT: api/ImmediateCalls/5/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateCallStatus(int id, ImmediateCallUpdateDto dto)
        {
            var call = await _context.ImmediateCalls.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            call.Status = dto.Status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/ImmediateCalls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImmediateCallReadDto>>> GetImmediateCalls()
        {
            return await _context.ImmediateCalls
                .Select(call => new ImmediateCallReadDto
                {
                    CallID = call.CallID,
                    UserID = call.UserID,
                    CounselorID = call.CounselorID,
                    CallRequestedAt = call.CallRequestedAt,
                    Status = call.Status
                }).ToListAsync();
        }
        // ✅ GET: api/ImmediateCalls/5 (now shows parameter clearly in Swagger)
        [HttpGet("{id}")]
        public async Task<ActionResult<ImmediateCallReadDto>> GetImmediateCall([FromRoute] int id)
        {
            var call = await _context.ImmediateCalls.FindAsync(id);

            if (call == null)
            {
                return NotFound();
            }

            return new ImmediateCallReadDto
            {
                CallID = call.CallID,
                UserID = call.UserID,
                CounselorID = call.CounselorID,
                CallRequestedAt = call.CallRequestedAt,
                Status = call.Status
            };
        }
    }
}
