using System.ComponentModel.DataAnnotations;

namespace WMS.WebUI.Configurations;

public sealed class ApiConfiguration
{
    public const string SectionName = "API";

    [Required]
    public string Url { get; init; }
}
