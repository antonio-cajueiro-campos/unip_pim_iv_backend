
using Microsoft.AspNetCore.SignalR;

namespace TSB.Portal.Backend.Application.UseCases.WebSocketChat;
public class WebSocketChat : Hub
{
	private static List<Message> Messages;
	public WebSocketChat()
	{
		if (Messages == null)
		Messages = new List<Message>();
	}
	public void NewMessage(string text, string username, long userId)
	{
		Clients.All.SendAsync("newMessage", text, username, userId);
		Messages.Add(new () {
			UserId = userId,
			Text = text,
			UserName = username
		});
	}

	public void NewUser(string username, string connectionId)
	{
		Clients.Client(connectionId).SendAsync("previousMessages", Messages);
		Clients.All.SendAsync("newUser", username);
	}
}

public class Message
{
	public long UserId { get; set; }
	public string UserName { get; set; }
	public string Text { get; set; }
}