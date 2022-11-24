using System.ComponentModel.DataAnnotations;
using TSB.Portal.Backend.CrossCutting.Enums;

namespace TSB.Portal.Backend.Application.UseCases.ActiveInsurance;

public class ActiveInsuranceInput
{
	[Required]
	public long Id { get; set; }

	[Required]
	public Sinistros Tipo { get; set; }

	public ActiveInsuranceInput()
	{
	}
}