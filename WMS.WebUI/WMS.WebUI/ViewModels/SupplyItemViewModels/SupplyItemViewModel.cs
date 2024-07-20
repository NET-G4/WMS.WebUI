using System.ComponentModel;

namespace WMS.WebUI.ViewModels.SupplyItemViewModels;

public class SupplyItemViewModel
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    [DisplayName("Unit price")]
    public decimal UnitPrice { get; set; }
    public int ProductId { get; set; }
    public virtual string Product { get; set; }
    public int SupplyId { get; set; }
}
