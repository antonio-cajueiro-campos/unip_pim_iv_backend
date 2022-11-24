using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;

namespace TSB.Portal.Backend.Application.UseCases.RegistrateCobertura;

public class RegistrateCoberturaUseCase : IDefaultUseCase<RegistrateCoberturaOutput, RegistrateCoberturaInput>
{
	private readonly DataContext database;

	public RegistrateCoberturaUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<RegistrateCoberturaOutput> Handle(RegistrateCoberturaInput registrateCobertura)
	{
		return this.RegistrateCobertura(registrateCobertura);
	}
	
	private DefaultResponse<RegistrateCoberturaOutput> RegistrateCobertura(RegistrateCoberturaInput registrateCobertura)
	{
		try
		{
			if (registrateCobertura.ValorCobertura == decimal.Zero)
			{
				return new()
				{
					Status = 400,
					Error = true,
					Message = Messages.BadRequest,
					Data = null
				};
			}

			var Cobertura = registrateCobertura.MapObjectTo(new Cobertura());

			this.database.Coberturas.Add(Cobertura);
			this.database.SaveChanges();

			database.Entry(Cobertura).GetDatabaseValues();

			return new()
			{
				Status = 201,
				Error = false,
				Message = Messages.Created,
				Data = new ()
				{
					Cobertura = Cobertura
				}
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