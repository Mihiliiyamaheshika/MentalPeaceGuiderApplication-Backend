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
    public class BookingRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/BookingRequests
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] CreateBookingRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check foreign keys exist
            var userExists = _context.Users.Any(u => u.UserId == dto.UserID);
            var counselorExists = _context.Counselors.Any(c => c.CounselorID == dto.CounselorID);

            if (!userExists || !counselorExists)
                return BadRequest(new { message = "Invalid UserID or CounselorID" });

            var request = new BookingRequest
            {
                UserID = dto.UserID,
                CounselorID = dto.CounselorID,
                RequestedDateTime = dto.RequestedDateTime,
                Message = dto.Message,
                Status = dto.Status
            };

            _context.BookingRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking request created successfully!", requestID = request.RequestID });
        }

        [HttpGet("{id}")]
        public IActionResult GetRequestById(int id)
        {
            var request = _context.BookingRequests.FirstOrDefault(r => r.RequestID == id);
            if (request == null)
                return NotFound(new { message = "Booking request not found" });

            return Ok(request);
        }

        // PUT: api/BookingRequests/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] CreateBookingRequestDto dto)
        {
            var request = _context.BookingRequests.FirstOrDefault(r => r.RequestID == id);
            if (request == null)
                return NotFound(new { message = "Booking request not found" });

            // Update fields
            request.UserID = dto.UserID;
            request.CounselorID = dto.CounselorID;
            request.RequestedDateTime = dto.RequestedDateTime;
            request.Message = dto.Message;
            request.Status = dto.Status;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Booking request updated successfully!" });
        }

        // DELETE: api/BookingRequests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = _context.BookingRequests.FirstOrDefault(r => r.RequestID == id);
            if (request == null)
                return NotFound(new { message = "Booking request not found" });

            _context.BookingRequests.Remove(request);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Booking request deleted successfully!" });
        }
    }
}

