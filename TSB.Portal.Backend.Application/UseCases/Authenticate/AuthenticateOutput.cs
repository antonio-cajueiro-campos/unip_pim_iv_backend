using TSB.Portal.Backend.Application.Transport;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate;

public class AuthenticateOutput
{
	public string Token { get; set; }

	public AuthenticateOutput(string token)
	{
		Token = token;
	}

	public AuthenticateOutput()	{}
}
