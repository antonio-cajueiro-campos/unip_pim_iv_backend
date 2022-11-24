namespace TSB.Portal.Backend.Application.EntitiesUseCase;

public class PriceSelector {
	public string Id { get; set; }
	public string Title { get; set; }
	public string Icon { get; set; }
	public List<Selector> List { get; set; }
	public decimal MonthlyPayment { get; set; }
	public int NumberOfMonths { get; set; }
	public double SinistroTax { get; set; }
}
