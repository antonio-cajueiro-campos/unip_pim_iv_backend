using TSB.Portal.Backend.Application.Transport;

namespace TSB.Portal.Backend.Application.UseCases.UserRegister;
public class UserRegisterOutput
{
	public string Token { get; set; }

	public UserRegisterOutput(string token)
	{
		Token = token;
	}

	public UserRegisterOutput()	{}
}