
using Microsoft.EntityFrameworkCore;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;
public class GetUserInfosUseCase : IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput>
{
	private DataContext database { get; set; }

	public GetUserInfosUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<GetUserInfosOutput> Handle(GetUserInfosInput userInfosInput)
	{
		return this.GetUserInfos(userInfosInput);
	}

	private DefaultResponse<GetUserInfosOutput> GetUserInfos(GetUserInfosInput userInfosInput)
	{
		if (userInfosInput.Id == null && userInfosInput.ClaimsPrincipal == null)
		{
			return new()
			{
				StatusCode = 400,
				Error = true,
				Data = null,
				Message = Messages.BadRequest
			};
		}

		try
		{
			var userId = userInfosInput.Id ?? userInfosInput.ClaimsPrincipal.GetUserId();
			
			var user = this.database.Users.Include(c => c.Credential).First(c => c.Id == userId);

			if (user == null)
			{
				return new()
				{
					StatusCode = 404,
					Error = true,
					Data = null,
					Message = Messages.UserNotFound
				};
			}

			var userData = user.MapObjectTo(new GetUserInfosOutput());

			userData.Credential = user.Credential.MapObjectToDynamic();		

			((IDictionary<String, Object>)userData.Credential).Remove("Password");
			((IDictionary<String, Object>)userData.Credential).Remove("Id");

			return new()
			{
				StatusCode = 200,
				Error = false,
				Data = userData,
				Message = Messages.Success
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				StatusCode = 500,
				Error = true,
				Data = null,
				Message = Messages.Error + ex
			};
		}
	}
}
