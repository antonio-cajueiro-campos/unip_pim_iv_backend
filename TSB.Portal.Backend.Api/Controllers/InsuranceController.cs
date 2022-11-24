using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSB.Portal.Backend.Api.Extensions;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.GetApolice;
using TSB.Portal.Backend.Application.UseCases.GetPriceSelectors;
using TSB.Portal.Backend.Application.UseCases.GetSinistro;
using TSB.Portal.Backend.Application.UseCases.RegistrateApolice;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InsuranceController : ControllerBase
{
	public IDefaultUseCase<GetPriceSelectorsOutput, GetPriceSelectorsInput> priceSelectors;
	public IDefaultUseCase<GetApoliceOutput, GetApoliceInput> getApolice;
	public IDefaultUseCase<GetSinistroOutput, GetSinistroInput> getHistoricoSinistro;
	public IDefaultUseCase<RegistrateApoliceOutput, RegistrateApoliceInput> registrateApolice;
    
	public InsuranceController(
		IDefaultUseCase<GetPriceSelectorsOutput, GetPriceSelectorsInput> priceSelectors,
		IDefaultUseCase<GetApoliceOutput, GetApoliceInput> getApolice,
		IDefaultUseCase<GetSinistroOutput, GetSinistroInput> getHistoricoSinistro,
		IDefaultUseCase<RegistrateApoliceOutput, RegistrateApoliceInput> registrateApolice)
	{
		this.priceSelectors = priceSelectors;
		this.getApolice = getApolice;
		this.getHistoricoSinistro = getHistoricoSinistro;
		this.registrateApolice = registrateApolice;
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

	[HttpGet("getApolice/{id}")]
	[SwaggerOperation("Retorna as apólices")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<GetApoliceOutput>))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<GetApoliceOutput>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<GetApoliceOutput>))]
	public IActionResult GetApolice([FromRoute][Required] long id)
	{
		var input = new GetApoliceInput();
		var result = this.getApolice.Handle(input.SetId(id));
		return new ObjectResult(result).SetStatus(result.Status);
	}

	[HttpGet("getHistoricoSinistros/{id}")]
	[SwaggerOperation("Retorna o histórico de sinistros do cliente")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<GetSinistroOutput>))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<GetSinistroOutput>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<GetSinistroOutput>))]
	public IActionResult GetHistoricoSinistro([FromRoute][Required] long id)
	{
		var input = new GetSinistroInput();
		input.IdCliente = id;
		var result = this.getHistoricoSinistro.Handle(input);
		return new ObjectResult(result).SetStatus(result.Status);
	}

	[HttpPost("registrateApolice")]
	[SwaggerOperation("Salva os dados de uma apólice")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<RegistrateApoliceOutput>))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<RegistrateApoliceOutput>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<RegistrateApoliceOutput>))]
	public IActionResult RegistrateApolice([FromBody][Required] RegistrateApoliceInput input)
	{
		var result = this.registrateApolice.Handle(input);
		return new ObjectResult(result).SetStatus(result.Status);
	}
}
