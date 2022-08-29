namespace TSB.Portal.Backend.Application.UseCases.Authenticate;

public class AuthenticateOutput
{
	public string Token { get; set; }
	public AuthenticateOutput(string Token)
	{
		this.Token = Token;
	}

	public AuthenticateOutput()
	{
	}
}
