using TSB.Portal.Backend.Application.Transport;

namespace TSB.Portal.Backend.Application.UseCases.ValidateJwtToken.Interfaces;
public interface IValidateJwtTokenUseCase
{
	public DefaultResponse<ValidateJwtTokenOutput> Handle(ValidateJwtTokenInput validateJwtTokenInput);

}