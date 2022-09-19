using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;

namespace TSB.Portal.Backend.Application.UseCases.ChangeUserData;
public class ChangeUserDataOutput
{
	public string Name { get; set; }
	public string Document { get; set; }
	public string Telefone { get; set; }
	public string Cargo { get; set; }
	public string ChavePIX { get; set; }
	public CredentialDTO Credential { get; set; }
	public EnderecoDTO Endereco { get; set; }
}