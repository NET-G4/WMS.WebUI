namespace WMS.WebUI.ViewModels.PartnerViewModels;

public class EditPartnerViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Balance { get; set; }
    public PartnerType Type { get; set; }
}
