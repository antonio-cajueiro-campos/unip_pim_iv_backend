namespace TSB.Portal.Backend.Application.UseCases.GetSinistro;

public class GetSinistroOutput
{
	public DateTime Ocorrencia { get; set; }
	public string TipoSinistro { get; set; }
	public decimal Valor { get; set; }
}