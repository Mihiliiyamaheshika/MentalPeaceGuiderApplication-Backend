using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentalPeaceGuider.Data;
using MentalPeaceGuider.Models;
using MentalPeaceGuider.Dtos;

namespace MentalPeaceGuider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotInteractionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatbotInteractionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChatbotInteractions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatbotInteractionDto>>> GetAll()
        {
            var interactions = await _context.ChatbotInteractions
                .Select(i => new ChatbotInteractionDto
                {
                    InteractionID = i.InteractionID,
                    UserID = i.UserID,
                    UserMessage = i.UserMessage,
                    BotResponse = i.BotResponse,
                    CreatedAt = i.CreatedAt
                })
                .ToListAsync();

            return Ok(interactions);
        }

        // GET: api/ChatbotInteractions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatbotInteractionDto>> GetById(int id)
        {
            var interaction = await _context.ChatbotInteractions.FindAsync(id);

            if (interaction == null)
                return NotFound();

            return new ChatbotInteractionDto
            {
                InteractionID = interaction.InteractionID,
                UserID = interaction.UserID,
                UserMessage = interaction.UserMessage,
                BotResponse = interaction.BotResponse,
                CreatedAt = interaction.CreatedAt
            };
        }

        // POST: api/ChatbotInteractions
        [HttpPost]
        public async Task<ActionResult<ChatbotInteractionDto>> Create(CreateChatbotInteractionDto dto)
        {
            var interaction = new ChatbotInteraction
            {
                UserID = dto.UserID,
                UserMessage = dto.UserMessage,
                BotResponse = dto.BotResponse,
                CreatedAt = DateTime.Now
            };

            _context.ChatbotInteractions.Add(interaction);
            await _context.SaveChangesAsync();

            var result = new ChatbotInteractionDto
            {
                InteractionID = interaction.InteractionID,
                UserID = interaction.UserID,
                UserMessage = interaction.UserMessage,
                BotResponse = interaction.BotResponse,
                CreatedAt = interaction.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = interaction.InteractionID }, result);
        }

        // PUT: api/ChatbotInteractions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateChatbotInteractionDto dto)
        {
            var interaction = await _context.ChatbotInteractions.FindAsync(id);
            if (interaction == null)
                return NotFound();

            interaction.UserMessage = dto.UserMessage;
            interaction.BotResponse = dto.BotResponse;

            _context.Entry(interaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ChatbotInteractions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var interaction = await _context.ChatbotInteractions.FindAsync(id);
            if (interaction == null)
                return NotFound();

            _context.ChatbotInteractions.Remove(interaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
