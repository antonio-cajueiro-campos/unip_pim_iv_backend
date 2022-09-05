using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.Transport;

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

	public DefaultResponse<AuthenticateOutput> Handle(AuthenticateInput authenticateInput)
	{
		return this.Authenticate(authenticateInput);
	}

	public DefaultResponse<AuthenticateOutput> Authenticate(AuthenticateInput authenticateInput)
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

			return new () {
				StatusCode = 200,
				Error = false,
				Data = new ("Bearar " + GenerateToken(credential)),
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
