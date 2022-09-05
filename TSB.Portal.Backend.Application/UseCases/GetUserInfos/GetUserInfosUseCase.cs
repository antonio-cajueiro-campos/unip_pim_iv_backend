
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos.Interfaces;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;
public class GetUserInfosUseCase : IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput>
{
	public DefaultResponse<GetUserInfosOutput> Handle(GetUserInfosInput userInfosInput) {
		return new();
	}
}
