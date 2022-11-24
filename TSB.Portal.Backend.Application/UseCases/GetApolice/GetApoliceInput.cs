namespace TSB.Portal.Backend.Application.UseCases.GetApolice;

public class GetApoliceInput
{
	public long IdCliente { get; set; }

	public GetApoliceInput SetId(long id)
	{
		this.IdCliente = id;
		return this;
	}
}