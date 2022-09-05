
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos.Interfaces;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;
public class GetUserInfosUseCase : IGetUserInfosUseCase
{
	public DefaultResponse<string> Handle(GetUserInfosInput userInfosInput) {
		// TODO - verificar o tipó de usuario pelo header e seu jwt token, e entao consultar o banco de dados e retornar os dados
		return new();
	}
}
