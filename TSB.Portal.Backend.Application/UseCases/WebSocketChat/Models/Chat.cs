namespace TSB.Portal.Backend.Application.UseCases.WebSocketChat.Models;
public class Chat
{
	public long ChatId { get; set; }
	public string Type { get; set; }
	public List<Message> MessagesList { get; set; }
}
