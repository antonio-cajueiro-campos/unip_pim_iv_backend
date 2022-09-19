using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.UseCases.Authenticate;
using System.Text.RegularExpressions;
using TSB.Portal.Backend.CrossCutting.Enums;

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
		try {
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

			var user = userRegisterInput.MapObjectTo(new User());
			user.Credential = userRegisterInput.MapObjectTo(new Credential());
			user.RegistrationDate = DateTime.Now;

			user.Credential.Password = BCryptNet.HashPassword(user.Credential.Password);
			user.Credential.Role = userRegisterInput.Role != null ? userRegisterInput.Role.ToString() : Roles.Cliente.ToString();

			this.database.Users.Add(user);
			this.database.SaveChanges();
			
			database.Entry(user).GetDatabaseValues();

			if (user.Credential.Role == "Cliente") {
				var cliente = userRegisterInput.MapObjectTo(new Cliente());
				cliente.User = user;
				cliente.Endereco = userRegisterInput.Endereco.MapObjectTo(new Endereco());
				this.database.Clientes.Add(cliente);
				this.database.SaveChanges();

			} else if (user.Credential.Role == "Funcionario") {
				var funcionario = userRegisterInput.MapObjectTo(new Funcionario());
				funcionario.User = user;
				this.database.Funcionarios.Add(funcionario);
				this.database.SaveChanges();
			}

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
		} catch (Exception ex) {
			return new() {
				Status = 500,
				Error = true,
				Message = Messages.Error + ex,
				Data = null
			};
		}
	}
}