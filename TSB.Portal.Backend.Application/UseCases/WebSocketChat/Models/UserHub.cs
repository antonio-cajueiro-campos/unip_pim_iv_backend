namespace TSB.Portal.Backend.Application.UseCases.WebSocketChat.Models;
public class UserHub
{
	public long UserId { get; set; }
	public string UserConnectionId { get; set; }
	public string Username { get; set; }
	public string Role { get; set; }
}
