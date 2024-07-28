using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly HttpClient _client;

    public AccountController()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:7097/api/");
    }

    public IActionResult Login()
    {

    return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _client.PostAsJsonAsync("auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Response.Cookies.Append("JWT", token, new CookieOptions { HttpOnly = true, Secure = true });
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }
}
