using System.ComponentModel;

namespace WMS.WebUI.ViewModels.SupplierViewModels;

public class SupplierDisplayViewModel
{
    public int Id { get; set; }
    [DisplayName("Full name")]
    public string FullName{ get; set; }
    [DisplayName("Phone number")]
    public string PhoneNumber { get; set; }
    public decimal Balance { get; set; }
}
