using Microsoft.AspNetCore.Mvc;
using MentalPeaceGuider.Data;
using MentalPeaceGuider.Models;
using MentalPeaceGuider.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace MentalPeaceGuider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableSlotsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AvailableSlotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new slot
        [HttpPost("add")]
        public async Task<IActionResult> AddSlot([FromBody] AddSlotDto dto)
        {
            var slot = new AvailableSlot
            {
                CounselorID = dto.CounselorID,
                SlotDateTime = dto.SlotDateTime,
                IsBooked = false // default when adding new slot
            };

            _context.AvailableSlots.Add(slot);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Slot added successfully!" });
        }

        //Get slots by counselor
        [HttpGet("counselor/{counselorId}")]
        public IActionResult GetSlotsByCounselor(int counselorId)
        {
            var slots = _context.AvailableSlots
                .Where(s => s.CounselorID == counselorId)
                .ToList();

            return Ok(slots);
        }

        //Update a slot
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSlot([FromBody] UpdateSlotDto dto)
        {
            var slot = _context.AvailableSlots.FirstOrDefault(s => s.SlotID == dto.SlotId);
            if (slot == null) return NotFound(new { message = "Slot not found" });

            slot.SlotDateTime = dto.SlotDateTime;
            slot.IsBooked = dto.IsBooked;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Slot updated successfully!" });
        }

        // Delete a slot
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            var slot = _context.AvailableSlots.FirstOrDefault(s => s.SlotID == id);
            if (slot == null) return NotFound(new { message = "Slot not found" });

            _context.AvailableSlots.Remove(slot);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Slot deleted successfully!" });
        }
    }
}
