using System;

namespace MentalPeaceGuider.Dtos
{
    public class ChatbotInteractionDto
    {
        public int InteractionID { get; set; }
        public int UserID { get; set; }
        public string UserMessage { get; set; }
        public string BotResponse { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateChatbotInteractionDto
    {
        public int UserID { get; set; }
        public string UserMessage { get; set; }
        public string BotResponse { get; set; }
    }

    public class UpdateChatbotInteractionDto
    {
        public string UserMessage { get; set; }
        public string BotResponse { get; set; }
    }
}
