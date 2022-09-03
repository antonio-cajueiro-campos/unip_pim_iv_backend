using Microsoft.AspNetCore.Mvc;
using TSB.Portal.Backend.Application.UseCases.UserRegister;
using TSB.Portal.Backend.Application.UseCases.UserRegister.Interfaces;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public IUserRegisterUseCase UserRegisterUseCase;
    public UserController(IUserRegisterUseCase UserRegisterUseCase)
    {
        this.UserRegisterUseCase = UserRegisterUseCase;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterInput userRegister)
    {
        var result = this.UserRegisterUseCase.Handle(userRegister);
    	return new ObjectResult(result);
    }
}
