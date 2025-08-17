using MentalPeaceGuider.Data;
using MentalPeaceGuider.Dtos;
using MentalPeaceGuider.DTOs;
using MentalPeaceGuider.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MentalPeaceGuider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Create booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = _context.Users.Any(u => u.UserId == dto.UserID);
            var counselorExists = _context.Counselors.Any(c => c.CounselorID == dto.CounselorID);
            var requestExists = _context.BookingRequests.Any(r => r.RequestID == dto.RequestID);

            if (!userExists || !counselorExists || !requestExists)
                return BadRequest(new { message = "Invalid UserID, CounselorID, or RequestID" });

            var booking = new Booking
            {
                UserID = dto.UserID,
                CounselorID = dto.CounselorID,
                RequestID = dto.RequestID,
                ScheduledDateTime = dto.ScheduledDateTime,
                VideoCallLink = dto.VideoCallLink,
                Status = dto.Status
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking created successfully!", bookingID = booking.BookingID });
        }

        // GET: All bookings
        [HttpGet]
        public IActionResult GetBookings()
        {
            var bookings = _context.Bookings.ToList();
            return Ok(bookings);
        }

        // GET: Booking by ID
        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            return Ok(booking);
        }

        // PUT: Update booking
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] CreateBookingDto dto)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            // Check foreign keys
            if (!_context.Users.Any(u => u.UserId == dto.UserID) ||
                !_context.Counselors.Any(c => c.CounselorID == dto.CounselorID) ||
                !_context.BookingRequests.Any(r => r.RequestID == dto.RequestID))
            {
                return BadRequest(new { message = "Invalid UserID, CounselorID, or RequestID" });
            }

            booking.UserID = dto.UserID;
            booking.CounselorID = dto.CounselorID;
            booking.RequestID = dto.RequestID;
            booking.ScheduledDateTime = dto.ScheduledDateTime;
            booking.VideoCallLink = dto.VideoCallLink;
            booking.Status = dto.Status;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Booking updated successfully!" });
        }

        // DELETE: Delete booking
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking deleted successfully!" });
        }
    }
}
