using Microsoft.AspNetCore.Mvc;

namespace TSB.Portal.Backend.Api.Extensions;
public static class ResultExtensions
{
	public static ObjectResult SetStatus(this ObjectResult self, int statusCode)
	{
		self.StatusCode = statusCode;
		
		return self;
	}
}
