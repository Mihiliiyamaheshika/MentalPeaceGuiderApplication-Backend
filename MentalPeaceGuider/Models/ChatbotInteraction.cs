using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class ChatbotInteraction
    {
        [Key]
        public int InteractionID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [MaxLength(1000)]
        public string UserMessage { get; set; }

        [Required]
        [MaxLength(1000)]
        public string BotResponse { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("UserID")]
        public Users User { get; set; }
    }
}
