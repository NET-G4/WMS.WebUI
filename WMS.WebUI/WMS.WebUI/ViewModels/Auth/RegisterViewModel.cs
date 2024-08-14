using System.ComponentModel.DataAnnotations;

namespace WMS.WebUI.ViewModels.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string? ClientUri { get; set; }
}
