using System.Text;
using TSB.Portal.Backend.Application.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.CrossCutting.Constants;

namespace TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
public class ValidateJwtTokenUseCase : IDefaultUseCase<ValidateJwtTokenOutput, ValidateJwtTokenInput>
{
	private readonly DataContext database;
	private readonly IConfiguration configuration;
	public ValidateJwtTokenUseCase(DataContext database, IConfiguration configuration)
	{
		this.database = database;
		this.configuration = configuration;
	}

	public DefaultResponse<ValidateJwtTokenOutput> Handle(ValidateJwtTokenInput validateJwtTokenInput)
	{
		return ValidateJwtToken(validateJwtTokenInput);
	}


	private DefaultResponse<ValidateJwtTokenOutput> ValidateJwtToken(ValidateJwtTokenInput validateJwtTokenInput)
	{
		var token = validateJwtTokenInput.Token;

		if (string.IsNullOrEmpty(token))
		{
			return new()
			{
				Status = 400,
				Data = new()
				{
					IsValidToken = false,
					TokenExpire = null
				},
				Error = true,
				Message = Messages.Unauthorized
			};
		}

		try
		{

			token = token.Replace("Bearer ", "");

			var tokenHandler = new JwtSecurityTokenHandler();
			var secretKey = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]);

			tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(secretKey),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero
			}, out SecurityToken jsonToken);

			var jwtToken = jsonToken as JwtSecurityToken;

			DateTime tokenExpiration = jwtToken.ValidTo.AddHours(-3);

			return new()
			{
				Status = 200,
				Error = false,
				Message = Messages.Authenticated,
				Data = new()
				{
					IsValidToken = true,
					TokenExpire = tokenExpiration
				}
			};

		}
		catch (Exception ex)
		{
			return new()
			{
				Status = 500,
				Data = new()
				{
					IsValidToken = false,
					TokenExpire = null
				},
				Error = true,
				Message = Messages.Error + ex
			};
		}
	}
}