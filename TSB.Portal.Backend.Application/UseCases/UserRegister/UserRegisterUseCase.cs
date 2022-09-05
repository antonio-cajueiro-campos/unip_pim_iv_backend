using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.UseCases.Authenticate;
using TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;

namespace TSB.Portal.Backend.Application.UseCases.UserRegister;
public class UserRegisterUseCase : IDefaultUseCase<UserRegisterOutput, UserRegisterInput> {
	private DataContext database { get; set; }
	private IDefaultUseCase<AuthenticateOutput, AuthenticateInput> authenticateUseCase { get; set; }
	public UserRegisterUseCase(DataContext database, IDefaultUseCase<AuthenticateOutput, AuthenticateInput> authenticateUseCase)
	{
		this.database = database;
		this.authenticateUseCase = authenticateUseCase;
	}
	public DefaultResponse<UserRegisterOutput> Handle(UserRegisterInput userRegisterInput) {
		return RegisterUser(userRegisterInput);
	}


	public DefaultResponse<UserRegisterOutput> RegisterUser(UserRegisterInput userRegisterInput) {

		if (this.database.Credentials.Any(x => x.Username == userRegisterInput.Username))
			return new() {
				StatusCode = 400,
				Error = true,
				Message = Messages.UserAlreadyTaken,
				Data = null
			};

		var credential = userRegisterInput.MapObjectTo(new Credential());
		credential.Password = BCryptNet.HashPassword(credential.Password);
		credential.Role = userRegisterInput.Role != null ? userRegisterInput.Role.ToString() : "Segurado";

		var user = userRegisterInput.MapObjectTo(new User());
        user.RegistrationDate = DateTime.Now;

		this.database.Credentials.Add(credential);
		this.database.Users.Add(user);

		this.database.SaveChanges();

		var authenticateResponse = authenticateUseCase.Handle(new() {
			Username = userRegisterInput.Username,
			Password = userRegisterInput.Password
		});

		System.Console.WriteLine(authenticateResponse.Data.Token);

		return new() {
			StatusCode = 201,
			Error = false,
			Message = Messages.Created,
			Data = new() {
				Token = authenticateResponse.Data.Token
			}
		};
	}
}