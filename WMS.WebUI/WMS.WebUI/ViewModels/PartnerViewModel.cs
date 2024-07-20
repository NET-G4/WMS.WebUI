namespace WMS.WebUI.ViewModels;

public class PartnerViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public PartnerType Type { get; set; }
    public string PhoneNumber { get; set; }
}

public enum PartnerType
{
    Customer,
    Supplier
}
