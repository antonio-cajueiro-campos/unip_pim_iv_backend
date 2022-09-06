using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Application.UseCases.ValidateJwtToken;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate;
public class AuthenticateUseCase : IDefaultUseCase<AuthenticateOutput, AuthenticateInput>
{
	private DataContext database { get; set; }
	private IConfiguration configuration { get; set; }
	private IDefaultUseCase<ValidateJwtTokenOutput, ValidateJwtTokenInput> validateJwtToken { get; set; }
	public AuthenticateUseCase(DataContext database, IConfiguration configuration, IDefaultUseCase<ValidateJwtTokenOutput, ValidateJwtTokenInput> validateJwtToken)
	{
		this.database = database;
		this.configuration = configuration;
		this.validateJwtToken = validateJwtToken;
	}

	public DefaultResponse<AuthenticateOutput> Handle(AuthenticateInput authenticateInput)
	{
		return this.Authenticate(authenticateInput);
	}

	private DefaultResponse<AuthenticateOutput> Authenticate(AuthenticateInput authenticateInput)
	{
		try
		{
			Credential credential = database.Credentials.SingleOrDefault(x => x.Username == authenticateInput.Username);
			
			if (credential == null || !BCryptNet.Verify(authenticateInput.Password, credential.Password))
			{
				return new () {
					StatusCode = 404,
					Error = true,
					Data = null,
					Message = Messages.UserNotFound
				};
			}

			var token = GenerateToken(credential);
			var refreshToken = GenerateRefreshToken(credential);
			var expirationTime = GetExpirationTimeToken(token);

			return new () {
				StatusCode = 200,
				Error = false,
				Data = new () {
					Jwt = new () {
						Token = "Bearer " + token,
						RefreshToken = "Bearer " + refreshToken,
						ExpirationTime = expirationTime
					}					
				},
				Message = Messages.Authenticated
			};
		}
		catch (Exception ex)
		{
			return new () {
				StatusCode = 500,
				Error = true,
				Data = null,
				Message = Messages.Error + ex
			};
		}
	}

	private string GenerateToken(Credential credential)
	{
		var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
		var credenciais = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

		var claims = new List<Claim>();
		claims.Add(new Claim("Id", credential.Id.ToString()));
		claims.Add(new Claim(ClaimTypes.Role, credential.Role));

		var expirationTime = configuration["Jwt:TokenExpirationHours"];

		var jwt = new JwtSecurityToken(
			issuer: configuration["Jwt:ValidIssuer"],
			expires: DateTime.Now.AddHours(Convert.ToDouble(expirationTime)),
			audience: configuration["Jwt:ValidAudience"],
			signingCredentials: credenciais,
			claims: claims
		);

		return new JwtSecurityTokenHandler().WriteToken(jwt);
	}

	private string GenerateRefreshToken(Credential credential)
	{
		var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
		var credenciais = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

		var claims = new List<Claim>();
		claims.Add(new Claim("Id", credential.Id.ToString()));
		claims.Add(new Claim(ClaimTypes.Role, credential.Role));

		var expirationTime = configuration["Jwt:RefreshTokenExpirationHours"];

		var jwt = new JwtSecurityToken(
			issuer: configuration["Jwt:ValidIssuer"],
			expires: DateTime.Now.AddHours(Convert.ToDouble(expirationTime)),
			audience: configuration["Jwt:ValidAudience"],
			signingCredentials: credenciais,
			claims: claims
		);

		return new JwtSecurityTokenHandler().WriteToken(jwt);
	}

	private DateTime? GetExpirationTimeToken(string token)
	{
		var tokenValidated = this.validateJwtToken.Handle(new () {
			Token = token
		});

		return tokenValidated.Data.TokenExpire;
	}
}
