namespace TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
public interface IAuthenticateUseCase {

	public AuthenticateOutput Authenticate(AuthenticateInput authenticateInput);

}