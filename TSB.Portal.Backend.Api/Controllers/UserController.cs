using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSB.Portal.Backend.Api.Extensions;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.ChangeUserData;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos;
using TSB.Portal.Backend.Application.UseCases.UserRegister;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public IDefaultUseCase<UserRegisterOutput, UserRegisterInput> userRegister;
    public IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput> getUserInfos;
    public IDefaultUseCase<ChangeUserDataOutput, ChangeUserDataInput> changeUserData;
    public UserController
    (
        IDefaultUseCase<UserRegisterOutput, UserRegisterInput> userRegister, 
        IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput> getUserInfos,
        IDefaultUseCase<ChangeUserDataOutput, ChangeUserDataInput> changeUserData
    ) {
        this.userRegister = userRegister;
        this.getUserInfos = getUserInfos;
        this.changeUserData = changeUserData;
    }

    [HttpPost("register")]
    [SwaggerOperation("Registra usuário no sistema")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DefaultResponse<UserRegisterOutput>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DefaultResponse<UserRegisterOutput>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<UserRegisterOutput>))]
    public IActionResult UserRegister([FromBody] UserRegisterInput userRegister)
    {
        var result = this.userRegister.Handle(userRegister);
    	return new ObjectResult(result).SetStatus(result.Status);
    }

    [HttpGet("infos")]
    [SwaggerOperation("Obtém as informações do usuário registrado e autenticado no sistema")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    public IActionResult GetUserInfos()
    {
        var result = this.getUserInfos.Handle(new (HttpContext.User));
    	return new ObjectResult(result).SetStatus(result.Status);
    }

    [HttpGet("infos/{id}")]
    [SwaggerOperation("Obtém as informações do usuário registrado no sistema pelo ID")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<GetUserInfosOutput>))]
    public IActionResult GetUserInfosById(long id)
    {
        var result = this.getUserInfos.Handle(new (id));
    	return new ObjectResult(result).SetStatus(result.Status);
    }

    [HttpPost("infos")]
    [SwaggerOperation("Atualiza as informações do usuário registrado e autenticado no sistema")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultResponse<ChangeUserDataOutput>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultResponse<ChangeUserDataOutput>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DefaultResponse<ChangeUserDataOutput>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DefaultResponse<ChangeUserDataOutput>))]
    public IActionResult ChangeUserData([FromBody] ChangeUserDataInput changeUserData)
    {
        changeUserData.ClaimsPrincipal = HttpContext.User;
        var result = this.changeUserData.Handle(changeUserData);

        return new ObjectResult(result).SetStatus(result.Status);
    }
}
