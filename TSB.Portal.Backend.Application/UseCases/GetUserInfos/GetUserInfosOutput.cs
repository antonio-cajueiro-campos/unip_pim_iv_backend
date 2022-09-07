namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;

public class GetUserInfosOutput
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Document { get; set; }
	public dynamic Credential { get; set; }
	public DateTime RegistrationDate { get; set; }
}
