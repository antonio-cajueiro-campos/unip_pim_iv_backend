using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;

public class CompleteRegistrationUseCase : IDefaultUseCase<CompleteRegistrationOutput, CompleteRegistrationInput>
{
private DataContext database { get; set; }
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
		try {
			
			//get user by id

			// set new infos
			var Endereco = completeRegistration.MapObjectTo(new Endereco());
			
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