using Microsoft.AspNetCore.Mvc;
using MentalPeaceGuider.Data;
using MentalPeaceGuider.Models;
using MentalPeaceGuider.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MentalPeaceGuider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CancelledBookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CancelledBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/CancelledBookings
        [HttpPost]
        public async Task<IActionResult> CreateCancelledBooking([FromBody] CreateCancelledBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookingExists = await _context.Bookings.AnyAsync(b => b.BookingID == dto.BookingID);
            if (!bookingExists)
                return BadRequest(new { message = "Invalid BookingID" });

            var cancelledBooking = new CancelledBooking
            {
                BookingID = dto.BookingID,
                CancelledBy = dto.CancelledBy,
                Reason = dto.Reason,
                CancelledAt = dto.CancelledAt
            };

            _context.CancelledBookings.Add(cancelledBooking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cancelled booking created successfully!", cancelID = cancelledBooking.CancelID });
        }

        // GET: api/CancelledBookings
        [HttpGet]
        public async Task<IActionResult> GetAllCancelledBookings()
        {
            var cancelledBookings = await _context.CancelledBookings
                .Include(cb => cb.Booking)
                .ToListAsync();

            return Ok(cancelledBookings);
        }

        // GET: api/CancelledBookings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCancelledBookingById(int id)
        {
            var cancelledBooking = await _context.CancelledBookings
                .Include(cb => cb.Booking)
                .FirstOrDefaultAsync(cb => cb.CancelID == id);

            if (cancelledBooking == null)
                return NotFound(new { message = "Cancelled booking not found" });

            return Ok(cancelledBooking);
        }

        // PUT: api/CancelledBookings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCancelledBooking(int id, [FromBody] UpdateCancelledBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cancelledBooking = await _context.CancelledBookings.FindAsync(id);
            if (cancelledBooking == null)
                return NotFound(new { message = "Cancelled booking not found" });

            cancelledBooking.CancelledBy = dto.CancelledBy;
            cancelledBooking.Reason = dto.Reason;
            cancelledBooking.CancelledAt = dto.CancelledAt;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Cancelled booking updated successfully!" });
        }

        // DELETE: api/CancelledBookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCancelledBooking(int id)
        {
            var cancelledBooking = await _context.CancelledBookings.FindAsync(id);
            if (cancelledBooking == null)
                return NotFound(new { message = "Cancelled booking not found" });

            _context.CancelledBookings.Remove(cancelledBooking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cancelled booking deleted successfully!" });
        }
    }
}
