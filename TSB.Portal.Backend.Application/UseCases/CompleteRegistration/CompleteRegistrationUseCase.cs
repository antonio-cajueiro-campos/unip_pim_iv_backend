using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;
using Microsoft.EntityFrameworkCore;

namespace TSB.Portal.Backend.Application.UseCases.CompleteRegistration;

public class CompleteRegistrationUseCase : IDefaultUseCase<CompleteRegistrationOutput, CompleteRegistrationInput>
{
	private readonly DataContext database;

	public CompleteRegistrationUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<CompleteRegistrationOutput> Handle(CompleteRegistrationInput completeRegistration)
	{
		return this.CompleteRegistration(completeRegistration);
	}

	private DefaultResponse<CompleteRegistrationOutput> CompleteRegistration(CompleteRegistrationInput completeRegistration)
	{
		try
		{
			if (!completeRegistration.ClaimsPrincipal.HasClaim((e) => true))
			{
				return new()
				{
					Status = 401,
					Error = true,
					Data = null,
					Message = Messages.Unauthorized
				};
			}

			var userId = completeRegistration.ClaimsPrincipal.GetUserId();
			var userRole = completeRegistration.ClaimsPrincipal.GetUserRole();
			var cliente = this.database.Clientes.Include(c => c.User).Include(c => c.Endereco).FirstOrDefault(c => c.User.Id == userId);

			if (cliente.Endereco != null)
			{
				return new()
				{
					Status = 400,
					Error = true,
					Data = null,
					Message = Messages.InvalidOperation
				};
			}

			cliente.Endereco = completeRegistration.MapObjectTo(new Endereco());
			cliente.ChavePIX = completeRegistration.ChavePIX;
			cliente.Telefone = completeRegistration.Telefone;

			this.database.Clientes.Update(cliente);
			this.database.SaveChanges();

			return new()
			{
				Status = 200,
				Error = false,
				Message = Messages.Updated
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				Status = 500,
				Error = true,
				Message = Messages.Error + ex,
				Data = null
			};
		}
	}
}
