using TSB.Portal.Backend.Application.EntitiesUseCase;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate;

public class AuthenticateOutput
{
	public JwtEntity Jwt { get; set; }
}
