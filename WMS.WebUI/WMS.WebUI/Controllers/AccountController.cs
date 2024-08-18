using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Services.Interfaces;
using WMS.WebUI.ViewModels.Auth;

namespace WMS.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly HttpClient _client;
    private readonly IUserAgentService _userAgentService;

    public AccountController(IUserAgentService userAgentService)
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:7097/api/");
        _userAgentService = userAgentService;
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

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.ClientUri = Url.Action("EmailConfirmed", "Account", null, Request.Scheme);
            var response = await _client.PostAsJsonAsync("auth/register", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ConfirmEmail");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    public IActionResult Logout()
    {
        var cookies = HttpContext.Request.Cookies;

        foreach (var cookie in cookies)
        {
            Response.Cookies.Delete(cookie.Key);
        }

        return RedirectToAction("Login");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        forgotPassword.ClientUri = Url.Action("ResetPassword", "Account", null, Request.Scheme);
        forgotPassword.Device = _userAgentService.GetDevice();
        forgotPassword.OperatingSystem = _userAgentService.GetOperatingSystem();

        var response = await _client.PostAsJsonAsync("auth/forgotpassword", forgotPassword);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(ResetConfirmation));
        }

        return RedirectToAction(nameof(HomeController.Error), "Home");
    }

    public IActionResult ResetConfirmation()
    {
        return View();
    }

    public IActionResult ResetPassword(string token, string email)
    {
        var resetView = new ResetPasswordViewModel
        {
            Token = token,
            Email = email
        };
        return View(resetView);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var response = await _client.PostAsJsonAsync("auth/resetPassword", resetPassword);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Login");
        }

        return View();
    }

    public async Task<IActionResult> EmailConfirmed(string token, string email)
    {
        var confirmEmail = new ConfirmEmailViewModel
        {
            Email = email,
            Token = token
        };

        var response = await _client.PostAsJsonAsync("auth/confirmEmail", confirmEmail);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("AccountConfirmation", new { isConfirmed = true, message = "You have successfully confirmed your email. Now, you can login to the system." });
        }

        return RedirectToAction("AccountConfirmation", new { isConfirmed = false, message = "There was an error confirming your email. Please, try again." });
    }

    public ActionResult ConfirmEmail()
    {
        return View();
    }

    public ActionResult AccountConfirmation(bool isConfirmed, string message)
    {
        ViewBag.IsConfirmed = isConfirmed;
        ViewBag.Message = message;
        return View();
    }
}