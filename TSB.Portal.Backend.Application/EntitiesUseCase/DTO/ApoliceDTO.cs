namespace TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
public class ApoliceDTO
{
    public CoberturaDTO Cobertura { get; set; }
	public ClienteDTO Cliente { get; set; }
    public DateTime Vigencia { get; set; }
    public DateTime Emissao { get; set; }
}