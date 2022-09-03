using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos.Interfaces;
using TSB.Portal.Backend.Application.UseCases.UserRegister;
using TSB.Portal.Backend.Application.UseCases.UserRegister.Interfaces;
using TSB.Portal.Backend.CrossCutting.Enums;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public IUserRegisterUseCase UserRegisterUseCase;
    public IGetUserInfosUseCase GetUserInfosUseCase;
    public UserController(IUserRegisterUseCase userRegisterUseCase, IGetUserInfosUseCase getUserInfosUseCase)
    {
        this.UserRegisterUseCase = userRegisterUseCase;
        this.GetUserInfosUseCase = getUserInfosUseCase;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<UserRegisterOutput>))]
    public IActionResult Register([FromBody] UserRegisterInput userRegister, [FromRoute] Roles roles)
    {
        var result = this.UserRegisterUseCase.Handle(userRegister, roles);
    	return new ObjectResult(result);
    }

    [HttpGet("user-infos")]
    public IActionResult GetUserInfos()
    {
        var result = this.GetUserInfosUseCase.Handle(new ()
        {
            Token = Request.Headers["Authorization"]
        });
    	return new ObjectResult(result);
    }

    [HttpGet("user-infos/{id}")]
    public IActionResult GetUserInfosById(long id)
    {
        var result = this.GetUserInfosUseCase.Handle(new ()
        {
            Id = id
        });
    	return new ObjectResult(result);
    }
}
