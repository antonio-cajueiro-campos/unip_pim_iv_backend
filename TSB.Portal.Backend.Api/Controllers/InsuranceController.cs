using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSB.Portal.Backend.Api.Extensions;
using TSB.Portal.Backend.Application.EntitiesUseCase;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.GetPriceSelectors;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InsuranceController : ControllerBase
{
	public IDefaultUseCase<GetPriceSelectorsOutput, GetPriceSelectorsInput> priceSelectors;
    
	public InsuranceController(IDefaultUseCase<GetPriceSelectorsOutput, GetPriceSelectorsInput> priceSelectors)
	{
		this.priceSelectors = priceSelectors;
	}

	[HttpGet("getPriceSelectors")]
	[SwaggerOperation("Retorna os seletores de preço")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<GetPriceSelectorsOutput>))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<GetPriceSelectorsOutput>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<GetPriceSelectorsOutput>))]
	public IActionResult GetPriceSelectors()
	{
		var result = this.priceSelectors.Handle(null);
		return new ObjectResult(result).SetStatus(result.Status);
	}
}
