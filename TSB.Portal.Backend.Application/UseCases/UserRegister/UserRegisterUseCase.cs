using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.UserRegister.Interfaces;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;
using TSB.Portal.Backend.Infra.Repositories.CredentialRepository.Entity;

namespace TSB.Portal.Backend.Application.UseCases.UserRegister;
public class UserRegisterUseCase : IUserRegisterUseCase {
	private DataContext database { get; set; }
	public UserRegisterUseCase(DataContext database)
	{
		this.database = database;
	}
	public DefaultResponse<UserRegisterOutput> Handle(UserRegisterInput userRegisterInput) {

		UserRegisterOutput userRegisterOutput = RegisterUser(userRegisterInput);

		return new () {
			Data = userRegisterOutput
		};
	}


	public UserRegisterOutput RegisterUser(UserRegisterInput userRegisterInput) {

		var credential = userRegisterInput.MapObjectTo(new Credential());

		credential.Password = BCryptNet.HashPassword(credential.Password);
        //credential.RegistrationDate = DateTime.Now;
		this.database.Credentials.Add(credential);
		this.database.SaveChanges();

		return new UserRegisterOutput();		
	}
}