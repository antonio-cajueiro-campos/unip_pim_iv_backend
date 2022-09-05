
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos.Interfaces;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;
public class GetUserInfosUseCase : IGetUserInfosUseCase
{
	public DefaultResponse<string> Handle(GetUserInfosInput userInfosInput) {
		return new();
	}
}
