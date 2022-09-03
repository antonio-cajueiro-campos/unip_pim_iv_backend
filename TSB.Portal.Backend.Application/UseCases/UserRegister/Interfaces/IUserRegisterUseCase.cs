using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Enums;

namespace TSB.Portal.Backend.Application.UseCases.UserRegister.Interfaces;
public interface IUserRegisterUseCase
{
	public DefaultResponse<UserRegisterOutput> Handle(UserRegisterInput userRegisterInput);

}