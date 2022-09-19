namespace TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
public class HistoricoSinistroDTO
{
    public ClienteDTO Cliente { get; set; }
    public SinistroDTO Sinistro { get; set; }
    public DateTime Ocorrencia { get; set; }
    public Decimal Valor { get; set; }
}