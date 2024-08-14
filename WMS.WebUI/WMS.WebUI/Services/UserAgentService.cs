using UAParser;
using WMS.WebUI.Services.Interfaces;

namespace WMS.WebUI.Services;

public class UserAgentService : IUserAgentService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UserAgentService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public string GetDevice()
	{
		var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
		if (string.IsNullOrEmpty(userAgent))
		{
			return "Unknown Device";
		}

		var uaParser = Parser.GetDefault();
		var clientInfo = uaParser.Parse(userAgent);

		return clientInfo.UA.ToString();
	}

	public string GetOperatingSystem()
	{
		var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
		if (string.IsNullOrEmpty(userAgent))
		{
			return "Unknown OS";
		}

		var uaParser = Parser.GetDefault();
		var clientInfo = uaParser.Parse(userAgent);

		return clientInfo.OS.ToString();
	}
}
