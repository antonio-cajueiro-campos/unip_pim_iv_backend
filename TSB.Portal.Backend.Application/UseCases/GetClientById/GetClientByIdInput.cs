using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Application.UseCases.GetClientById;
public class GetClientByIdInput
{
	[Required]
	public long Id { get; set; }

	public GetClientByIdInput SetId(long id)
	{
		this.Id = id;
		return this;
	}
}
