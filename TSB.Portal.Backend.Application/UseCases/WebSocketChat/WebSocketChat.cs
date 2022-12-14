using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.CrossCutting.Enums;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Application.UseCases.WebSocketChat.Models;

namespace TSB.Portal.Backend.Application.UseCases.WebSocketChat;

//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WebSocketChat : Hub
{
	private readonly DataContext _database;
	private static List<UserHub> UserHubList;
	private static List<UserChat> UserChatList;
	private static List<Chat> ChatList;
	public WebSocketChat(DataContext database)
	{
		if (UserHubList == null || UserHubList.Count == 0)
			UserHubList = new List<UserHub>();

		if (UserChatList == null || UserChatList.Count == 0)
			UserChatList = new List<UserChat>();

		if (ChatList == null || ChatList.Count == 0)
			ChatList = new List<Chat>();

		_database = database;
	}

	public override Task OnConnectedAsync()
	{
		var httpContext = Context.GetHttpContext();
		IQueryCollection query = httpContext.Request.Query;
		var connectionId = Context.ConnectionId;

		string tokenJwt = query["access_token"];
		long userId = long.Parse(query["userId"]);

		var userDb = _database.Users.Include(u => u.Credential).FirstOrDefault(u => u.Id == userId);
		if (userDb != null) {
			var newUser = new UserHub()
			{
				UserId = userId,
				Username = userDb.Name,
				UserConnectionId = connectionId,
				Role = userDb.Credential.Role
			};

			if (UserHubList.Any(uh => uh.UserId == userId))
			{
				var oldUserHub = UserHubList.FirstOrDefault(uh => uh.UserId == userId);
				
				if (oldUserHub != null)
					UserHubList.Remove(oldUserHub);
			}

			UserHubList.Add(newUser);
		}

		return base.OnConnectedAsync();
	}

	public override Task OnDisconnectedAsync(Exception exception)
	{
		return base.OnDisconnectedAsync(exception);
	}

	public void InitCliente(long userId, string type, string sinistro = "")
	{
		var userHub = UserHubList.FirstOrDefault(u => u.UserId == userId);
		if (userHub != null)
		{
			var role = userHub.Role;
			var connectionId = userHub.UserConnectionId;

			var chatsOfUser = GetChatListOfUser(userId);

			if (chatsOfUser != null)
			{
				if (!chatsOfUser.Any(c => c.Type == type))
				{
					var firstMessage = GetFirstMessage(type, sinistro, userHub.Username);
					Chat chat = CreateChat(userId, type, firstMessage);
					Clients.Client(connectionId).SendAsync("initCliente", chat.ChatId);
					StartedChats(userId);
					Clients.Client(connectionId).SendAsync("previousMessages", chat.MessagesList);
					UpdateChatList();
				}
				else
				{
					Chat chat = chatsOfUser.FirstOrDefault(c => c.Type == type);

					if (chat != null)
					{
						Clients.Client(connectionId).SendAsync("initCliente", chat.ChatId);
						StartedChats(userId);
						Clients.Client(connectionId).SendAsync("previousMessages", chat.MessagesList);
					}
				}
			}
		}
	}

	public void InitFuncionario(long userId)
	{
		var userHub = UserHubList.FirstOrDefault(u => u.UserId == userId);

		if (userHub != null)
		{
			var role = userHub.Role;
			var connectionId = userHub.UserConnectionId;

			var chatListForFrontEnd = ConvertChatListForFrontEnd(ChatList);
			var chatsOfUser = ConvertChatListForFrontEnd(GetChatListOfUser(userId));

			Clients.Client(connectionId).SendAsync("initFuncionario", chatListForFrontEnd);
			StartedChats(userId);
		}
	}

	public void ConnectToChat(long userId, long chatId)
	{
		var userHub = UserHubList.FirstOrDefault(u => u.UserId == userId);
		var chat = ChatList.FirstOrDefault(c => c.ChatId == chatId);

		if (userHub != null)
		{
			if (chat != null)
			{
				if (!UserAlreadyInTheChat(userId, chatId)) {
					var newUserMessage = new Message()
					{
						OwnerId = userId,
						Timestamp = DateTime.Now,
						Username = userHub.Username,
						Text = userHub.Username + " entrou no bate-papo.",
						Type = ChatMessageTypes.Announcement.ToString()
					};

					UserChatList.Add(new()
					{
						UserId = userId,
						ChatId = chatId
					});

					NewMessage(newUserMessage, chat.ChatId);
				}

				Clients.Client(userHub.UserConnectionId).SendAsync("connectToChat", chatId);
				Clients.Client(userHub.UserConnectionId).SendAsync("previousMessages", chat.MessagesList);
			}
			else
			{
				Clients.Client(userHub.UserConnectionId).SendAsync("chatNotFound");
			}
		}
	}

	public void LeaveSession(long userId, long chatId)
	{
		var userChat = UserChatList.FirstOrDefault(uc => uc.UserId == userId && uc.ChatId == chatId);
		if (userChat != null)
		{
			var chat = ChatList.FirstOrDefault(c => c.ChatId == chatId);
			var user = UserHubList.FirstOrDefault(u => u.UserId == userId);

			if (chat != null && user != null)
			{
				var newUserMessage = new Message()
				{
					OwnerId = userId,
					Timestamp = DateTime.Now,
					Username = user.Username,
					Text = user.Username + " deixou o bate-papo.",
					Type = ChatMessageTypes.Announcement.ToString()
				};
				UserChatList.Remove(userChat);
				StartedChats(userId);
				NewMessage(newUserMessage, chat.ChatId);
			}
		}
	}

	public void CloseSession(long chatId)
	{
		var chat = ChatList.FirstOrDefault(c => c.ChatId == chatId);
		if (chat != null)
		{
			var usersInChat = GetUsersInChat(chatId);
			usersInChat.ForEach(u =>
			{
				Clients.Client(u.UserConnectionId).SendAsync("closeSession");
			});

			RemoveAllUsersFromChat(chatId);
			ChatList.Remove(chat);
		}
	}

	public void NewMessage(Message newMessage, long chatId)
	{
		var timestamp = DateTime.Now;
		newMessage.Timestamp = timestamp;

		var chat = ChatList.FirstOrDefault(c => c.ChatId == chatId);

		if (chat != null)
		{
			chat.MessagesList.Add(newMessage);

			var usersNoChat = GetUsersInChat(chatId);

			usersNoChat.ForEach(user =>
			{
				Clients.Client(user.UserConnectionId).SendAsync("newMessage", newMessage, chatId);
			});
		}
	}

	public void IsWriting(string username, long userId, long chatId)
	{
		Chat chat = ChatList.FirstOrDefault(c => c.ChatId == chatId);

		var usersNoChat = GetUsersInChat(chatId);

		if (chat != null && usersNoChat.Count > 0)
		{
			usersNoChat.ForEach(u =>
			{
				Clients.Client(u.UserConnectionId).SendAsync("isWriting", username, userId, chatId);
			});
		}
	}

	public void UpdateChatList()
	{
		var FuncionarioList = UserHubList.Where(u => u.Role == "Funcionario");
		foreach (UserHub func in FuncionarioList)
		{
			var chatListForFrontEnd = ConvertChatListForFrontEnd(ChatList);

			Clients.Client(func.UserConnectionId).SendAsync("updateChatList", chatListForFrontEnd);
		}
	}

	public void StartedChats(long userId)
	{
		var user = UserHubList.FirstOrDefault(u => u.UserId == userId);
		if (user != null) {
			var chatsOfUserListForFrontEnd = ConvertChatListForFrontEnd(GetChatListOfUser(user.UserId));
			Clients.Client(user.UserConnectionId).SendAsync("startedChats", chatsOfUserListForFrontEnd);
		}
	}

	private Chat CreateChat(long userId, string type, Message firstMessage)
	{
		var chatId = ChatList.Count;
		chatId = chatId + 1;
		var chat = new Chat()
		{
			ChatId = chatId,
			Type = type,
			MessagesList = new List<Message>(),
		};

		chat.MessagesList.Add(
			new()
			{
				OwnerId = 0,
				Timestamp = DateTime.Now,
				Username = "System",
				Text = "Novo canal de comunica????o iniciado",
				Type = ChatMessageTypes.Announcement.ToString()
			}
		);

		if (firstMessage != null)
			chat.MessagesList.Add(firstMessage);

		UserChatList.Add(new UserChat
		{
			UserId = userId,
			ChatId = chatId
		});

		ChatList.Add(chat);

		return chat;
	}

	private List<UserHub> GetUsersInChat(long chatId)
	{
		var userChatList = UserChatList.FindAll(c => c.ChatId == chatId).ToList();
		var usersNoChat = new List<UserHub>();
		foreach (var userChat in userChatList)
		{
			UserHub userNoChat = UserHubList.FirstOrDefault(u => u.UserId == userChat.UserId);
			if (userNoChat != null)
			{
				usersNoChat.Add(userNoChat);
			}
		}
		return usersNoChat;
	}

	private void RemoveAllUsersFromChat(long chatId)
	{
		var userChatList = UserChatList.FindAll(c => c.ChatId == chatId).ToList();
		foreach (var chatFound in userChatList)
		{
			UserChatList.Remove(chatFound);
		}
	}

	private Message GetFirstMessage(string type, string sinitro = "", string cliente = "")
	{
		switch (type)
		{
			case "service": return null;
			case "sinistro":
				return new Message()
				{
					OwnerId = 0,
					Timestamp = DateTime.Now,
					Username = "System",
					Text = $"Solicita????o de ativamento de seguro, sinitro {sinitro} para o cliente {cliente}.",
					Type = ChatMessageTypes.Message.ToString()
				};
			default: return null;
		}
	}

	private bool UserAlreadyInTheChat(long userId, long chatId) {
		return UserChatList.Any(uc => uc.UserId == userId && uc.ChatId == chatId);
	}

	private List<Chat> GetChatListOfUser(long userId)
	{
		var userChatList = UserChatList.FindAll(u => u.UserId == userId).ToList();
		var chatsOfUser = new List<Chat>();
		foreach (var userChat in userChatList)
		{
			Chat chat = ChatList.FirstOrDefault(c => c.ChatId == userChat.ChatId);
			if (chat != null)
			{
				chatsOfUser.Add(chat);
			}
		}

		return chatsOfUser;
	}

	private List<ChatForFrontEnd> ConvertChatListForFrontEnd(List<Chat> chatList)
	{
		var chatListForFrontEnd = new List<ChatForFrontEnd>();

		foreach (Chat chat in chatList)
		{
			var chatForFrontEnd = chat.MapObjectTo(new ChatForFrontEnd());
			chatForFrontEnd.UserList = GetUsersInChat(chat.ChatId);

			chatListForFrontEnd.Add(chatForFrontEnd);
		}
		return chatListForFrontEnd;
	}	
}
