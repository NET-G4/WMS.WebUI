namespace WMS.WebUI.ViewModels.CustomerViewModels;

public class CustomerActionViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public decimal Balance { get; set; }
    public decimal? Discount { get; set; } = 0;
}
