using Microsoft.AspNetCore.Mvc;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<AuthenticateOutput>))]
    public IActionResult Login([FromBody] AuthenticateInput authenticate)
    {
        var result = this.authenticate.Handle(authenticate);
        
    	return new ObjectResult(result).SetStatus(result.Status);
    }

    [HttpPost("validate")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<ValidateJwtTokenOutput>))]
    public ObjectResult ValidateJwtToken()
    {
        var result = this.validateJwtToken.Handle(new ()
        {
            Token = Request.Headers["Authorization"]
        });

        return new ObjectResult(result).SetStatus(result.Status);
    }
}