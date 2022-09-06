using System.ComponentModel.DataAnnotations;
using TSB.Portal.Backend.Application.EntitiesUseCase;

namespace TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
public class ValidateJwtTokenInput {
	public string Token { get; set; }	
}