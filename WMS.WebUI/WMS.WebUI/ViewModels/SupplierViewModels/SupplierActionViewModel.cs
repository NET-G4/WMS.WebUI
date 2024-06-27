namespace WMS.WebUI.ViewModels.SupplierViewModels;

public class SupplierActionViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Balance { get; set; }
    //public ICollection<SupplyDto> Supplies { get; set; }
    //public SupplierDto()
    //{
    //    Supplies = new List<SupplyDto>();
    //}
}
