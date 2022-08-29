using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate;

public class AuthenticateInput
{
	[Required(ErrorMessage = "Preencha o campo Username.")]
	public string Username { get; set; }
	
	[Required(ErrorMessage = "Preencha o campo Senha.")]
	public string Password { get; set; }
}
