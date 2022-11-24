
using Microsoft.EntityFrameworkCore;
using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;

namespace TSB.Portal.Backend.Application.UseCases.GetAllClients;
public class GetAllClientsUseCase : IDefaultUseCase<GetAllClientsOutput, GetAllClientsInput>
{
	private readonly DataContext database;

	public GetAllClientsUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<GetAllClientsOutput> Handle(GetAllClientsInput clientsInput)
	{
		return this.GetAllClients(clientsInput);
	}

	private DefaultResponse<GetAllClientsOutput> GetAllClients(GetAllClientsInput clientsInput)
	{
		try
		{
			var clientes = database.Clientes
				.Include(c => c.User)
				.Include(c => c.Endereco)
				.ToList();

			return new()
			{
				Status = 200,
				Error = false,
				Message = Messages.Success,
				Data = new()
				{
					Clientes = clientes
				}
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
