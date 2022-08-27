using Microsoft.AspNetCore.Mvc;
using TSB.Portal.Backend.Application.UseCases.GetAuthenticate.Models;

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
    public IActionResult Login([FromBody] AuthenticateDTO authenticate)
    {
    	return new ObjectResult("");
    }
}