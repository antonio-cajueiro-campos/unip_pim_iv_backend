namespace TSB.Portal.Backend.Application.UseCases.GetApolice;

public class GetApoliceOutput
{
	public DateTime Vigencia { get; set; }
	public DateTime Emissao { get; set; }
	public IDictionary<string, decimal> Sinistros { get; set; }
	public decimal ValorCobertura { get; set; }
}