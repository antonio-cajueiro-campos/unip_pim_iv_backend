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
using TSB.Portal.Backend.CrossCutting.Extensions;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate;
public class AuthenticateUseCase : IDefaultUseCase<AuthenticateOutput, AuthenticateInput>
{
	private readonly DataContext database;
	private readonly IConfiguration configuration;
	private readonly IDefaultUseCase<ValidateJwtTokenOutput, ValidateJwtTokenInput> validateJwtToken;
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
			Credential credential = database.Credentials.SingleOrDefault(x => x.Username == authenticateInput.Username.Trim());
			
			if (credential == null || !BCryptNet.Verify(authenticateInput.Password, credential.Password))
			{
				return new () {
					Status = 404,
					Error = true,
					Data = null,
					Message = Messages.UserNotFound
				};
			}

			var token = GenerateToken(credential);
			var refreshToken = GenerateToken(credential, true);
			var expirationTime = token.GetExpirationDateFromToken();

			return new () {
				Status = 200,
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
				Status = 500,
				Error = true,
				Data = null,
				Message = Messages.Error + ex
			};
		}
	}

	private string GenerateToken(Credential credential, bool isRefreshToken = false)
	{
		var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
		var credenciais = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

		var claims = new List<Claim>();
		claims.Add(new Claim("Id", credential.Id.ToString()));
		claims.Add(new Claim(ClaimTypes.Role, credential.Role));

		var tokenExpirationHours = isRefreshToken ? "RefreshTokenExpirationHours" : "TokenExpirationHours";
		var expirationTime = configuration["Jwt:" + tokenExpirationHours];

		var jwt = new JwtSecurityToken(
			issuer: configuration["Jwt:ValidIssuer"],
			expires: DateTime.Now.AddHours(Convert.ToDouble(expirationTime)),
			audience: configuration["Jwt:ValidAudience"],
			signingCredentials: credenciais,
			claims: claims
		);

		return new JwtSecurityTokenHandler().WriteToken(jwt);
	}
}
