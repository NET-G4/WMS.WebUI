using System.ComponentModel;

namespace WMS.WebUI.ViewModels.PartnerViewModels;

public class PartnerViewModel
{
    public int Id { get; set; }
    
    [DisplayName("Full name")]
    public string FullName { get; set; }

    [DisplayName("Phone number")]
    public string? PhoneNumber { get; set; }
    public decimal Balance { get; set; }
    public PartnerType Type { get; set; }
}

public enum PartnerType
{
    Customer,
    Supplier
}
