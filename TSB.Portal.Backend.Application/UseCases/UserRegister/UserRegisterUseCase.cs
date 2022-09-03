using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.UserRegister.Interfaces;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Enums;

namespace TSB.Portal.Backend.Application.UseCases.UserRegister;
public class UserRegisterUseCase : IUserRegisterUseCase {
	private DataContext database { get; set; }
	public UserRegisterUseCase(DataContext database)
	{
		this.database = database;
	}
	public DefaultResponse<UserRegisterOutput> Handle(UserRegisterInput userRegisterInput, Roles role) {

		UserRegisterOutput userRegisterOutput = RegisterUser(userRegisterInput, role);

		return new () {
			Data = userRegisterOutput
		};
	}


	public UserRegisterOutput RegisterUser(UserRegisterInput userRegisterInput, Roles role) {

		var credential = userRegisterInput.MapObjectTo(new Credential());
		credential.Password = BCryptNet.HashPassword(credential.Password);
		credential.Role = role.ToString();

		var user = userRegisterInput.MapObjectTo(new User());
        user.RegistrationDate = DateTime.Now;

		this.database.Credentials.Add(credential);
		this.database.Users.Add(user);

		this.database.SaveChanges();

		return new UserRegisterOutput();		
	}
}