using WMS.WebUI.Helpers;

namespace WMS.WebUI.Services;

public class RolesService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RolesService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool HasRole(string role)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["JWT"];

        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        var roles = JwtHelper.GetUserRoles(token);

        return roles.Exists(x => x.Equals(role, StringComparison.InvariantCultureIgnoreCase));
    }
}
