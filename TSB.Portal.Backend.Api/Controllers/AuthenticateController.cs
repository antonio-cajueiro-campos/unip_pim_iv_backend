 using Microsoft.AspNetCore.Mvc;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase
{
    public AuthenticateController()
    {
    }

    [HttpPost("validate")]
    public ObjectResult ValidateJwtToken()
    {
        return new ObjectResult("");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] CredentialsDTO credentials)
    {
    	return new ObjectResult("");
    }
}