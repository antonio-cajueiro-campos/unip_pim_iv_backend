using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.UseCases.Authenticate;
using System.Text.RegularExpressions;

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
		return UserRegister(userRegisterInput);
	}

	private DefaultResponse<UserRegisterOutput> UserRegister(UserRegisterInput userRegisterInput) {

		var username = userRegisterInput.Username = userRegisterInput.Username.Trim().ToLower();
		var document = userRegisterInput.Document = Regex.Replace(userRegisterInput.Document, "[^0-9]", "", RegexOptions.IgnoreCase);

		if (this.database.Credentials.Any(x => x.Username == username))
			return new() {
				Status = 400,
				Error = true,
				Message = Messages.UsernameAlreadyTaken(username),
				Data = null
			};

		if (this.database.Users.Any(x => x.Document == document))
			return new() {
				Status = 400,
				Error = true,
				Message = Messages.DocumentAlreadyTaken(document),
				Data = null
			};

		var credential = userRegisterInput.MapObjectTo(new Credential());
		credential.Password = BCryptNet.HashPassword(credential.Password);
		credential.Role = userRegisterInput.Role != null ? userRegisterInput.Role.ToString() : "Cliente";

		var user = userRegisterInput.MapObjectTo(new User());
		user.Credential = credential;
        user.RegistrationDate = DateTime.Now;

		this.database.Users.Add(user);
		this.database.SaveChanges();

		var authenticateResponse = authenticateUseCase.Handle(
			userRegisterInput.MapObjectTo(new AuthenticateInput())
		);

		return new() {
			Status = 201,
			Error = false,
			Message = Messages.Created,
			Data = new() {
				Jwt = authenticateResponse.Data.Jwt
			}
		};
	}
}