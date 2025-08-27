using MentalPeaceGuider.Data;
using MentalPeaceGuider.Dtos;
using MentalPeaceGuider.DTOs;
using MentalPeaceGuider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var booking = _context.Bookings
                .Where(b => b.BookingID == id)
                .Select(b => new
                {
                    b.BookingID,
                    b.RequestID,
                    b.UserID,
                    b.CounselorID,
                    ScheduledDateTime = b.ScheduledDateTime,
                    EndDateTime = b.ScheduledDateTime.AddHours(1), // optional end time
                    b.VideoCallLink,
                    b.Status,
                    IsPaid = b.IsPaid,
                    PaymentReference = b.PaymentReference ?? "", // safe null handling
                    UserName = b.Users != null ? b.Users.FullName : "", // corrected property
                    CounselorName = b.Counselor != null ? b.Counselor.FullName : "" // corrected property
                })
                .FirstOrDefault();

            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            return Ok(booking);
        }

        // GET: api/Bookings/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetConfirmedBookingsByUser(int userId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.UserID == userId && b.Status == "Confirmed")
                .ToListAsync();

            if (!bookings.Any())
                return NotFound("No confirmed bookings found.");

            return Ok(bookings);
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

        // ------------------- NEW FUNCTIONALITIES -------------------

        // PUT: Confirm Booking
        [HttpPut("{id}/confirm")]
        public async Task<IActionResult> ConfirmBooking(int id, [FromBody] string videoCallLink)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            booking.Status = "Confirmed";
            booking.VideoCallLink = videoCallLink; // optional
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking confirmed successfully!" });
        }

        // PUT: Mark as Paid
        [HttpPut("{id}/pay")]
        public async Task<IActionResult> PayBooking(int id, [FromBody] string paymentReference)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            // 1️⃣ Mark as paid
            booking.IsPaid = true;
            booking.PaymentReference = paymentReference;

            // 2️⃣ Generate Jitsi link automatically
            if (string.IsNullOrEmpty(booking.VideoCallLink))
            {
                // You can use BookingID as a unique room name
                string meetingId = $"Booking-{booking.BookingID}-{Guid.NewGuid().ToString().Substring(0, 8)}";
                string jitsiDomain = "https://meet.jit.si";
                booking.VideoCallLink = $"{jitsiDomain}/{meetingId}";
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Payment updated successfully!",
                videoLink = booking.VideoCallLink // optional: return link to frontend
            });
        }

        // GET: /api/Bookings/videoLink/{requestID}
        [HttpGet("videoLink/{requestID}")]
        public IActionResult GetVideoLinkByRequest(int requestID)
        {
            // Find the booking safely
            var booking = _context.Bookings
                .Where(b => b.RequestID == requestID)
                .Select(b => new
                {
                    videoCallLink = b.VideoCallLink // can be null
                })
                .FirstOrDefault();

            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            return Ok(booking);
        }



        // PUT: Reschedule Booking
        [HttpPut("{id}/reschedule")]
        public async Task<IActionResult> RescheduleBooking(int id, [FromBody] System.DateTime newDateTime)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            booking.ScheduledDateTime = newDateTime;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking rescheduled successfully!" });
        }

        // PUT: Cancel Booking
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            booking.Status = "Cancelled";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking cancelled successfully!" });
        }
    }
}
