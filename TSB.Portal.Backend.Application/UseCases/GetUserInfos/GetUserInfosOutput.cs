using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;

public class GetUserInfosOutput
{
	public UserDTO User { get; set; }
    public EnderecoDTO Endereco { get; set; }
    public string Telefone { get; set; }
    public string ChavePIX { get; set; }
	public string Cargo { get; set; }
}
