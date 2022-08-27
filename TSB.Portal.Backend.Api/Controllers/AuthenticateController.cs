using Microsoft.AspNetCore.Mvc;
using TSB.Portal.Backend.Application.UseCases.Authenticate;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase
{
    public AuthenticateUseCase authenticate;
    public AuthenticateController(AuthenticateUseCase authenticate)
    {
        this.authenticate = authenticate;
    }

    [HttpPost("validate")]
    public ObjectResult ValidateJwtToken()
    {
        return new ObjectResult("");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthenticateInput authenticate)
    {
        this.authenticate.Login(authenticate);

    	return new ObjectResult("");
    }
}