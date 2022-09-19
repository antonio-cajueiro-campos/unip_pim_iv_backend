namespace TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
public class ValidateJwtTokenInput {
	public string Token { get; set; }

	public ValidateJwtTokenInput(string token) {
		this.Token = token;
	}
}