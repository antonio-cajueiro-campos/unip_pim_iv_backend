using Microsoft.AspNetCore.Mvc;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
   
    public UserController()
    {
    }

    [HttpGet(Name = "GetUser")]
    public ObjectResult GetUser()
    {
        return new ObjectResult("");
    }
}
