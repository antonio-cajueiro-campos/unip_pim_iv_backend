using TSB.Portal.Backend.Application.Transport;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
public interface IAuthenticateUseCase
{
	public DefaultResponse<AuthenticateOutput> Handle(AuthenticateInput credentials);
}