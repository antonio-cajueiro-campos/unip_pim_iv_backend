namespace TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
public class ValidateJwtTokenOutput
{
	public bool IsValidToken { get; set; }
	public DateTime? TokenExpire { get; set; }

	public ValidateJwtTokenOutput(bool isValidToken, DateTime? tokenExpire)
	{
		IsValidToken = isValidToken;
		TokenExpire = tokenExpire;
	}

	public ValidateJwtTokenOutput()	{}
}