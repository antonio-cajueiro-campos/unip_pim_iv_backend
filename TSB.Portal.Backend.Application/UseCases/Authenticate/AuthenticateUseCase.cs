using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
using TSB.Portal.Backend.Infra.Repositories.CredentialRepository.Entity;
using TSB.Portal.Backend.Infra.Repositories;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate;
public class AuthenticateUseCase : IAuthenticateUseCase
{
	private DataContext database { get; set; }
	private IConfiguration configuration { get; set; }
	public AuthenticateUseCase(DataContext database, IConfiguration configuration)
	{
		this.database = database;
		this.configuration = configuration;
	}

	public AuthenticateOutput Login(AuthenticateInput credentials)
	{
		AuthenticateOutput authenticateOutput = this.Authenticate(credentials);

		return authenticateOutput;
	}

	// public ResponseModel ValidateJwtToken(string token)
	// {
	// 	if (string.IsNullOrEmpty(token)) return GenericResponses.Unauthorized();

	// 	token = token.Replace("Bearer ", "");

	// 	var tokenHandler = new JwtSecurityTokenHandler();
	// 	var secretKey = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]);
	// 	try
	// 	{
	// 		tokenHandler.ValidateToken(token, new TokenValidationParameters
	// 		{
	// 			ValidateIssuerSigningKey = true,
	// 			IssuerSigningKey = new SymmetricSecurityKey(secretKey),
	// 			ValidateIssuer = false,
	// 			ValidateAudience = false,
	// 			ClockSkew = TimeSpan.Zero
	// 		}, out SecurityToken validatedToken);

	// 		var jwtToken = (JwtSecurityToken)validatedToken;

	// 		DateTime tokenExpiration = jwtToken.ValidTo.AddHours(-3);

	// 		return token.Authenticated($"JWT token válido até {tokenExpiration}");
	// 	}
	// 	catch
	// 	{
	// 		return GenericResponses.Unauthorized();
	// 	}
	// }

	public AuthenticateOutput Authenticate(AuthenticateInput authenticateInput)
	{
		try
		{
			Credential credential = database.Credentials.Include(c => c.Roles).SingleOrDefault(x => x.Username == authenticateInput.Username);

			if (credential == null || !BCryptNet.Verify(authenticateInput.Password, credential.Password))
			{
				return new AuthenticateOutput();
			}

			return new AuthenticateOutput(GenerateToken(credential));
		}
		catch (InvalidOperationException)
		{
			return new AuthenticateOutput();
		}
	}

	private string GenerateToken(Credential credential)
	{
		var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
		var credenciais = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

		var claims = new List<Claim>();
		claims.Add(new Claim("Id", credential.Id.ToString()));

		foreach (var authRole in credential.Roles)
		{
			var role = this.database.Roles.Find(authRole.RoleId);
			claims.Add(new Claim(ClaimTypes.Role, role.Name));
		}

		var jwt = new JwtSecurityToken(
			issuer: configuration["Jwt:ValidIssuer"],
			expires: DateTime.Now.AddDays(7),
			audience: configuration["Jwt:ValidAudience"],
			signingCredentials: credenciais,
			claims: claims
		);
		return new JwtSecurityTokenHandler().WriteToken(jwt);
	}
}
