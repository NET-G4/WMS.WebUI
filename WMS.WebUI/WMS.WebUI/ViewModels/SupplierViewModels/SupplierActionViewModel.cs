using System.ComponentModel;

namespace WMS.WebUI.ViewModels.SupplierViewModels;

public class SupplierActionViewModel
{
    public int Id { get; set; }

    [DisplayName("First name")]
    public string FirstName { get; set; }

    [DisplayName("Last name")]
    public string LastName { get; set; }
    
    [DisplayName("Phone number")]
    public string PhoneNumber { get; set; }
    
    public decimal Balance { get; set; }
    //public ICollection<SupplyDto> Supplies { get; set; }
    //public SupplierDto()
    //{
    //    Supplies = new List<SupplyDto>();
    //}
}
