using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WMS.WebUI.Helpers;

public static class JwtHelper
{
    public static List<string> GetUserRoles(string token)
    {
        var claims = GetTokenClaims(token);
        var roles = claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
        
        return roles;
    }

    private static IEnumerable<Claim> GetTokenClaims(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        return jwtToken.Claims;
    }
}
