using System.ComponentModel;

namespace WMS.WebUI.ViewModels.CustomerViewModels;

public class CustomerActionViewModel
{
    public int Id { get; set; }

    [DisplayName("First name")]
    public string FirstName { get; set; }
    
    [DisplayName("Last name")]
    public string LastName { get; set; }
    
    [DisplayName("Phone number")]
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public decimal Balance { get; set; }
    public decimal? Discount { get; set; } = 0;
}
