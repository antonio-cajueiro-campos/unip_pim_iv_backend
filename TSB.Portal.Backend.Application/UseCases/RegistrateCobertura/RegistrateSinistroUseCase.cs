using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;

namespace TSB.Portal.Backend.Application.UseCases.RegistrateCobertura;

public class RegistrateCoberturaUseCase : IDefaultUseCase<RegistrateCoberturaOutput, RegistrateCoberturaInput>
{
private DataContext database { get; set; }
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
		try {
			
			//get user by id

			// set new infos
			var Cobertura = registrateCobertura.MapObjectTo(new Cobertura());
			
			//update user
			//this.database.Users.Add(user);
			this.database.SaveChanges();

			return new() {
				Status = 200,
				Error = false,
				Message = Messages.Success
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