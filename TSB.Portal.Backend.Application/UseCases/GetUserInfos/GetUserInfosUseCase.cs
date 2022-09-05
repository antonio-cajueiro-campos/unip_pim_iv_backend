
using TSB.Portal.Backend.Application.Transport;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;
public class GetUserInfosUseCase : IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput>
{
	public DefaultResponse<GetUserInfosOutput> Handle(GetUserInfosInput userInfosInput) {
		return new();
	}
}
