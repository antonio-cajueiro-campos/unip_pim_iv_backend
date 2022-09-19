namespace TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
public class UserDTO
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Document { get; set; }
	public CredentialDTO Credential { get; set; }
	public DateTime RegistrationDate { get; set; }
}