public class ChatbotInteraction
{
    public int InteractionID { get; set; }
    public int UserID { get; set; }
    public string UserMessage { get; set; }
    public string BotResponse { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User User { get; set; }
}
 