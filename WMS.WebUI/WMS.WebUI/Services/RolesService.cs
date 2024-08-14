using WMS.WebUI.Helpers;

namespace WMS.WebUI.Services;

public class RolesService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RolesService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool HasRole(params string[] roles)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["JWT"];

        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        var userRoles = JwtHelper.GetUserRoles(token);
        var hasRole = userRoles.Exists(userRole => 
            roles.Any(role => role.Equals(userRole, StringComparison.OrdinalIgnoreCase)));

        return hasRole;
    }
}
