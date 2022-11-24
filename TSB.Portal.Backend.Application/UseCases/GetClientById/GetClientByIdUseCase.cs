
using Microsoft.EntityFrameworkCore;
using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;

namespace TSB.Portal.Backend.Application.UseCases.GetClientById;
public class GetClientByIdUseCase : IDefaultUseCase<GetClientByIdOutput, GetClientByIdInput>
{
	private readonly DataContext database;

	public GetClientByIdUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<GetClientByIdOutput> Handle(GetClientByIdInput clientByIdInput)
	{
		return this.GetClientById(clientByIdInput);
	}

	private DefaultResponse<GetClientByIdOutput> GetClientById(GetClientByIdInput clientByIdInput)
	{
		try
		{
			var cliente = database.Clientes
				.Include(c => c.User)
				.Include(c => c.Endereco)
				.FirstOrDefault(c => c.Id == clientByIdInput.Id);

			if (cliente == null) return UserNotFoundError();

			return new()
			{
				Status = 200,
				Error = false,
				Message = Messages.Success,
				Data = new()
				{
					Cliente = cliente
				}
			};
		}
		catch (InvalidOperationException)
		{
			return UserNotFoundError();
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

	private DefaultResponse<GetClientByIdOutput> UserNotFoundError()
	{
		return new()
			{
				Status = 404,
				Error = true,
				Data = null,
				Message = Messages.UserNotFound
			};
	}
}
