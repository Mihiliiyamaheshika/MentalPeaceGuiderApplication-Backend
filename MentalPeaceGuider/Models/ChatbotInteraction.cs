using MentalPeaceGuider.Models; // ensures User class is recognized
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class ChatbotInteraction
    {
        [Key]
        [Column("InteractionId")]
        public int InteractionID { get; set; }
        public int UserID { get; set; }
        public string UserMessage { get; set; }
        public string BotResponse { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Users Users { get; set; }
    }
}
