using Microsoft.Extensions.DependencyInjection;

using TSB.Portal.Backend.Application.UseCases.UserRegister;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos;
using TSB.Portal.Backend.Application.UseCases.Authenticate;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.ChangeUserData;

namespace TSB.Portal.Backend.Application.Extensions;
public static class DIConfigure
{
	public static IServiceCollection AddDependecies(this IServiceCollection self)
	{
		self.AddScoped<IDefaultUseCase<AuthenticateOutput, AuthenticateInput>, AuthenticateUseCase>();
		self.AddScoped<IDefaultUseCase<UserRegisterOutput, UserRegisterInput>, UserRegisterUseCase>();
		self.AddScoped<IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput>, GetUserInfosUseCase>();
		self.AddScoped<IDefaultUseCase<ValidateJwtTokenOutput, ValidateJwtTokenInput>, ValidateJwtTokenUseCase>();
		self.AddScoped<IDefaultUseCase<EmployeeRegisterOutput, EmployeeRegisterInput>, EmployeeRegisterUseCase>();
		self.AddScoped<IDefaultUseCase<ChangeUserDataOutput, ChangeUserDataInput>, ChangeUserDataUseCase>();		

		return self;
	}
}
