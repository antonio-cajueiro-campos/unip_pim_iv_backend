using System.Text;
using TSB.Portal.Backend.Application.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken.Interfaces;

namespace TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;
public class ValidateJwtTokenUseCase : IValidateJwtTokenUseCase
{
	private DataContext database { get; set; }
	private IConfiguration configuration { get; set; }
	public ValidateJwtTokenUseCase(DataContext database, IConfiguration configuration)
	{
		this.database = database;
		this.configuration = configuration;
	}

	public DefaultResponse<ValidateJwtTokenOutput> Handle(ValidateJwtTokenInput validateJwtTokenInput)
	{
		ValidateJwtTokenOutput validateJwtTokenOutput;

		try
		{
			validateJwtTokenOutput = ValidateJwtToken(validateJwtTokenInput);
		}
		catch (Exception ex)
		{
			return new()
			{
				StatusCode = 401,
				Data = new()
				{
					IsValidToken = false,
					TokenExpire = null
				},
				Error = true,
				Message = Messages.Error + ex
			};
		}

		if (!validateJwtTokenOutput.IsValidToken)
		{
			return new()
			{
				StatusCode = 400,
				Message = Messages.Unauthorized,
				Data = null,
				Error = true
			};
		}

		return new()
		{
			StatusCode = 200,
			Message = Messages.Authenticated,
			Data = validateJwtTokenOutput,
			Error = false
		};
	}


	public ValidateJwtTokenOutput ValidateJwtToken(ValidateJwtTokenInput validateJwtTokenInput)
	{
		var token = validateJwtTokenInput.Token;

		if (string.IsNullOrEmpty(token))
			return new ValidateJwtTokenOutput
			{
				IsValidToken = false,
				TokenExpire = null
			};

		token = validateJwtTokenInput.Token.Replace("Bearer ", "");

		var tokenHandler = new JwtSecurityTokenHandler();
		var secretKey = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]);

		tokenHandler.ValidateToken(token, new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(secretKey),
			ValidateIssuer = false,
			ValidateAudience = false,
			ClockSkew = TimeSpan.Zero
		}, out SecurityToken validatedToken);

		var jwtToken = (JwtSecurityToken)validatedToken;

		DateTime tokenExpiration = jwtToken.ValidTo.AddHours(-3);

		return new()
		{
			IsValidToken = true,
			TokenExpire = tokenExpiration
		};

	}
}