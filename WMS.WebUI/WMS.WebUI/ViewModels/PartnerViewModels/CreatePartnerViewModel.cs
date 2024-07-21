namespace WMS.WebUI.ViewModels.PartnerViewModels;

public class CreatePartnerViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Balance { get; set; }
    public PartnerType Type { get; set; }
}
