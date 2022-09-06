using TSB.Portal.Backend.Application.EntitiesUseCase;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate;

public class AuthenticateOutput
{
	public JwtEntity Jwt { get; set; }

	public AuthenticateOutput(string token, string refreshToken, DateTime? expirationTime)
	{
		Jwt = new () {
			Token = token,
			RefreshToken = refreshToken,
			ExpirationTime = expirationTime
		};
	}

	public AuthenticateOutput()	{}
}
