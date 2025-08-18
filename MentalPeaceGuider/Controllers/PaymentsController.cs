using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentalPeaceGuider.Data;
using MentalPeaceGuider.Models;
using MentalPeaceGuider.Dtos;

namespace MentalPeaceGuider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            var payments = await _context.Payments
                .Select(p => new PaymentDto
                {
                    PaymentID = p.PaymentID,
                    BookingID = p.BookingID,
                    UserID = p.UserID,
                    Amount = p.Amount,
                    PaymentStatus = p.PaymentStatus,
                    PaymentDate = p.PaymentDate
                })
                .ToListAsync();

            return Ok(payments);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return new PaymentDto
            {
                PaymentID = payment.PaymentID,
                BookingID = payment.BookingID,
                UserID = payment.UserID,
                Amount = payment.Amount,
                PaymentStatus = payment.PaymentStatus,
                PaymentDate = payment.PaymentDate
            };
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment(CreatePaymentDto dto)
        {
            var payment = new Payment
            {
                BookingID = dto.BookingID,
                UserID = dto.UserID,
                Amount = dto.Amount,
                PaymentStatus = dto.PaymentStatus,
                PaymentDate = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            var result = new PaymentDto
            {
                PaymentID = payment.PaymentID,
                BookingID = payment.BookingID,
                UserID = payment.UserID,
                Amount = payment.Amount,
                PaymentStatus = payment.PaymentStatus,
                PaymentDate = payment.PaymentDate
            };

            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentID }, result);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentDto dto)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            payment.Amount = dto.Amount;
            payment.PaymentStatus = dto.PaymentStatus;
            payment.PaymentDate = dto.PaymentDate;

            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
