
using Microsoft.EntityFrameworkCore;
using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;
public class GetUserInfosUseCase : IDefaultUseCase<GetUserInfosOutput, GetUserInfosInput>
{
	private DataContext database { get; set; }

	public GetUserInfosUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<GetUserInfosOutput> Handle(GetUserInfosInput userInfosInput)
	{
		return this.GetUserInfos(userInfosInput);
	}

	private DefaultResponse<GetUserInfosOutput> GetUserInfos(GetUserInfosInput userInfosInput)
	{
		if (userInfosInput.Id == null && !userInfosInput.ClaimsPrincipal.HasClaim((e) => true))
		{
			return new()
			{
				Status = 400,
				Error = true,
				Data = null,
				Message = Messages.BadRequest
			};
		}

		try
		{
			var userId = userInfosInput.Id ?? userInfosInput.ClaimsPrincipal.GetUserId();
			var user = this.database.Users.Include(c => c.Credential).First(c => c.Id == userId);
			var userData = new GetUserInfosOutput();

			switch (user.Credential.Role) {
                case "Funcionario":
				var funcionario = this.database.Funcionarios.Include(c => c.User).First(c => c.User.Id == userId);
				userData.Funcionario = funcionario.MapObjectTo(new FuncionarioDTO());
				userData.Funcionario.User = funcionario.User.MapObjectTo(new UserDTO());
				userData.Funcionario.User.Credential = funcionario.User.Credential.MapObjectTo(new CredentialDTO());
                break;
                case "Cliente":
				var cliente = this.database.Clientes.Include(c => c.User).Include(c => c.Endereco).First(c => c.User.Id == userId);
				userData.Cliente = cliente.MapObjectTo(new ClienteDTO());
				userData.Cliente.User = cliente.User.MapObjectTo(new UserDTO());
				userData.Cliente.User.Credential = cliente.User.Credential.MapObjectTo(new CredentialDTO());
				userData.Cliente.Endereco = cliente.Endereco.MapObjectTo(new EnderecoDTO());
                break;
            }

			return new()
			{
				Status = 200,
				Error = false,
				Data = userData,
				Message = Messages.Success
			};
		}
		catch (InvalidOperationException)
		{
			return new()
			{
				Status = 404,
				Error = true,
				Data = null,
				Message = Messages.UserNotFound
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				Status = 500,
				Error = true,
				Data = null,
				Message = Messages.Error + ex
			};
		}
	}
}
