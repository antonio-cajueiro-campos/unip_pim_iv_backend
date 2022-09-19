using System.Security.Claims;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;
public class GetUserInfosInput
{
	public long? Id { get; set; }
	public ClaimsPrincipal ClaimsPrincipal { get; set; }

	public GetUserInfosInput(ClaimsPrincipal claimsPrincipal) {
		this.ClaimsPrincipal = claimsPrincipal;
	}

	public GetUserInfosInput(long? id) {
		this.Id = id;
	}
}
