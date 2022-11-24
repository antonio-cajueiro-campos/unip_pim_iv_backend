using Microsoft.Extensions.DependencyInjection;

using TSB.Portal.Backend.Application.UseCases.UserRegister;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
using TSB.Portal.Backend.Application.UseCases.GetUserInfos;
using TSB.Portal.Backend.Application.UseCases.Authenticate;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.ChangeUserData;
using TSB.Portal.Backend.Application.UseCases.GetPriceSelectors;
using TSB.Portal.Backend.Application.UseCases.CompleteRegistration;
using TSB.Portal.Backend.Application.UseCases.EmployeeRegister;
using TSB.Portal.Backend.Application.UseCases.RegistrateSinistro;
using TSB.Portal.Backend.Application.UseCases.RegistrateCobertura;
using TSB.Portal.Backend.Application.UseCases.GetSinistro;
using TSB.Portal.Backend.Application.UseCases.GetApolice;
using TSB.Portal.Backend.Application.UseCases.RegistrateApolice;
using TSB.Portal.Backend.Application.UseCases.GetAllClients;
using TSB.Portal.Backend.Application.UseCases.GetClientById;
using TSB.Portal.Backend.Application.UseCases.ActiveInsurance;

namespace TSB.Portal.Backend.Application.Extensions;
public static class DIConfigure
{
	public static IServiceCollection AddDependecies(this IServiceCollection self)
	{
		self.AddScoped<IDefaultUseCase<AuthenticateOutput, AuthenticateInput>, AuthenticateUseCase>();
		self.AddScoped<IDefaultUseCase<ChangeUserDataOutput, ChangeUserDataInput>, ChangeUserDataUseCase>();
		self.AddScoped<IDefaultUseCase<CompleteRegistrationOutput, CompleteRegistrationInput>, CompleteRegistrationUseCase>();
		self.AddScoped<IDefaultUseCase<EmployeeRegisterOutput, EmployeeRegisterInput>, EmployeeRegisterUseCase>();
		self.AddScoped<IDefaultUseCase<GetPriceSelectorsOutput, GetPriceSelectorsInput>, GetPriceSelectorsUseCase>();
		self.AddScoped<IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput>, GetUserInfosUseCase>();
		self.AddScoped<IDefaultUseCase<RegistrateCoberturaOutput, RegistrateCoberturaInput>, RegistrateCoberturaUseCase>();
		self.AddScoped<IDefaultUseCase<RegistrateSinistroOutput, RegistrateSinistroInput>, RegistrateSinistroUseCase>();
		self.AddScoped<IDefaultUseCase<UserRegisterOutput, UserRegisterInput>, UserRegisterUseCase>();
		self.AddScoped<IDefaultUseCase<ValidateJwtTokenOutput, ValidateJwtTokenInput>, ValidateJwtTokenUseCase>();
		self.AddScoped<IDefaultUseCase<GetSinistroOutput, GetSinistroInput>, GetSinistroUseCase>();
		self.AddScoped<IDefaultUseCase<GetApoliceOutput, GetApoliceInput>, GetApoliceUseCase>();
		self.AddScoped<IDefaultUseCase<RegistrateApoliceOutput, RegistrateApoliceInput>, RegistrateApoliceUseCase>();
		self.AddScoped<IDefaultUseCase<GetClientByIdOutput, GetClientByIdInput>, GetClientByIdUseCase>();
		self.AddScoped<IDefaultUseCase<GetAllClientsOutput, GetAllClientsInput>, GetAllClientsUseCase>();
		self.AddScoped<IDefaultUseCase<ActiveInsuranceOutput, ActiveInsuranceInput>, ActiveInsuranceUseCase>();

		return self;
	}
}
