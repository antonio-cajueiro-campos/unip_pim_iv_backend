using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSB.Portal.Backend.Api.Extensions;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.Authenticate;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase
{
    public IDefaultUseCase<AuthenticateOutput, AuthenticateInput> authenticate;
    public IDefaultUseCase<ValidateJwtTokenOutput, ValidateJwtTokenInput> validateJwtToken;
    public AuthenticateController(IDefaultUseCase<AuthenticateOutput, AuthenticateInput> authenticate, IDefaultUseCase<ValidateJwtTokenOutput, ValidateJwtTokenInput> validateJwtToken)
    {
        this.authenticate = authenticate;
        this.validateJwtToken = validateJwtToken;
    }

    [HttpPost("login")]
    [SwaggerOperation("Autenticação do usuário")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<AuthenticateOutput>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<AuthenticateOutput>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<AuthenticateOutput>))]
    public IActionResult Login([FromBody] AuthenticateInput authenticate)
    {
        var result = this.authenticate.Handle(authenticate);
    	return new ObjectResult(result).SetStatus(result.Status);
    }

    [HttpPost("validate")]
    [SwaggerOperation("Valida o Bearer JWT enviado pelo header")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<ValidateJwtTokenOutput>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DefaultResponse<ValidateJwtTokenOutput>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<ValidateJwtTokenOutput>))]
    public ObjectResult ValidateJwtToken()
    {
        var result = this.validateJwtToken.Handle(new (Request.Headers["Authorization"]));
        return new ObjectResult(result).SetStatus(result.Status);
    }
}