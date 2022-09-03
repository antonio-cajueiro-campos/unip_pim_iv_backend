using Microsoft.AspNetCore.Mvc;
using TSB.Portal.Backend.Application.UseCases.Authenticate;
using TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken.Interfaces;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase
{
    public IAuthenticateUseCase authenticate;
    public IValidateJwtTokenUseCase validateJwtTokenUseCase;
    public AuthenticateController(IAuthenticateUseCase authenticate, IValidateJwtTokenUseCase validateJwtTokenUseCase)
    {
        this.authenticate = authenticate;
        this.validateJwtTokenUseCase = validateJwtTokenUseCase;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthenticateInput authenticate)
    {
        var result = this.authenticate.Handle(authenticate);
    	return new ObjectResult(result);
    }

    [HttpPost("validate")]
    public ObjectResult ValidateJwtToken()
    {
        var result = this.validateJwtTokenUseCase.Handle(new ()
        {
            Token = Request.Headers["Authorization"]
        });

        return new ObjectResult(result);
    }
}