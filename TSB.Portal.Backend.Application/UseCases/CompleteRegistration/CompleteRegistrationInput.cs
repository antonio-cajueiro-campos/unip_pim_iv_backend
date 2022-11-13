using System.Security.Claims;

namespace TSB.Portal.Backend.Application.UseCases.CompleteRegistration;

public class CompleteRegistrationInput
{
	// Cliente
	public string Telefone { get; set; }
    public string CEP { get; set; }
	public string Cidade { get; set; }
	public string Estado { get; set; }
	public string Bairro { get; set; }
	public string Rua { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string ChavePIX { get; set; }

	// Funcionario
	public string Cargo { get; set; }

	// identificação
	public ClaimsPrincipal ClaimsPrincipal { get; set; }
}