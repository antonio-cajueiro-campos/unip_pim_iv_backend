using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSB.Portal.Backend.Api.Extensions;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.ActiveInsurance;
using TSB.Portal.Backend.Application.UseCases.GetAllClients;
using TSB.Portal.Backend.Application.UseCases.GetClientById;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FuncionarioController : ControllerBase
{
	public IDefaultUseCase<GetClientByIdOutput, GetClientByIdInput> getClientById;
	public IDefaultUseCase<GetAllClientsOutput, GetAllClientsInput> getAllClients;
	public IDefaultUseCase<ActiveInsuranceOutput, ActiveInsuranceInput> activeInsurance;
    
	public FuncionarioController(
		IDefaultUseCase<GetClientByIdOutput, GetClientByIdInput> getClientById,
		IDefaultUseCase<GetAllClientsOutput, GetAllClientsInput> getAllClients,
		IDefaultUseCase<ActiveInsuranceOutput, ActiveInsuranceInput> activeInsurance)
	{
		this.getClientById = getClientById;
		this.getAllClients = getAllClients;
		this.activeInsurance = activeInsurance;
	}

	[HttpGet("getClient/{id}")]
	[SwaggerOperation("Retorna o cliente por seu id")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<GetClientByIdOutput>))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<GetClientByIdOutput>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<GetClientByIdOutput>))]
	public IActionResult GetClienteById([FromRoute][Required] long id)
	{
		var input = new GetClientByIdInput();
		var result = this.getClientById.Handle(input.SetId(id));
		return new ObjectResult(result).SetStatus(result.Status);
	}

	[HttpGet("getAllClients")]
	[SwaggerOperation("Retorna todos os Clientes")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<GetAllClientsOutput>))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<GetAllClientsOutput>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<GetAllClientsOutput>))]
	public IActionResult GetAllClients()
	{
		var result = this.getAllClients.Handle(null);
		return new ObjectResult(result).SetStatus(result.Status);
	}

	[HttpGet("activeInsurance")]
	[SwaggerOperation("Ativa o seguro do Cliente")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<ActiveInsuranceOutput>))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<ActiveInsuranceOutput>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<ActiveInsuranceOutput>))]
	public IActionResult ActiveInsurance([FromQuery][Required] ActiveInsuranceInput input)
	{
		var result = this.activeInsurance.Handle(input);
		return new ObjectResult(result).SetStatus(result.Status);
	}
}
