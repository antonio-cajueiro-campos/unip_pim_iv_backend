using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;

namespace TSB.Portal.Backend.Application.UseCases.ChangeUserData;
public class ChangeUserDataUseCase : IDefaultUseCase<ChangeUserDataOutput, ChangeUserDataInput>
{
	private DataContext database { get; set; }
	public ChangeUserDataUseCase(DataContext database)
	{
		this.database = database;
	}
	public DefaultResponse<ChangeUserDataOutput> Handle(ChangeUserDataInput changeUserData)
	{
		return this.ChangeUserData(changeUserData);
	}
	private DefaultResponse<ChangeUserDataOutput> ChangeUserData(ChangeUserDataInput changeUserData)
	{
		try
		{
			var userId = changeUserData.ClaimsPrincipal.GetUserId();
			var UserRole = changeUserData.ClaimsPrincipal.GetUserRole();

			var user = this.database.Users.Include(c => c.Credential).FirstOrDefault(c => c.Id == userId);

			if (user == null) return this.GetUserNotFoundError();

			user = changeUserData.MapObjectToIfNotNull(user);

			if (changeUserData.Password != null)
			{
				user.Credential.Password = BCryptNet.HashPassword(changeUserData.Password);
			}

			database.Users.Update(user);
			database.SaveChanges();
			database.Entry(user).GetDatabaseValues();

			var dataOutput = user.MapObjectTo(new ChangeUserDataOutput());

			dataOutput.Credential = user.Credential.MapObjectTo(new CredentialDTO());

			switch (UserRole)
			{
				case "Funcionario":
					var funcionario = this.database.Funcionarios.Include(c => c.User).FirstOrDefault(c => c.User.Id == userId);

					if (funcionario == null) return this.GetUserNotFoundError();

					funcionario = changeUserData.MapObjectToIfNotNull(funcionario);
					database.Funcionarios.Update(funcionario);
					database.SaveChanges();
					database.Entry(funcionario).GetDatabaseValues();
					dataOutput = funcionario.MapObjectTo(dataOutput);
					break;

				case "Cliente":
					var cliente = this.database.Clientes.Include(c => c.User).Include(c => c.Endereco).FirstOrDefault(c => c.User.Id == userId);

					if (cliente == null) return this.GetUserNotFoundError();

					cliente = changeUserData.MapObjectToIfNotNull(cliente);
					cliente.Endereco = changeUserData.MapObjectToIfNotNull(cliente.Endereco);
					database.Clientes.Update(cliente);
					database.SaveChanges();
					database.Entry(cliente).GetDatabaseValues();
					dataOutput = cliente.MapObjectTo(dataOutput);
					dataOutput.Endereco = cliente.Endereco.MapObjectTo(new EnderecoDTO());
					break;
			}

			return GetUpdatedOk(dataOutput);
		}
		catch (InvalidOperationException)
		{
			return GetInvalidOperation();
		}
		catch (Exception ex)
		{
			return GetServerError(ex);
		}
	}

	private DefaultResponse<ChangeUserDataOutput> GetUpdatedOk(ChangeUserDataOutput data)
	{
		return new()
		{
			Error = false,
			Status = 200,
			Message = Messages.Updated,
			Data = data
		};
	}
    
    private DefaultResponse<ChangeUserDataOutput> GetInvalidOperation()
	{
		return new()
		{
			Status = 400,
			Error = true,
			Data = null,
			Message = Messages.InvalidOperation
		};
	}

    private DefaultResponse<ChangeUserDataOutput> GetUserNotFoundError()
	{
		return new()
		{
			Status = 404,
			Error = true,
			Data = null,
			Message = Messages.UserNotFound
		};
	}

	private DefaultResponse<ChangeUserDataOutput> GetServerError(Exception ex)
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
