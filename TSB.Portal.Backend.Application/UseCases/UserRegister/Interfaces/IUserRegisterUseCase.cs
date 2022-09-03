using TSB.Portal.Backend.Application.Transport;

namespace TSB.Portal.Backend.Application.UseCases.UserRegister.Interfaces;
public interface IUserRegisterUseCase
{
	public DefaultResponse<UserRegisterOutput> Handle(UserRegisterInput credentials);

}