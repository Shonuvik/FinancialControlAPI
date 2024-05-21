using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace Presentation.Extensions
{
	public static class ValidateTokenExtension
	{
		public static string GetName(this StringValues values)
		{
            var handler = new JwtSecurityTokenHandler();
            var token = values.ToString().Split(" ")[1];
            var claims = handler.ReadToken(token) as JwtSecurityToken;
            return claims?.Claims?.FirstOrDefault(x => x.Type == "unique_name")?.Value ?? string.Empty;
        }
	}
}

