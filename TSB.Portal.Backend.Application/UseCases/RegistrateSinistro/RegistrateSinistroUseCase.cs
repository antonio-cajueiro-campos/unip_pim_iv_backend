using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;

namespace TSB.Portal.Backend.Application.UseCases.RegistrateSinistro;

public class RegistrateSinistroUseCase : IDefaultUseCase<RegistrateSinistroOutput, RegistrateSinistroInput>
{
	private readonly DataContext database;

	public RegistrateSinistroUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<RegistrateSinistroOutput> Handle(RegistrateSinistroInput registrateSinistro)
	{
		return this.RegistrateSinistro(registrateSinistro);
	}

	private DefaultResponse<RegistrateSinistroOutput> RegistrateSinistro(RegistrateSinistroInput registrateSinistro)
	{
		try
		{
			if (registrateSinistro.Tipo == null || registrateSinistro.ValorSinistro == decimal.Zero)
			{
				return new()
				{
					Status = 400,
					Error = true,
					Message = Messages.BadRequest,
					Data = null
				};
			}

			var Sinistro = registrateSinistro.MapObjectTo(new Sinistro());

			this.database.Sinistros.Add(Sinistro);
			this.database.SaveChanges();

			return new()
			{
				Status = 201,
				Error = false,
				Message = Messages.Created
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