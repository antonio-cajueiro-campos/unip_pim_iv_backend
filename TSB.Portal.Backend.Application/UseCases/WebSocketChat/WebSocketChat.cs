
using Microsoft.AspNetCore.SignalR;

namespace TSB.Portal.Backend.Application.UseCases.WebSocketChat;
public class WebSocketChat : Hub
{
	private static List<Message> MessagesList;
	public WebSocketChat()
	{
		if (MessagesList == null)
		MessagesList = new List<Message>();
	}
	public void NewMessage(string text, string username, long userId)
	{
		var timestamp = DateTime.Now;

		Clients.All.SendAsync("newMessage", text, username, userId, timestamp);
		MessagesList.Add(new () {
			UserId = userId,
			Text = text,
			Timestamp = timestamp,
			UserName = username
		});
	}

	public void NewUser(string username, string connectionId)
	{
		Clients.Client(connectionId).SendAsync("previousMessages", MessagesList);
		Clients.All.SendAsync("newUser", username);
	}

	public void MeIsWriting(string username, long userId)
	{
		Clients.All.SendAsync("meIsWriting", username, userId);
	}

	public void OtherIsWriting(string username, long userId)
	{
		Clients.All.SendAsync("otherIsWriting", username, userId);
	}
}

public class Message
{
	public long UserId { get; set; }
	public DateTime Timestamp { get; set; }
	public string UserName { get; set; }
	public string Text { get; set; }
}