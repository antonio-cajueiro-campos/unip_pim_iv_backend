using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TSB.Portal.Backend.CrossCutting.Constants;

namespace TSB.Portal.Backend.Application.UseCases.CompleteRegistration;

public class CompleteRegistrationInput
{
	// Cliente
	
	[Required(ErrorMessage = Messages.RequiredTelefone)]
	public string Telefone { get; set; }

	[Required(ErrorMessage = Messages.RequiredCEP)]
    public string CEP { get; set; }

	[Required(ErrorMessage = Messages.RequiredCidade)]
	public string Cidade { get; set; }

	[Required(ErrorMessage = Messages.RequiredEstado)]
	public string Estado { get; set; }

	[Required(ErrorMessage = Messages.RequiredBairro)]
	public string Bairro { get; set; }

	[Required(ErrorMessage = Messages.RequiredRua)]
	public string Rua { get; set; }

	[Required(ErrorMessage = Messages.RequiredNumero)]
    public string Numero { get; set; }
    public string Complemento { get; set; }

	[Required(ErrorMessage = Messages.RequiredChavePIX)]
    public string ChavePIX { get; set; }

	// Funcionario
	public string Cargo { get; set; }

	// identificação
	public ClaimsPrincipal ClaimsPrincipal { get; set; }
}