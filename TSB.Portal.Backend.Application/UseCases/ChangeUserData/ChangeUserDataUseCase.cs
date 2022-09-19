using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;

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
	public DefaultResponse<ChangeUserDataOutput> ChangeUserData(ChangeUserDataInput changeUserData)
	{
		try
		{
			var userId = changeUserData.ClaimsPrincipal.GetUserId();
			var UserRole = changeUserData.ClaimsPrincipal.GetUserRole();

			var user = this.database.Users.Include(c => c.Credential).First(c => c.Id == userId);
			user = changeUserData.MapObjectTo(user);

            if (changeUserData.Password != null) {
                user.Credential.Password = BCryptNet.HashPassword(changeUserData.Password);
            }

            database.Users.Update(user);
            database.SaveChanges();
            database.Entry(user).GetDatabaseValues();

            var dataOutput = user.MapObjectTo(new ChangeUserDataOutput());

			switch (UserRole)
			{
				case "Funcionario":
					var funcionario = this.database.Funcionarios.Include(c => c.User).First(c => c.User.Id == userId);
					funcionario = changeUserData.MapObjectTo(funcionario);
					database.Funcionarios.Update(funcionario);
                    database.SaveChanges();
                    database.Entry(funcionario).GetDatabaseValues();
                    dataOutput = funcionario.MapObjectTo(dataOutput);
					break;

				case "Cliente":
					var cliente = this.database.Clientes.Include(c => c.User).Include(c => c.Endereco).First(c => c.User.Id == userId);
					cliente = changeUserData.MapObjectTo(cliente);
					cliente.Endereco = changeUserData.MapObjectTo(cliente.Endereco);
					database.Clientes.Update(cliente);
			        database.SaveChanges();
                    database.Entry(cliente).GetDatabaseValues();
                    dataOutput = cliente.MapObjectTo(dataOutput);
					break;
			}



			return new()
			{
				Error = false,
				Status = 200,
				Message = Messages.Updated,
				Data = dataOutput
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
