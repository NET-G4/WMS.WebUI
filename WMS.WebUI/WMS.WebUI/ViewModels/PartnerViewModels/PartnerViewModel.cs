namespace WMS.WebUI.ViewModels.PartnerViewModels;

public class PartnerViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal Balance { get; set; }
    public PartnerType Type { get; set; }
}

public enum PartnerType
{
    Customer,
    Supplier
}
