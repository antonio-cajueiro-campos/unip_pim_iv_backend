using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TSB.Portal.Backend.CrossCutting.Extensions
{
	public static class JwtUtilsExtensions
	{
		public static bool IsTheRequestedOneId(this ClaimsPrincipal user, long givenId) =>
            givenId.ToString().Equals(user.GetUserId());

		public static long GetUserId(this ClaimsPrincipal user)
        {
            return long.Parse(user.Claims.First(c => c.Type.Equals("Id")).Value);
        }

		public static string GetUserRole(this ClaimsPrincipal user)
        {
            return user.Claims.First(c => c.Type == ClaimTypes.Role).Value;
        }

		public static bool HasRole(this ClaimsPrincipal user, string role)
        {
            Claim userClaim = user.Claims.First(c => c.Type == ClaimTypes.Role);
            return userClaim.Value.Equals(role);
        }

		public static string GetUserRoleFromToken(this string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var jsonToken = tokenHandler.ReadToken(CleanBearer(token));

			var jwtToken = jsonToken as JwtSecurityToken;

			return jwtToken.Claims.First(claim => claim.Type == "role").Value;
		}

		public static DateTime GetExpirationDateFromToken(this string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var jsonToken = tokenHandler.ReadToken(CleanBearer(token));

			var jwtToken = jsonToken as JwtSecurityToken;

			return jwtToken.ValidTo.AddHours(-3);
		}

		private static string CleanBearer(string token)
		{
			return token.Replace("Bearer ", "");
		}
	}
}
