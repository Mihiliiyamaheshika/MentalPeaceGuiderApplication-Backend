using Microsoft.AspNetCore.Mvc;
using MentalPeaceGuider.Data;
using MentalPeaceGuider.Models;
using MentalPeaceGuider.Dtos;
using Microsoft.EntityFrameworkCore;

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
                EndDateTime = dto.EndDateTime ?? dto.RequestedDateTime.AddHours(2), // ✅ FIX: set default
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
        // GET: api/BookingRequests/user/{userId}
        [HttpGet("user/{userId}")]
        public IActionResult GetRequestsByUser(int userId)
        {
            var requests = _context.BookingRequests
                .Where(r => r.UserID == userId)
                .Select(r => new BookingRequestDto
                {
                    RequestID = r.RequestID,
                    UserID = r.UserID,
                    CounselorID = r.CounselorID,
                    RequestedDateTime = r.RequestedDateTime,
                    Message = r.Message,
                    Status = r.Status,
                    CounselorName = r.Counselor != null ? r.Counselor.FullName : null
                })
                .ToList();

            if (!requests.Any())
                return NotFound(new { message = "No booking requests found for this user" });

            return Ok(requests);
        }


        [HttpGet("counselor/{counselorId}")]
        public async Task<IActionResult> GetCounselorBookings(int counselorId)
        {
            var bookings = await _context.BookingRequests
                .Where(b => b.CounselorID == counselorId)
                .Select(b => new
                {
                    b.RequestID,
                    b.UserID,
                    UserName = b.Users.FullName, // assuming navigation property exists
                    b.CounselorID,
                    CounselorName = b.Counselor.FullName, // assuming navigation property
                    RequestedDateTime = b.RequestedDateTime,
                    EndDateTime = b.EndDateTime ?? b.RequestedDateTime.AddHours(1), // default 1 hour if EndDateTime null
                    b.Message,
                    b.Status
                })
                .ToListAsync();

            return Ok(bookings);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var request = await _context.BookingRequests.FindAsync(id);
            if (request == null)
                return NotFound();

            request.Status = status; // e.g., "Confirmed"
            _context.BookingRequests.Update(request);
            await _context.SaveChangesAsync();

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

