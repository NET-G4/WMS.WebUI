using System.ComponentModel.DataAnnotations;

namespace WMS.WebUI.ViewModels.Auth;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? ClientUri { get; set; } = null;
    public string? Device { get; set; }
    public string? OperatingSystem { get; set; }
}
