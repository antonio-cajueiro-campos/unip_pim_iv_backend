using System.ComponentModel.DataAnnotations;
using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
using TSB.Portal.Backend.CrossCutting.Enums;
using TSB.Portal.Backend.CrossCutting.Validation;
using TSB.Portal.Backend.Infra.Repository.Entities;

namespace TSB.Portal.Backend.Application.UseCases.UserRegister;
public class UserRegisterInput {
	
	[Required(ErrorMessage = "Preencha o campo Username.")]
	public string Username { get; set; }
	
	[Required(ErrorMessage = "Preencha o campo Senha.")]
	public string Password { get; set; }

	[Required(ErrorMessage = "Preencha o campo Nome.")]
	public string Name { get; set; }
	
	[Required(ErrorMessage = "Preencha o campo Documento.")]
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