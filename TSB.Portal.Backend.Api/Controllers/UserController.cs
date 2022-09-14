using Microsoft.AspNetCore.Mvc;
using TSB.Portal.Backend.Api.Extensions;
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
    public IActionResult UserRegister([FromBody] UserRegisterInput userRegister)
    {
        var result = this.userRegister.Handle(userRegister);

    	return new ObjectResult(result).SetStatus(result.Status);
    }

    [HttpGet("infos")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    public IActionResult GetUserInfos()
    {
        var result = this.getUserInfos.Handle(new ()
        {
            ClaimsPrincipal = HttpContext.User
        });

    	return new ObjectResult(result).SetStatus(result.Status);
    }

    [HttpGet("infos/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    public IActionResult GetUserInfosById(long id)
    {
        var result = this.getUserInfos.Handle(new ()
        {
            Id = id
        });
        
    	return new ObjectResult(result).SetStatus(result.Status);
    }
}
