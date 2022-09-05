using Microsoft.AspNetCore.Mvc;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos;
using TSB.Portal.Backend.Application.UseCases.UserRegister;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public IDefaultUseCase<UserRegisterOutput, UserRegisterInput> userRegister;
    public IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput> getUserInfos;
    public UserController(IDefaultUseCase<UserRegisterOutput, UserRegisterInput> userRegister, IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput> getUserInfos)
    {
        this.userRegister = userRegister;
        this.getUserInfos = getUserInfos;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<UserRegisterOutput>))]
    public IActionResult Register([FromBody] UserRegisterInput userRegister)
    {
        var result = this.userRegister.Handle(userRegister);
    	return new ObjectResult(result);
    }

    [HttpGet("infos")]
    public IActionResult GetUserInfos()
    {
        var result = this.getUserInfos.Handle(new ()
        {
            Token = Request.Headers["Authorization"]
        });
    	return new ObjectResult(result);
    }

    [HttpGet("infos/{id}")]
    public IActionResult GetUserInfosById(long id)
    {
        var result = this.getUserInfos.Handle(new ()
        {
            Id = id
        });
    	return new ObjectResult(result);
    }
}
