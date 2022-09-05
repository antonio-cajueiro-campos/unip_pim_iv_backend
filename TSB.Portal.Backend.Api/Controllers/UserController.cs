using Microsoft.AspNetCore.Mvc;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos;
using TSB.Portal.Backend.Application.UseCases.UserRegister;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public IDefaultUseCase<UserRegisterOutput, UserRegisterInput> UserRegisterUseCase;
    public IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput> GetUserInfosUseCase;
    public UserController(IDefaultUseCase<UserRegisterOutput, UserRegisterInput> userRegisterUseCase, IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput> getUserInfosUseCase)
    {
        this.UserRegisterUseCase = userRegisterUseCase;
        this.GetUserInfosUseCase = getUserInfosUseCase;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<UserRegisterOutput>))]
    public IActionResult Register([FromBody] UserRegisterInput userRegister)
    {
        var result = this.UserRegisterUseCase.Handle(userRegister);
    	return new ObjectResult(result);
    }

    [HttpGet("infos")]
    public IActionResult GetUserInfos()
    {
        var result = this.GetUserInfosUseCase.Handle(new ()
        {
            Token = Request.Headers["Authorization"]
        });
    	return new ObjectResult(result);
    }

    [HttpGet("infos/{id}")]
    public IActionResult GetUserInfosById(long id)
    {
        var result = this.GetUserInfosUseCase.Handle(new ()
        {
            Id = id
        });
    	return new ObjectResult(result);
    }
}
