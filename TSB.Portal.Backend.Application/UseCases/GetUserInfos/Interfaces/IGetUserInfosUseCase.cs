using TSB.Portal.Backend.Application.Transport;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos.Interfaces;
public interface IGetUserInfosUseCase
{
	public DefaultResponse<string> Handle(GetUserInfosInput userInfosInput);
}