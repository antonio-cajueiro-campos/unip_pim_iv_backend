namespace TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
public class HistoricoPagamentoDTO
{
    public string IdPagamento { get; set; }
    public ApoliceDTO Apolice { get; set; }
    public DateTime Data { get; set; }
}