using Microsoft.Extensions.DependencyInjection;

using TSB.Portal.Backend.Application.UseCases.UserRegister;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
using TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate;
public static class DIConfigure
{
	public static IServiceCollection AddDependecies(this IServiceCollection self)
	{
		self.AddScoped<IDefaultUseCase<AuthenticateOutput, AuthenticateInput>, AuthenticateUseCase>();
		self.AddScoped<IDefaultUseCase<UserRegisterOutput, UserRegisterInput>, UserRegisterUseCase>();
		self.AddScoped<IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput>, GetUserInfosUseCase>();
		self.AddScoped<IDefaultUseCase<ValidateJwtTokenOutput, ValidateJwtTokenInput>, ValidateJwtTokenUseCase>();
		
		return self;
	}
}
