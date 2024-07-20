using System.ComponentModel;
using WMS.WebUI.ViewModels.SupplyItemViewModels;

namespace WMS.WebUI.ViewModels.SupplyViewModels;

public class SupplyViewModel
{
    public int Id { get; set; }
    [DisplayName("Total due")]
    public decimal TotalDue { get; set; }
    [DisplayName("Total paid")]
    public decimal TotalPaid { get; set; }
    public DateTime Date { get; set; }
    public int SupplierId { get; set; }
    public string Supplier { get; set; }
    public ICollection<SupplyItemViewModel> SupplyItems { get; set; }
    public SupplyViewModel()
    {
        SupplyItems = [];
    }
}
