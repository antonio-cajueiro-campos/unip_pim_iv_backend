
namespace TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
public class ClienteDTO
{
    public UserDTO User { get; set; }
    public EnderecoDTO Endereco { get; set; }
    public string Telefone { get; set; }
    public string ChavePIX { get; set; }
}