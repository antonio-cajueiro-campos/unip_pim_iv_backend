using Microsoft.AspNetCore.SignalR;
using TSB.Portal.Backend.CrossCutting.Enums;
using TSB.Portal.Backend.Infra.Repository;
using System.Security.Claims;
using TSB.Portal.Backend.CrossCutting.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace TSB.Portal.Backend.Application.UseCases.WebSocketChat;
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class WebSocketChat : Hub
{
	private DataContext database { get; set; }
	private static List<UserHub> UserHubList;
	private static List<Chat> ChatList;
	public WebSocketChat(DataContext database)
	{
		if (UserHubList == null || UserHubList.Count == 0)
			UserHubList = new List<UserHub>();

		if (ChatList == null || ChatList.Count == 0)
			ChatList = new List<Chat>();

		this.database = database;
	}

	// Fluxo inicial, todos os usuarios passam por ele
	public override Task OnConnectedAsync()
	{
		var httpContext = Context.GetHttpContext();
		IQueryCollection query = httpContext.Request.Query;
		var connectionId = Context.ConnectionId;

		string tokenJwt = query["access_token"];
		long userId = long.Parse(query["userId"]);
		string userRole = query["role"];

		var userDb = this.database.Users.Find(userId);

		var newUser = new UserHub()
		{
			UserId = userId,
			UserName = userDb.Name,
			UserConnectionId = connectionId,
			Role = userRole
		};

		// adiciona usuario a lista de usuarios no HUB
		if (!UserHubList.Any(uh => uh.UserId == userId))
		{
			UserHubList.Add(newUser);
		}
		else
		{
			var oldUserHub = UserHubList.FirstOrDefault(uh => uh.UserId == userId);
			UserHubList.Remove(oldUserHub);
			UserHubList.Add(newUser);

			if (ChatList.Any(c => c.UserList.Any(u => u.UserId == userId)))
			{
				var chat = ChatList.FirstOrDefault(c => c.UserList.Any(u => u.UserId == userId));
				var oldUserChat = chat.UserList.First(u => u.UserId == userId);

				chat.UserList.Remove(oldUserChat);

				chat.UserList.Add(newUser);
			}
		}

		return base.OnConnectedAsync();
	}

	// Init Fluxo do Cliente Chat Service
	public void CreateChat(long userId)
	{
		var userHub = UserHubList.FirstOrDefault(u => u.UserId == userId);
		if (userHub != null)
		{
			var role = userHub.Role;
			var connectionId = userHub.UserConnectionId;

			// se for um cliente e nao tiver um chat com ele dentro
			if (!ChatList.Any(c => c.UserList.Any(u => u.UserId == userId)) && role == "Cliente")
			{
				// cria um chat
				var chat = new Chat()
				{
					ChatId = userId,
					MessagesList = new List<Message>(),
					UserList = new List<UserHub>()
				};

				// coloca uma msg inicial de chat
				chat.MessagesList.Add(
					new()
					{
						UserId = 0,
						Timestamp = DateTime.Now,
						UserName = "System",
						Text = "Novo canal de comunicação iniciado",
						Type = ChatMessageTypes.Announcement.ToString()
					}
				);

				chat.UserList.Add(userHub);

				ChatList.Add(chat);

				Clients.Client(connectionId).SendAsync("previousMessages", chat.MessagesList);

				var FuncionarioList = UserHubList.Where(u => u.Role == "Funcionario");
				foreach (UserHub Func in FuncionarioList)
				{
					Clients.Client(Func.UserConnectionId).SendAsync("updateChatList", ChatList);
				}
			}
			// Se for cliente e já existir chat com o ID dele
			else if (role == "Cliente")
			{
				Chat chat = ChatList.FirstOrDefault(c => c.UserList.Any(u => u.UserId == userId));

				if (chat != null)
				{
					Clients.Client(connectionId).SendAsync("previousMessages", chat.MessagesList);
				}
			}
		}
	}

	// Init Fluxo do Cliente Chat Sinistro
	public void CreateChatSinistro(long userId)
	{
		var userHub = UserHubList.FirstOrDefault(u => u.UserId == userId);
		if (userHub != null)
		{
			var role = userHub.Role;
			var connectionId = userHub.UserConnectionId;

			// se for um cliente e nao tiver um chat com ele dentro
			if (!ChatList.Any(c => c.UserList.Any(u => u.UserId == userId)) && role == "Cliente")
			{
				// cria um chat
				var chat = new Chat()
				{
					ChatId = userId,
					MessagesList = new List<Message>(),
					UserList = new List<UserHub>()
				};

				// coloca uma msg inicial de chat
				chat.MessagesList.Add(
					new()
					{
						UserId = 0,
						Timestamp = DateTime.Now,
						UserName = "System",
						Text = "Novo canal de comunicação iniciado",
						Type = ChatMessageTypes.Announcement.ToString()
					}
				);

				chat.MessagesList.Add(
					new()
					{
						UserId = 0,
						Timestamp = DateTime.Now,
						UserName = "System",
						Text = "O seguinte sinistro ocorreu em {endereço}, solicita a ativação do meu seguro.",
						Type = ChatMessageTypes.Message.ToString()
					}
				);

				chat.UserList.Add(userHub);

				ChatList.Add(chat);

				Clients.Client(connectionId).SendAsync("previousMessages", chat.MessagesList);

				var FuncionarioList = UserHubList.Where(u => u.Role == "Funcionario");
				foreach (UserHub Func in FuncionarioList)
				{
					Clients.Client(Func.UserConnectionId).SendAsync("updateChatList", ChatList);
				}
			}
			// Se for cliente e já existir chat com o ID dele
			else if (role == "Cliente")
			{
				Chat chat = ChatList.FirstOrDefault(c => c.UserList.Any(u => u.UserId == userId));

				if (chat != null)
				{
					Clients.Client(connectionId).SendAsync("previousMessages", chat.MessagesList);
				}
			}
		}
	}


	// Init Fluxo do funcionario
	public void InitFuncionario(long userId)
	{
		var userHub = UserHubList.FirstOrDefault(u => u.UserId == userId);

		if (userHub != null)
		{
			var role = userHub.Role;
			var connectionId = userHub.UserConnectionId;

			// se for um funcionario e nao tiver um chat com ele dentro, retorna a lista de chats
			if (!ChatList.Any(c => c.UserList.Any(u => u.UserId == userId)) && role == "Funcionario")
			{
				Clients.Client(connectionId).SendAsync("initFuncionario", ChatList, 0);
			}
			// se for um funcionario e tiver um chat com ele dentro, manda as msgs
			else if (role == "Funcionario")
			{
				Chat chat = ChatList.FirstOrDefault(c => c.UserList.Any(u => u.UserId == userId));

				if (chat != null)
				{
					Clients.Client(connectionId).SendAsync("initFuncionario", ChatList, chat.ChatId);
					Clients.Client(connectionId).SendAsync("previousMessages", chat.MessagesList);
				}
			}
		}
	}

	public void UpdateChatList()
	{
		var FuncionarioList = UserHubList.Where(u => u.Role == "Funcionario");
		foreach (UserHub Func in FuncionarioList)
		{
			Clients.Client(Func.UserConnectionId).SendAsync("updateChatList", ChatList);
		}
	}

	public void ConnectToChat(long userId, long chatId)
	{
		var userHub = UserHubList.First(u => u.UserId == userId);

		var userRole = userHub.Role;

		if (userRole == "Funcionario")
		{
			var chat = ChatList.FirstOrDefault(c => c.ChatId == chatId);
			if (chat != null)
			{
				var newUserMessage = new Message()
				{
					UserId = userId,
					Timestamp = DateTime.Now,
					UserName = userHub.UserName,
					Text = userHub.UserName + " entrou no bate-papo.",
					Type = ChatMessageTypes.Announcement.ToString()
				};

				chat.UserList.Add(userHub);

				NewMessage(newUserMessage, chat.ChatId);
				
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
		var chat = ChatList.FirstOrDefault(c => c.ChatId == chatId);
		if (chat != null)
		{
			var user = chat.UserList.FirstOrDefault(u => u.UserId == userId);
			if (user != null)
			{
				var newUserMessage = new Message()
				{
					UserId = userId,
					Timestamp = DateTime.Now,
					UserName = user.UserName,
					Text = user.UserName + " deixou o bate-papo.",
					Type = ChatMessageTypes.Announcement.ToString()
				};

				chat.UserList.Remove(user);
				NewMessage(newUserMessage, chat.ChatId);
			}
		}
	}

	public void CloseSession(long chatId)
	{
		var chat = ChatList.FirstOrDefault(c => c.ChatId == chatId);
		if (chat != null)
		{
			chat.UserList.ForEach(u =>
			{
				Clients.Client(u.UserConnectionId).SendAsync("closeSession");
			});

			ChatList.Remove(chat);
		}
	}


	// Fluxo de ambos	
	public override Task OnDisconnectedAsync(Exception exception)
	{
		//UserHubList.Remove(UserHubList.FirstOrDefault(uh => uh.UserConnectionId == Context.ConnectionId));

		return base.OnDisconnectedAsync(exception);
	}

	public void NewMessage(Message newMessage, long chatId)
	{
		var timestamp = DateTime.Now;
		newMessage.Timestamp = timestamp;

		var chat = ChatList.FirstOrDefault(c => c.ChatId == chatId);
		if (chat != null)
		{
			chat.MessagesList.Add(newMessage);

			chat.UserList.ForEach(user =>
			{
				Clients.Client(user.UserConnectionId).SendAsync("newMessage", newMessage);
			});
		}
	}

	public void IsWriting(string userName, long userId)
	{
		var chat = ChatList.FirstOrDefault(c => c.UserList.Any(u => u.UserId == userId));
		if (chat != null)
		{
			chat.UserList.ForEach(u =>
			{
				Clients.Client(u.UserConnectionId).SendAsync("isWriting", userName, userId);
			});
		}
	}

	public class UserHub
	{
		public long UserId { get; set; }
		public string UserConnectionId { get; set; }
		public string UserName { get; set; }
		public string Role { get; set; }
	}

	public class Chat
	{
		public long ChatId { get; set; }
		public List<UserHub> UserList { get; set; }
		public List<Message> MessagesList { get; set; }
	}

	public class Message
	{
		public long UserId { get; set; }
		public DateTime Timestamp { get; set; }
		public string UserName { get; set; }
		public string Text { get; set; }
		public string Type { get; set; }
	}
}