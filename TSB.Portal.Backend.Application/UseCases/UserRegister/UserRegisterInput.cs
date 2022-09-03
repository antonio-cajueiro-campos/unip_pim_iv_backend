using System.ComponentModel.DataAnnotations;
using TSB.Portal.Backend.CrossCutting.Enums;

namespace TSB.Portal.Backend.Application.UseCases.UserRegister;
public class UserRegisterInput {
	
	[Required(ErrorMessage = "Preencha o campo Username.")]
	public string Username { get; set; }
	
	[Required(ErrorMessage = "Preencha o campo Senha.")]
	public string Password { get; set; }

	[Required(ErrorMessage = "Preencha o campo Nome.")]
	public string Name { get; set; }
	
	[Required(ErrorMessage = "Preencha o campo Documento.")]
	public string Document { get; set; }

	public Roles? Role { get; set; }
}