using System.ComponentModel.DataAnnotations;
using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.CrossCutting.Enums;
using TSB.Portal.Backend.CrossCutting.Validation;

namespace TSB.Portal.Backend.Application.UseCases.UserRegister;
public class UserRegisterInput {
	
	[Required(ErrorMessage = Messages.RequiredUsername)]
	public string Username { get; set; }
	
	[Required(ErrorMessage = Messages.RequiredPassword)]
	public string Password { get; set; }

	[Required(ErrorMessage = Messages.RequiredRepassword)]
	public string Repassword { get; set; }

	[Required(ErrorMessage = Messages.RequiredName)]
	public string Name { get; set; }
	
	[Required(ErrorMessage = Messages.RequiredDocument)]
	[Document]
	public string Document { get; set; }

	public Roles? Role { get; set; }

	// Cliente
	public string Telefone { get; set; }
    public EnderecoDTO Endereco { get; set; }
    public string ChavePIX { get; set; }

	// Funcionario
	public string Cargo { get; set; }
}