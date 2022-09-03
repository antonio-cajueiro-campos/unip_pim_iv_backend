using Microsoft.Extensions.DependencyInjection;

using TSB.Portal.Backend.Application.UseCases.UserRegister;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
using TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
using TSB.Portal.Backend.Application.UseCases.UserRegister.Interfaces;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken.Interfaces;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate;
public static class DIConfigure
{
	public static IServiceCollection AddDependecies(this IServiceCollection self)
	{
		self.AddScoped<IAuthenticateUseCase, AuthenticateUseCase>();
		self.AddScoped<IUserRegisterUseCase, UserRegisterUseCase>();
		self.AddScoped<IValidateJwtTokenUseCase, ValidateJwtTokenUseCase>();
		
		return self;
	}
}
