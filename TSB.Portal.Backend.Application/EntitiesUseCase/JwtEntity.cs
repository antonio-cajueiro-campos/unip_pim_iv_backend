namespace TSB.Portal.Backend.Application.EntitiesUseCase;

public class JwtEntity {
	public string Token { get; set; }
	public string RefreshToken { get; set; }
	public DateTime? ExpirationTime { get; set; }
}
