using Microsoft.AspNetCore.Mvc;
using MentalPeaceGuider.Data;
using MentalPeaceGuider.Models;
using MentalPeaceGuider.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace MentalPeaceGuider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RescheduleRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RescheduleRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/RescheduleRequests
        [HttpPost]
        public async Task<IActionResult> CreateReschedule([FromBody] CreateRescheduleRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check booking exists
            var bookingExists = _context.Bookings.Any(b => b.BookingID == dto.BookingId);
            if (!bookingExists)
                return BadRequest(new { message = "Invalid BookingId" });

            var reschedule = new RescheduleRequest
            {
                BookingId = dto.BookingId,
                ProposedDateTime = dto.ProposedDateTime,
                Status = dto.Status
            };

            _context.RescheduleRequests.Add(reschedule);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Reschedule request created successfully", rescheduleId = reschedule.RescheduleId });
        }

        // GET: api/RescheduleRequests/{id}
        [HttpGet("{id}")]
        public IActionResult GetRescheduleById(int id)
        {
            var reschedule = _context.RescheduleRequests.FirstOrDefault(r => r.RescheduleId == id);
            if (reschedule == null)
                return NotFound(new { message = "Reschedule request not found" });

            return Ok(reschedule);
        }

        // PUT: api/RescheduleRequests/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReschedule(int id, [FromBody] UpdateRescheduleRequestDto dto)
        {
            var reschedule = _context.RescheduleRequests.FirstOrDefault(r => r.RescheduleId == id);
            if (reschedule == null)
                return NotFound(new { message = "Reschedule request not found" });

            reschedule.ProposedDateTime = dto.ProposedDateTime;
            reschedule.Status = dto.Status;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Reschedule request updated successfully" });
        }

        // DELETE: api/RescheduleRequests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReschedule(int id)
        {
            var reschedule = _context.RescheduleRequests.FirstOrDefault(r => r.RescheduleId == id);
            if (reschedule == null)
                return NotFound(new { message = "Reschedule request not found" });

            _context.RescheduleRequests.Remove(reschedule);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Reschedule request deleted successfully" });
        }
    }
}
