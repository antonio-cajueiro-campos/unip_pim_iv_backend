using Microsoft.AspNetCore.SignalR;
using TSB.Portal.Backend.CrossCutting.Enums;

namespace TSB.Portal.Backend.Application.UseCases.WebSocketChat;
public class WebSocketChat : Hub
{
	private static List<Message> MessagesList;
	private static List<User> UserList;
	private static List<Chat> ChatList;
	public WebSocketChat()
	{
		if (MessagesList == null || MessagesList.Count == 0)
			MessagesList = new List<Message>()
			{
				new ()
				{
					UserId = 0,
					Timestamp = DateTime.Now,
					UserName = "System",
					Text = "Novo canal de comunicação iniciado",
					Type = ChatMessageTypes.Announcement.ToString()
				}
			};

		if (UserList == null)
			UserList = new List<User>();
		
		if (ChatList == null)
			ChatList = new List<Chat>();
	}

	public void NewMessage(Message newMessage)
	{
		var timestamp = DateTime.Now;
		newMessage.Timestamp = timestamp;
		MessagesList.Add(newMessage);
		Clients.All.SendAsync("newMessage", newMessage);
	}

	public void CloseSession()
	{
		UserList.ForEach(u =>
		{
			Clients.Client(u.UserConnectionId).SendAsync("closeSession");
		});

		MessagesList = new List<Message>();
		UserList = new List<User>();
	}

	public void NewUser(string username, long userId, string connectionId, string type)
	{
		if (!UserList.Any(u => u.UserId == userId))
		{
			var newUserMessage = new Message()
			{
				UserId = userId,
				Timestamp = DateTime.Now,
				UserName = username,
				Text = username + " entrou no chat.",
				Type = ChatMessageTypes.Announcement.ToString()
			};

			var newUser = new User()
			{
				UserId = userId,
				UserName = username,
				UserConnectionId = connectionId,
				Type = type
			};

			UserList.Add(newUser);

			this.NewMessage(newUserMessage);
		}

		UserList.First(u => u.UserId == userId).UserConnectionId = connectionId;

		Clients.Client(connectionId).SendAsync("previousMessages", MessagesList);
	}

	public void IsWriting(string username, long userId)
	{
		Clients.All.SendAsync("isWriting", username, userId);
	}
}

public class User
{
	public long UserId { get; set; }
	public string UserConnectionId { get; set; }
	public string UserName { get; set; }
	public string Type { get; set; }
}

public class Chat
{
	public string ChatId { get; set; }
	//esse chat tem uma lista de pessoas que tem uma lista de msgs
}

public class Message
{
	public long UserId { get; set; }
	public DateTime Timestamp { get; set; }
	public string UserName { get; set; }
	public string Text { get; set; }
	public string Type { get; set; }
}
