using TSB.Portal.Backend.CrossCutting.Enums;

namespace TSB.Portal.Backend.Application.UseCases.RegistrateApolice;

public class RegistrateApoliceInput
{
	public long ClienteId { get; set; }
	public decimal ValorCobertura { get; set; }
	public IDictionary<Sinistros, decimal> Sinistros { get; set; }
}